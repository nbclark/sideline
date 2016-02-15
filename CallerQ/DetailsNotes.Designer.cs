namespace MobileSrc.Sideline
{
    partial class DetailsNotes
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
            this._callNotesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _callNotesTextBox
            // 
            this._callNotesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._callNotesTextBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._callNotesTextBox.Location = new System.Drawing.Point(0, 40);
            this._callNotesTextBox.Multiline = true;
            this._callNotesTextBox.Name = "_callNotesTextBox";
            this._callNotesTextBox.Size = new System.Drawing.Size(480, 400);
            this._callNotesTextBox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 40);
            this.label1.Text = "Call Notes";
            // 
            // DetailsNotes
            // 
            this.AutoScaleDimensions = Properties.Resources.ScaleDimensions;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this._callNotesTextBox);
            this.Controls.Add(this.label1);
            this.Name = "DetailsNotes";
            this.Size = new System.Drawing.Size(480, 440);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _callNotesTextBox;
        private System.Windows.Forms.Label label1;
    }
}
