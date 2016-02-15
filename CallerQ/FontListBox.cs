using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MobileSrc.Sideline
{
    // Derive an implementation of the
    // OwnerDrawnListBox class
    class FontListBox : OwnerDrawnListBox<string>
    {
        const int FONT_SIZE = 10;
        const int DRAW_OFFSET = 5;

        public FontListBox()
        {

            // Determine what the item height should be
            // by adding 30% padding after measuring
            // the letter A with the selected font.
            Graphics g = this.CreateGraphics();
            this.ItemHeight = 4*(int)(g.MeasureString("A", this.Font).Height * 1.3);
            g.Dispose();
        }

        // Determine what the text color should be
        // for the selected item drawn as highlighted
        Color CalcTextColor(Color backgroundColor)
        {
            if (backgroundColor.Equals(Color.Empty))
            {
                return Color.Black;
            }

            int sum = backgroundColor.R + backgroundColor.G + backgroundColor.B;

            if (sum > 256)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Font font;
            Color fontColor;

            // The base class contains a bitmap, offScreen, for constructing
            // the control and is rendered when all items are populated.
            // This technique prevents flicker.
            Graphics gOffScreen = Graphics.FromImage(this.OffScreen);
            gOffScreen.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            int itemTop = 0;

            // Draw the fonts in the list.
            for (int n = this.VScrollBar.Value; n < this.VScrollBar.Value + this.PaintCount; n++)
            {
                // If the font name contains "dings" it needs to be displayed
                // in the list box with a readable font with the default font.
                if (((string)this.FilteredItems[n]).ToLower().IndexOf("dings") != -1)
                {
                    font = new Font(this.Font.Name, FONT_SIZE, FontStyle.Regular);
                }
                else
                {
                    font = new Font((string)this.FilteredItems[n], FONT_SIZE, FontStyle.Regular);
                }

                // Draw the selected item to appear highlighted
                if (n == this.SelectedIndex)
                {
                    gOffScreen.FillRectangle(new SolidBrush(SystemColors.Highlight),
                        1,
                        itemTop + 1,
                        // If the scroll bar is visible, subtract the scrollbar width
                        // otherwise subtract 2 for the width of the rectangle
                        this.ClientSize.Width - (this.VScrollBar.Visible ? this.VScrollBar.Width : 2),
                        this.ItemHeight);
                    fontColor = CalcTextColor(SystemColors.Highlight);
                }
                else
                {
                    fontColor = this.ForeColor;
                }

                string data = (string)this.FilteredItems[n];
                SizeF stringSize = gOffScreen.MeasureString(data, font);
                // Draw the item
                gOffScreen.DrawString(data, font, new SolidBrush(fontColor), DRAW_OFFSET, itemTop + (this.ItemHeight - stringSize.Height) / 2);
                itemTop += this.ItemHeight;
            }

            // Draw the list box
            e.Graphics.DrawImage(this.OffScreen, 1, 1);

            gOffScreen.Dispose();
        }

        // Draws the external border around the control.

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //e.Graphics.Clear(this.BackColor);
            //e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
        }
    }
}