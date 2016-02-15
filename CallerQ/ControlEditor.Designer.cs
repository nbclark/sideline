namespace MobileSrc.Sideline
{
    partial class ControlEditor
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
            this._saveMenuItem = new System.Windows.Forms.MenuItem();
            this._cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this._saveMenuItem);
            this.mainMenu1.MenuItems.Add(this._cancelMenuItem);
            // 
            // _saveMenuItem
            // 
            this._saveMenuItem.Text = "Save";
            this._saveMenuItem.Click += new System.EventHandler(this._saveMenuItem_Click);
            // 
            // _cancelMenuItem
            // 
            this._cancelMenuItem.Text = "Cancel";
            this._cancelMenuItem.Click += new System.EventHandler(this._cancelMenuItem_Click);
            // 
            // ControlEditor
            // 
            this.AutoScaleDimensions = Properties.Resources.ScaleDimensions;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "ControlEditor";
            this.Text = "TextEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem _saveMenuItem;
        private System.Windows.Forms.MenuItem _cancelMenuItem;
    }
}