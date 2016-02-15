namespace MobileSrc.Sideline
{
    partial class SettingsEditor
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
            this._integrateCheckBox = new System.Windows.Forms.CheckBox();
            this._remindersCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._reminderTimeComboBox = new KeyFriendlyComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._reminderDayComboBox = new KeyFriendlyComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._displayNotesCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
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
            // _integrateCheckBox
            // 
            this._integrateCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._integrateCheckBox.Location = new System.Drawing.Point(3, 3);
            this._integrateCheckBox.Name = "_integrateCheckBox";
            this._integrateCheckBox.Size = new System.Drawing.Size(474, 40);
            this._integrateCheckBox.TabIndex = 0;
            this._integrateCheckBox.Text = "Integrate with Contacts";
            this._integrateCheckBox.CheckStateChanged += new System.EventHandler(this._integrateCheckBox_CheckStateChanged);
            // 
            // _remindersCheckBox
            // 
            this._remindersCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._remindersCheckBox.Location = new System.Drawing.Point(3, 95);
            this._remindersCheckBox.Name = "_remindersCheckBox";
            this._remindersCheckBox.Size = new System.Drawing.Size(474, 40);
            this._remindersCheckBox.TabIndex = 2;
            this._remindersCheckBox.Text = "Enable Reminders";
            this._remindersCheckBox.CheckStateChanged += new System.EventHandler(this._remindersCheckBox_CheckStateChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._reminderTimeComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._reminderDayComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 172);
            // 
            // _reminderTimeComboBox
            // 
            this._reminderTimeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._reminderTimeComboBox.Location = new System.Drawing.Point(0, 121);
            this._reminderTimeComboBox.Name = "_reminderTimeComboBox";
            this._reminderTimeComboBox.Size = new System.Drawing.Size(474, 41);
            this._reminderTimeComboBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(474, 40);
            this.label2.Text = "Default Reminder Time";
            // 
            // _reminderDayComboBox
            // 
            this._reminderDayComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._reminderDayComboBox.Location = new System.Drawing.Point(0, 40);
            this._reminderDayComboBox.Name = "_reminderDayComboBox";
            this._reminderDayComboBox.Size = new System.Drawing.Size(474, 41);
            this._reminderDayComboBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(474, 40);
            this.label1.Text = "Default Reminder Date";
            // 
            // _displayNotesCheckBox
            // 
            this._displayNotesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._displayNotesCheckBox.Location = new System.Drawing.Point(3, 49);
            this._displayNotesCheckBox.Name = "_displayNotesCheckBox";
            this._displayNotesCheckBox.Size = new System.Drawing.Size(474, 40);
            this._displayNotesCheckBox.TabIndex = 1;
            this._displayNotesCheckBox.Text = "Switch to Notes During Call";
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._remindersCheckBox);
            this.Controls.Add(this._integrateCheckBox);
            this.Controls.Add(this._displayNotesCheckBox);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "SettingsEditor";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem _saveMenuItem;
        private System.Windows.Forms.MenuItem _cancelMenuItem;
        private System.Windows.Forms.CheckBox _integrateCheckBox;
        private System.Windows.Forms.CheckBox _remindersCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private KeyFriendlyComboBox _reminderDayComboBox;
        private KeyFriendlyComboBox _reminderTimeComboBox;
        private System.Windows.Forms.CheckBox _displayNotesCheckBox;
    }
}