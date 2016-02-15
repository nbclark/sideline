using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace MobileSrc.Sideline
{
    internal class VerticalStretchPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            int width = this.Image.Width;
            int height = this.Image.Height;

            if (this.Height != this.Image.Height)
            {
                double scale = (double)this.Height / this.Image.Height;
                height = this.Height;
                width = (int)(this.Image.Width * scale);
            }

            e.Graphics.DrawImage(this.Image, new Rectangle(this.Width - width, 0, width, this.Height), new Rectangle(0, 0, this.Image.Width, this.Image.Height), GraphicsUnit.Pixel);
        }
    }

    internal class KeyFriendlyDateTimePicker : DateTimePicker
    {
        private int _gotKeyDown = 0;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Properties.Resources.IsWindowsMobileStandard)
            {
                base.OnKeyDown(e);
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    Win32.SendMessage(this.Handle, (int)Win32.WM_KEYDOWN, (int)Keys.PageDown, 0);
                    Win32.SendMessage(this.Handle, (int)Win32.WM_KEYUP, (int)Keys.PageDown, 0);
                    return;
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    _gotKeyDown++;
                    e.Handled = true;
                }
                else
                {
                    base.OnKeyDown(e);
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (Properties.Resources.IsWindowsMobileStandard)
            {
                base.OnKeyUp(e);
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                }
                else if (_gotKeyDown > 0)
                {
                    KeyFriendlyControl.OnKeyUp(this, e);
                }
                else
                {
                    base.OnKeyUp(e);
                }
                _gotKeyDown = 0;
            }
        }
    }

    internal class KeyFriendlyComboBox : ComboBox
    {
        private int _gotKeyDown = 0;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = (0 == Win32.SendMessage(this.Handle, 0x0157, 0, 0));
                _gotKeyDown++;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_gotKeyDown > 0)
            {
                _gotKeyDown = 0;
                int value = Win32.SendMessage(this.Handle, 0x0157, 0, 0);

                if (value == 0)
                {
                    e.Handled = true;
                    KeyFriendlyControl.OnKeyUp(this, e);
                    return;
                }
            }
            base.OnKeyUp(e);
        }
    }

    internal class KeyFriendlyTextBox : TextBox
    {
        private int _gotKeyDown = 0;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                _gotKeyDown++;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_gotKeyDown > 0)
            {
                KeyFriendlyControl.OnKeyUp(this, e);
            }
            _gotKeyDown = 0;
            base.OnKeyUp(e);
        }
    }

    internal class ImageButton : Control
    {
        private int _gotKeyDown = 0;
        private bool _isMouseDown = false;

        public ImageButton()
        {
            this.IsTransparent = true;
            this.BorderColor = Color.Empty;
            this.BorderWidth = 0;
            this.Stretch = true;
        }

        public int BorderWidth
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get;
            set;
        }

        public Bitmap Image
        {
            get;
            set;
        }

        public Bitmap PushImage
        {
            get;
            set;
        }

        public bool IsTransparent
        {
            get;
            set;
        }

        public bool Stretch
        {
            get;
            set;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isMouseDown = true;
            this.Refresh();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMouseDown = false;
            this.Refresh();
            base.OnMouseUp(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                OnClick(new EventArgs());
            }
            base.OnKeyPress(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                _gotKeyDown++;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_gotKeyDown > 0)
            {
                KeyFriendlyControl.OnKeyUp(this, e);
                e.Handled = true;
            }
            _gotKeyDown = 0;
            base.OnKeyUp(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.Refresh();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Refresh();
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (null == this.Image)
            {
                return;
            }
            Bitmap image = (_isMouseDown) ? this.PushImage : this.Image;

            if (image == null)
            {
                image = this.Image;
            }

            ImageAttributes attrs = new ImageAttributes();

            if (this.IsTransparent)
            {
                e.Graphics.Clear(this.BackColor);
                Color clr = this.Image.GetPixel(0, 0);
                attrs.SetColorKey(clr, clr);
            }
            int width = Math.Min(this.ClientSize.Width, image.Width);
            int height = Math.Min(this.ClientSize.Height, image.Height);

            Rectangle rect1 = (this.Stretch) ? this.ClientRectangle : new Rectangle(this.ClientRectangle.Left + (this.ClientSize.Width - width) / 2, this.ClientRectangle.Top + (this.ClientSize.Height - height) / 2, width, height);

            e.Graphics.DrawImage(image, rect1, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attrs);
            if (this.Focused)
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                rect.Inflate(-5, -5);

                using (Pen blackPen = new Pen(Color.Black, 1))
                {
                    e.Graphics.DrawRectangle(blackPen, rect);
                }
            }

            if (this.BorderWidth > 0 && this.BorderColor != Color.Empty)
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - (this.BorderWidth / 2), this.Height - (this.BorderWidth / 2));
                rect.Inflate(-this.BorderWidth / 2, -this.BorderWidth / 2);

                using (Pen blackPen = new Pen(this.BorderColor, this.BorderWidth))
                {
                    e.Graphics.DrawRectangle(blackPen, rect);
                }
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                using (SolidBrush brush = new SolidBrush(this.ForeColor))
                {
                    e.Graphics.DrawString(this.Text, this.Font, brush, this.ClientRectangle, format);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //
        }
    }

    internal static class KeyFriendlyControl
    {
        public static void OnKeyUp(Control c, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                bool b = c.Parent.SelectNextControl(c, true, true, true, true);
            }
            if (e.KeyCode == Keys.Up)
            {
                bool b = c.Parent.SelectNextControl(c, false, true, true, true);
            }
        }
    }
}
