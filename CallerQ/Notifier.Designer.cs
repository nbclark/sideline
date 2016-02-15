namespace MobileSrc.Sideline
{
    partial class Notifier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this._dismissMenu = new System.Windows.Forms.MenuItem();
            this._snoozeMenuItem = new System.Windows.Forms.MenuItem();
            this._contactPictureBox = new System.Windows.Forms.PictureBox();
            this._nameLabel = new System.Windows.Forms.Label();
            this._callReasonLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this._dueLabel = new MobileSrc.Sideline.ImageButton();
            this._viewDetailsbutton = new MobileSrc.Sideline.ImageButton();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this._dismissMenu);
            this.mainMenu1.MenuItems.Add(this._snoozeMenuItem);
            // 
            // _dismissMenu
            // 
            this._dismissMenu.Text = "Dismiss";
            this._dismissMenu.Click += new System.EventHandler(this._dismissMenu_Click);
            // 
            // _snoozeMenuItem
            // 
            this._snoozeMenuItem.Text = "Snooze";
            // 
            // _contactPictureBox
            // 
            this._contactPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._contactPictureBox.BackColor = System.Drawing.Color.Black;
            this._contactPictureBox.Location = new System.Drawing.Point(0, 79);
            this._contactPictureBox.Name = "_contactPictureBox";
            this._contactPictureBox.Size = new System.Drawing.Size(480, 265);
            this._contactPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // _nameLabel
            // 
            this._nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nameLabel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this._nameLabel.ForeColor = System.Drawing.Color.White;
            this._nameLabel.Location = new System.Drawing.Point(0, 347);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(480, 47);
            this._nameLabel.Text = "label2";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _callReasonLabel
            // 
            this._callReasonLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._callReasonLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this._callReasonLabel.ForeColor = System.Drawing.Color.White;
            this._callReasonLabel.Location = new System.Drawing.Point(0, 394);
            this._callReasonLabel.Name = "_callReasonLabel";
            this._callReasonLabel.Size = new System.Drawing.Size(480, 116);
            this._callReasonLabel.Text = "Same text\r\nThis is some sample test\r\nasdkljalksd\r\nasd\r\n";
            this._callReasonLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _dueLabel
            // 
            this._dueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this._dueLabel.BorderColor = System.Drawing.Color.Black;
            this._dueLabel.BorderWidth = 1;
            this._dueLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this._dueLabel.ForeColor = System.Drawing.Color.White;
            this._dueLabel.Image = null;
            this._dueLabel.IsTransparent = false;
            this._dueLabel.Location = new System.Drawing.Point(0, 0);
            this._dueLabel.Name = "_dueLabel";
            this._dueLabel.PushImage = null;
            this._dueLabel.Size = new System.Drawing.Size(480, 70);
            this._dueLabel.Stretch = true;
            this._dueLabel.TabIndex = 0;
            this._dueLabel.TabStop = false;
            this._dueLabel.Text = "View Call Details";
            // 
            // _viewDetailsbutton
            // 
            this._viewDetailsbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._viewDetailsbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this._viewDetailsbutton.BorderColor = System.Drawing.Color.Empty;
            this._viewDetailsbutton.BorderWidth = 0;
            this._viewDetailsbutton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this._viewDetailsbutton.ForeColor = System.Drawing.Color.White;
            this._viewDetailsbutton.Image = null;
            this._viewDetailsbutton.IsTransparent = false;
            this._viewDetailsbutton.Location = new System.Drawing.Point(5, 513);
            this._viewDetailsbutton.Name = "_viewDetailsbutton";
            this._viewDetailsbutton.PushImage = null;
            this._viewDetailsbutton.Size = new System.Drawing.Size(470, 70);
            this._viewDetailsbutton.Stretch = true;
            this._viewDetailsbutton.TabIndex = 0;
            this._viewDetailsbutton.Text = "View Call Details";
            this._viewDetailsbutton.Click += new System.EventHandler(this._viewDetailsbutton_Click);
            // 
            // Notifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this._callReasonLabel);
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._dueLabel);
            this.Controls.Add(this._viewDetailsbutton);
            this.Controls.Add(this._contactPictureBox);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "Notifier";
            this.Text = "Call Notification";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem _dismissMenu;
        private System.Windows.Forms.MenuItem _snoozeMenuItem;
        private System.Windows.Forms.PictureBox _contactPictureBox;
        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.Label _callReasonLabel;
        private ImageButton _viewDetailsbutton;
        private System.Windows.Forms.Timer timer1;
        private ImageButton _dueLabel;
    }
}