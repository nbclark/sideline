namespace MobileSrc.Sideline
{
    partial class DetailsView
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
            this._completeMenuItem = new System.Windows.Forms.MenuItem();
            this._menuMenuItem = new System.Windows.Forms.MenuItem();
            this.detailsGeneral1 = new MobileSrc.Sideline.DetailsGeneral();
            this.detailsNotes1 = new MobileSrc.Sideline.DetailsNotes();
            this._closeMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this._detailsMenuItem = new System.Windows.Forms.MenuItem();
            this._notesMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this._completeMenuItem);
            this.mainMenu1.MenuItems.Add(this._menuMenuItem);
            // 
            // _completeMenuItem
            // 
            this._completeMenuItem.Text = "Complete";
            this._completeMenuItem.Click += new System.EventHandler(this._completeMenuItem_Click);
            // 
            // _menuMenuItem
            // 
            this._menuMenuItem.MenuItems.Add(this._detailsMenuItem);
            this._menuMenuItem.MenuItems.Add(this._notesMenuItem);
            this._menuMenuItem.MenuItems.Add(this.menuItem1);
            this._menuMenuItem.MenuItems.Add(this._closeMenuItem);
            this._menuMenuItem.Text = "Menu";
            // 
            // detailsGeneral1
            // 
            this.detailsGeneral1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsGeneral1.AutoScroll = true;
            this.detailsGeneral1.BackColor = System.Drawing.SystemColors.Window;
            this.detailsGeneral1.Location = new System.Drawing.Point(7, 7);
            this.detailsGeneral1.Name = "detailsGeneral1";
            this.detailsGeneral1.Size = new System.Drawing.Size(466, 475);
            this.detailsGeneral1.TabIndex = 0;
            this.detailsGeneral1.TabStop = false;
            // 
            // detailsNotes1
            // 
            this.detailsNotes1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsNotes1.BackColor = System.Drawing.SystemColors.Window;
            this.detailsNotes1.Location = new System.Drawing.Point(7, 7);
            this.detailsNotes1.Name = "detailsNotes1";
            this.detailsNotes1.Size = new System.Drawing.Size(458, 480);
            this.detailsNotes1.TabIndex = 1;
            this.detailsNotes1.TabStop = false;
            // 
            // _closeMenuItem
            // 
            this._closeMenuItem.Text = "Close";
            this._closeMenuItem.Click += new System.EventHandler(this._closeMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // _detailsMenuItem
            // 
            this._detailsMenuItem.Text = "Details";
            this._detailsMenuItem.Click += new System.EventHandler(this._detailsMenuItem_Click);
            // 
            // _notesMenuItem
            // 
            this._notesMenuItem.Text = "Notes";
            this._notesMenuItem.Click += new System.EventHandler(this._notesMenuItem_Click);
            // 
            // DetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "DetailsView";
            this.Text = "Call Details";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem _completeMenuItem;
        private System.Windows.Forms.MenuItem _menuMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _detailsTabPage;
        private System.Windows.Forms.TabPage _notesTabPage;
        private DetailsGeneral detailsGeneral1;
        private DetailsNotes detailsNotes1;
        private System.Windows.Forms.MenuItem _closeMenuItem;
        private System.Windows.Forms.MenuItem _detailsMenuItem;
        private System.Windows.Forms.MenuItem _notesMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}