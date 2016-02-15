namespace MobileSrc.Sideline
{
    partial class DetailsReminder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._reminderCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._reminderDateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._reminderDayComboBox = new KeyFriendlyComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._reminderTimeComboBox = new KeyFriendlyComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _reminderCheckBox
            // 
            this._reminderCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._reminderCheckBox.Location = new System.Drawing.Point(0, 0);
            this._reminderCheckBox.Name = "_reminderCheckBox";
            this._reminderCheckBox.Size = new System.Drawing.Size(480, 40);
            this._reminderCheckBox.TabIndex = 0;
            this._reminderCheckBox.Text = "Enable Reminders";
            this._reminderCheckBox.CheckStateChanged += new System.EventHandler(this._reminderCheckBox_CheckStateChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._reminderTimeComboBox);
            this.panel1.Controls.Add(this._reminderDateLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._reminderDayComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 400);
            // 
            // _reminderDateLabel
            // 
            this._reminderDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._reminderDateLabel.Location = new System.Drawing.Point(0, 172);
            this._reminderDateLabel.Name = "_reminderDateLabel";
            this._reminderDateLabel.Size = new System.Drawing.Size(480, 40);
            this._reminderDateLabel.Text = "Reminder at";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 40);
            this.label2.Text = "Reminder Time";
            // 
            // _reminderDayComboBox
            // 
            this._reminderDayComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._reminderDayComboBox.Location = new System.Drawing.Point(0, 40);
            this._reminderDayComboBox.Name = "_reminderDayComboBox";
            this._reminderDayComboBox.Size = new System.Drawing.Size(480, 41);
            this._reminderDayComboBox.TabIndex = 1;
            this._reminderDayComboBox.SelectedIndexChanged += new System.EventHandler(this._reminderDayComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 40);
            this.label1.Text = "Reminder Date";
            // 
            // _reminderTimeComboBox
            // 
            this._reminderTimeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._reminderTimeComboBox.Location = new System.Drawing.Point(0, 121);
            this._reminderTimeComboBox.Name = "_reminderTimeComboBox";
            this._reminderTimeComboBox.Size = new System.Drawing.Size(480, 41);
            this._reminderTimeComboBox.TabIndex = 5;
            this._reminderTimeComboBox.SelectedIndexChanged += new System.EventHandler(this._reminderTimeComboBox_SelectedIndexChanged);
            // 
            // DetailsReminder
            // 
            this.AutoScaleDimensions = Properties.Resources.ScaleDimensions;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._reminderCheckBox);
            this.Name = "DetailsReminder";
            this.Size = new System.Drawing.Size(480, 440);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox _reminderCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private KeyFriendlyComboBox _reminderDayComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _reminderDateLabel;
        private KeyFriendlyComboBox _reminderTimeComboBox;

    }
}
