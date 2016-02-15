using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MobileSrc.Sideline
{
    // Derive an implementation of the
    // OwnerDrawnListBox class
    public enum TileSizeMode
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
    class ContactListBox : OwnerDrawnListBox<CallRecord>
    {
        const int DRAW_OFFSET = 5;
        int PICTURE_WIDTH = 20;
        int PICTURE_HEIGHT = 20;
        ImageList _imageList = new ImageList();

        static Dictionary<TileSizeMode, Font> TitleFonts = new Dictionary<TileSizeMode, Font>();
        static Dictionary<TileSizeMode, Font> NotesFonts = new Dictionary<TileSizeMode, Font>();
        static Dictionary<TileSizeMode, Font> DateFonts = new Dictionary<TileSizeMode, Font>();
        static Bitmap EmptyContact = null, HighPriority = null, LowPriority = null, NotifyOn = null, NotifyOff = null;

        static ContactListBox()
        {
            EmptyContact = Properties.Resources.user;
            HighPriority = Properties.Resources.highpriority;
            LowPriority = Properties.Resources.lowpriority;
            NotifyOn = Properties.Resources.notify;
            NotifyOff = Properties.Resources.notify_off;
        }

        private TileSizeMode _sizeMode = TileSizeMode.Small;

        public ContactListBox()
        {
            this.TileSize = TileSizeMode.Small;
        }

        private Font TitleFont
        {
            get { return TitleFonts[this.TileSize]; }
        }

        private Font DateFont
        {
            get { return DateFonts[this.TileSize]; }
        }

        private Font NotesFont
        {
            get { return NotesFonts[this.TileSize]; }
        }

        public TileSizeMode TileSize
        {
            get { return _sizeMode; }
            set
            {
                float titleFontSize, notesFontSize, dateFontSize, rowCount;
                switch (value)
                {
                    case TileSizeMode.Small:
                        {
                            titleFontSize = 8.5f;
                            notesFontSize = 7;
                            dateFontSize = 6;
                            rowCount = 1;
                        }
                        break;
                    case TileSizeMode.Medium:
                        {
                            titleFontSize = 9.5f;
                            notesFontSize = 8;
                            dateFontSize = 9;
                            rowCount = 2.65f;
                        }
                        break;
                    default:
                        {
                            titleFontSize = 10.5f;
                            notesFontSize = 9;
                            dateFontSize = 10;
                            rowCount = 4;
                        }
                        break;
                }

                if (!TitleFonts.ContainsKey(value))
                {
                    Font f = new Font(FontFamily.GenericSansSerif, titleFontSize, FontStyle.Bold);
                    TitleFonts.Add(value, f);
                }
                if (!NotesFonts.ContainsKey(value))
                {
                    Font f = new Font(FontFamily.GenericSansSerif, notesFontSize, FontStyle.Italic);
                    NotesFonts.Add(value, f);
                }
                if (!DateFonts.ContainsKey(value))
                {
                    Font f = new Font(FontFamily.GenericSansSerif, dateFontSize, FontStyle.Bold);
                    DateFonts.Add(value, f);
                }
                using (Graphics g = this.CreateGraphics())
                {
                    this.ItemHeight = (int)((DRAW_OFFSET * 2) + rowCount * (int)(g.MeasureString("A", TitleFonts[value]).Height));
                    PICTURE_HEIGHT = this.ItemHeight - (DRAW_OFFSET * 2);
                    PICTURE_WIDTH = (int)(PICTURE_HEIGHT * 3.0 / 4);
                }

                _sizeMode = value;

                OnResize(null);
                this.Refresh();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnItemAdded(object sender, EventArgs e)
        {
            base.OnItemAdded(sender, e);
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

        protected override void OnDrawItem(OwnerDrawnListBox<CallRecord>.DrawItemEventArgs e)
        {
            int itemTop = e.Bounds.Top;
            int itemLeft = e.Bounds.Left + DRAW_OFFSET;
            int textLeft = itemLeft + DRAW_OFFSET;
            int textRight = e.Bounds.Right - DRAW_OFFSET;

            CallRecord data = e.Item;

            string dateString = data.DueDate.ToShortDateString();

            Color dateColor = (data.IsActive) ? Color.Red : e.ForeColor;

            if (data.IsActive)
            {
                if (data.DueDate.Date < DateTime.Now.Date)
                {
                    dateString = "Overdue";
                    dateColor = Color.Red;
                }
                else if (data.DueDate.Date == DateTime.Now.Date)
                {
                    dateString = "Today";
                    dateColor = Color.Red;
                }
                else if (data.DueDate.Date == DateTime.Now.AddDays(1).Date)
                {
                    dateString = "Tomorrow";
                    dateColor = Color.Red;
                }
                else
                {
                    dateColor = e.ForeColor;
                }
            }
            else
            {
                dateString = "Completed";
            }

            dateString = string.Format("{0}", dateString, data.DueDate.ToShortTimeString());

            SizeF dateSize = e.Graphics.MeasureString(dateString, DateFont);
            SizeF titleSize = e.Graphics.MeasureString(data.Name, TitleFont);
            SizeF notesSize = e.Graphics.MeasureString(data.Description, NotesFont);

            StringFormat textFormat = new StringFormat();
            textFormat.LineAlignment = StringAlignment.Center;

            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                using (SolidBrush dateBrush = new SolidBrush(dateColor))
                {
                    ImageAttributes attrs = new ImageAttributes();
                    Image img = null;
                    if (this.TileSize != TileSizeMode.Small)
                    {
                        bool dispose = false;
                        img = e.Item.GetImage(new Size(PICTURE_WIDTH, e.Bounds.Height - (DRAW_OFFSET << 1)));
                        if (null == img)
                        {
                            int height = e.Bounds.Height - (DRAW_OFFSET << 1);
                            if (height < EmptyContact.Height)
                            {
                                img = CallRecord.GetImage(EmptyContact, new Size(PICTURE_WIDTH, height));
                                dispose = true;
                            }
                            else
                            {
                                img = EmptyContact;
                            }
                            attrs.SetColorKey(EmptyContact.GetPixel(0, 0), EmptyContact.GetPixel(0, 0));
                        }

                        e.Graphics.DrawImage
                        (
                            img,
                            new Rectangle(e.Bounds.Left + DRAW_OFFSET + ((PICTURE_WIDTH - img.Width) >> 1), e.Bounds.Top + DRAW_OFFSET/* + ((e.Bounds.Height - img.Height - DRAW_OFFSET - DRAW_OFFSET) >> 1)*/, img.Width, img.Height),
                            0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attrs
                        );

                        if (dispose)
                        {
                            img.Dispose();
                        }

                        textLeft += PICTURE_WIDTH;
                    }

                    Rectangle textRect = new Rectangle(textLeft, itemTop + DRAW_OFFSET, textRight - textLeft, (int)titleSize.Height);

                    Bitmap notImage = (data.WasNotified) ? NotifyOff : NotifyOn;
                    Bitmap priImage = (data.Priority == CallPriority.High) ? HighPriority : LowPriority;
                    attrs.SetColorKey(priImage.GetPixel(0, 0), priImage.GetPixel(0, 0));

                    int priImgWidth = Math.Min(textRect.Height + DRAW_OFFSET*2, priImage.Height);

                    e.Graphics.DrawImage
                    (
                        priImage,
                        new Rectangle(textRect.Right - DRAW_OFFSET - priImgWidth, textRect.Top + ((textRect.Height - priImgWidth) >> 1), priImgWidth, priImgWidth), 0, 0, priImage.Width, priImage.Height, GraphicsUnit.Pixel, attrs
                    );
                    attrs.SetColorKey(notImage.GetPixel(0, 0), notImage.GetPixel(0, 0));
                    e.Graphics.DrawImage
                    (
                        notImage,
                        new Rectangle(textRect.Right - (DRAW_OFFSET << 1) - (priImgWidth << 1), textRect.Top + ((textRect.Height - priImgWidth) >> 1), priImgWidth, priImgWidth), 0, 0, notImage.Width, notImage.Height, GraphicsUnit.Pixel, attrs
                    );
                    
                    Rectangle dateRect = new Rectangle(textRect.Left, textRect.Top, textRect.Width, textRect.Height);
                    if (this.TileSize == TileSizeMode.Small)
                    {
                        int dateWidth = 0;
                        foreach (string str in new string[] { "Tomorrow  ", "Completed  ", "Today  ", "Overdue  " })
                        {
                            dateWidth = Math.Max(dateWidth, (int)Math.Ceiling(e.Graphics.MeasureString(str, DateFont).Width));
                        }
                        dateRect = new Rectangle(textRect.Left, textRect.Top, dateWidth, textRect.Height);
                    }
                    // Draw the item
                    e.Graphics.DrawString(dateString, DateFont, dateBrush, dateRect, textFormat);

                    if (this.TileSize == TileSizeMode.Small)
                    {
                        textRect.Location = new Point(dateRect.Right + DRAW_OFFSET, textRect.Top);
                    }
                    else
                    {
                        textRect.Offset(0, (int)dateSize.Height);
                        textRect.Offset(0, DRAW_OFFSET);
                        e.Graphics.DrawLine(new Pen(Color.Black, 1), textLeft, textRect.Top, textRect.Right, textRect.Top);
                        textRect.Offset(0, DRAW_OFFSET);
                    }

                    e.Graphics.DrawString(data.Name, TitleFont, brush, textRect, textFormat);
                    textRect.Offset(0, (int)titleSize.Height);

                    if (this.TileSize == TileSizeMode.Large)
                    {
                        textRect.Height = (e.Bounds.Bottom - textRect.Top);

                        textFormat.LineAlignment = StringAlignment.Near;
                        e.Graphics.DrawString(data.Description, NotesFont, brush, textRect, textFormat);
                    }
                }
            }
        }

        int MulDiv(int a, int b, int c)
        {
            return (a * b) / c;
        }

        Size ScaleProportional(int uFitToWidth, int uFitToHeight, Image image)
        {
            // Scale (*puWidthToScale, *puHeightToScale) to fit within (uFitToWidth, uFitToHeight), while
            // maintaining the aspect ratio
            int nScaledWidth = MulDiv(image.Width, uFitToHeight, image.Height);
            int uWidthToScale, uHeightToScale;

            // If we didn't overflow and the scaled width does not exceed bounds
            if (nScaledWidth >= 0 && nScaledWidth <= (int)uFitToWidth)
            {
                uWidthToScale = nScaledWidth;
                uHeightToScale = uFitToHeight;
            }
            else
            {
                uHeightToScale = MulDiv(image.Height, uFitToWidth, image.Width);
                uWidthToScale = uFitToWidth;
            }

            return new Size(uWidthToScale, uHeightToScale);
        }
    }
}