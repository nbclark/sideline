namespace MobileSrc.Sideline
{
    partial class DetailsGeneral
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._contactPictureBox = new System.Windows.Forms.PictureBox();
            this._nameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._buttonPanel = new System.Windows.Forms.Panel();
            this._callReasonTextBox = new MobileSrc.Sideline.KeyFriendlyTextBox();
            this._alarmPictureBox = new MobileSrc.Sideline.ImageButton();
            this._commonPhrasesPictureBox = new MobileSrc.Sideline.ImageButton();
            this._priorityComboBox = new MobileSrc.Sideline.KeyFriendlyComboBox();
            this._dueDateDateTimePicker = new MobileSrc.Sideline.KeyFriendlyDateTimePicker();
            this.panel1.SuspendLayout();
            this._buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this._contactPictureBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 127);
            // 
            // _contactPictureBox
            // 
            this._contactPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._contactPictureBox.BackColor = System.Drawing.Color.White;
            this._contactPictureBox.Location = new System.Drawing.Point(1, 1);
            this._contactPictureBox.Name = "_contactPictureBox";
            this._contactPictureBox.Size = new System.Drawing.Size(109, 125);
            this._contactPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // _nameLabel
            // 
            this._nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nameLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._nameLabel.Location = new System.Drawing.Point(116, 0);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(323, 40);
            this._nameLabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(0, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 40);
            this.label1.Text = "Reason for Call";
            // 
            // _buttonPanel
            // 
            this._buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonPanel.Controls.Add(this._callReasonTextBox);
            this._buttonPanel.Location = new System.Drawing.Point(0, 173);
            this._buttonPanel.Name = "_buttonPanel";
            this._buttonPanel.Size = new System.Drawing.Size(480, 267);
            // 
            // _callReasonTextBox
            // 
            this._callReasonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._callReasonTextBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._callReasonTextBox.Location = new System.Drawing.Point(0, 0);
            this._callReasonTextBox.Multiline = true;
            this._callReasonTextBox.Name = "_callReasonTextBox";
            this._callReasonTextBox.Size = new System.Drawing.Size(480, 267);
            this._callReasonTextBox.TabIndex = 6;
            // 
            // _alarmPictureBox
            // 
            this._alarmPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._alarmPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this._alarmPictureBox.BorderColor = System.Drawing.Color.Empty;
            this._alarmPictureBox.BorderWidth = 0;
            this._alarmPictureBox.Image = null;
            this._alarmPictureBox.IsTransparent = true;
            this._alarmPictureBox.Location = new System.Drawing.Point(445, 3);
            this._alarmPictureBox.Name = "_alarmPictureBox";
            this._alarmPictureBox.PushImage = null;
            this._alarmPictureBox.Size = new System.Drawing.Size(32, 32);
            this._alarmPictureBox.Stretch = false;
            this._alarmPictureBox.TabIndex = 1;
            this._alarmPictureBox.Click += new System.EventHandler(this._alarmPictureBox_Click);
            // 
            // _commonPhrasesPictureBox
            // 
            this._commonPhrasesPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._commonPhrasesPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this._commonPhrasesPictureBox.BorderColor = System.Drawing.Color.Empty;
            this._commonPhrasesPictureBox.BorderWidth = 0;
            this._commonPhrasesPictureBox.Image = null;
            this._commonPhrasesPictureBox.IsTransparent = true;
            this._commonPhrasesPictureBox.Location = new System.Drawing.Point(445, 134);
            this._commonPhrasesPictureBox.Name = "_commonPhrasesPictureBox";
            this._commonPhrasesPictureBox.PushImage = null;
            this._commonPhrasesPictureBox.Size = new System.Drawing.Size(32, 32);
            this._commonPhrasesPictureBox.Stretch = false;
            this._commonPhrasesPictureBox.TabIndex = 4;
            this._commonPhrasesPictureBox.Click += new System.EventHandler(this._commonPhrasesPictureBox_Click);
            // 
            // _priorityComboBox
            // 
            this._priorityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._priorityComboBox.Location = new System.Drawing.Point(116, 87);
            this._priorityComboBox.Name = "_priorityComboBox";
            this._priorityComboBox.Size = new System.Drawing.Size(364, 41);
            this._priorityComboBox.TabIndex = 3;
            // 
            // _dueDateDateTimePicker
            // 
            this._dueDateDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dueDateDateTimePicker.Location = new System.Drawing.Point(116, 40);
            this._dueDateDateTimePicker.Name = "_dueDateDateTimePicker";
            this._dueDateDateTimePicker.Size = new System.Drawing.Size(364, 36);
            this._dueDateDateTimePicker.TabIndex = 2;
            // 
            // DetailsGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this._buttonPanel);
            this.Controls.Add(this._alarmPictureBox);
            this.Controls.Add(this._commonPhrasesPictureBox);
            this.Controls.Add(this._priorityComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dueDateDateTimePicker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._nameLabel);
            this.Name = "DetailsGeneral";
            this.Size = new System.Drawing.Size(480, 440);
            this.panel1.ResumeLayout(false);
            this._buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KeyFriendlyDateTimePicker _dueDateDateTimePicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox _contactPictureBox;
        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.Label label1;
        private KeyFriendlyComboBox _priorityComboBox;
        private ImageButton _commonPhrasesPictureBox;
        private ImageButton _alarmPictureBox;
        private System.Windows.Forms.Panel _buttonPanel;
        private KeyFriendlyTextBox _callReasonTextBox;
    }
}
