using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileSrc.Sideline
{
    public partial class DetailsView : Form
    {
        private CallRecord _record = null;

        public DetailsView()
        {
            InitializeComponent();
            this.detailsGeneral1.CallStarted += new EventHandler(detailsGeneral1_CallStarted);
            this.Load += new EventHandler(DetailsView_Load);
            this.Closing += new CancelEventHandler(DetailsView_Closing);

            if (Properties.Resources.IsWindowsMobileStandard)
            {
                detailsGeneral1.Dock = DockStyle.Fill;
                detailsNotes1.Dock = DockStyle.Fill;

                this.Controls.Add(detailsGeneral1);
            }
            else
            {
                // 
                // tabControl1
                // 
                this.tabControl1 = new System.Windows.Forms.TabControl();
                this._detailsTabPage = new System.Windows.Forms.TabPage();
                this._notesTabPage = new System.Windows.Forms.TabPage();
                this.tabControl1.SuspendLayout();
                this._detailsTabPage.SuspendLayout();
                this._notesTabPage.SuspendLayout();
                this.tabControl1.Controls.Add(this._detailsTabPage);
                this.tabControl1.Controls.Add(this._notesTabPage);
                this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.tabControl1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                this.tabControl1.Location = new System.Drawing.Point(0, 0);
                this.tabControl1.Name = "tabControl1";
                this.tabControl1.SelectedIndex = 0;
                this.tabControl1.Size = new System.Drawing.Size(480, 536);
                this.tabControl1.TabIndex = 0;
                this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                // 
                // _detailsTabPage
                // 
                this._detailsTabPage.AutoScroll = true;
                this._detailsTabPage.Controls.Add(this.detailsGeneral1);
                this._detailsTabPage.Location = new System.Drawing.Point(0, 0);
                this._detailsTabPage.Name = "_detailsTabPage";
                this._detailsTabPage.Size = new System.Drawing.Size(480, 485);
                this._detailsTabPage.Text = "Details";
                // 
                // _notesTabPage
                // 
                this._notesTabPage.Controls.Add(this.detailsNotes1);
                this._notesTabPage.Location = new System.Drawing.Point(0, 0);
                this._notesTabPage.Name = "_notesTabPage";
                this._notesTabPage.Size = new System.Drawing.Size(472, 490);
                this._notesTabPage.Text = "Notes";
                this.Controls.Add(this.tabControl1);
                this._detailsTabPage.ResumeLayout(false);
                this._notesTabPage.ResumeLayout(false);
                this.tabControl1.ResumeLayout(false);
            }
        }

        void DetailsView_Load(object sender, EventArgs e)
        {
            this.detailsGeneral1.Focus();
        }

        void DetailsView_Closing(object sender, CancelEventArgs e)
        {
            Program.SetNotification();
        }

        void detailsGeneral1_CallStarted(object sender, EventArgs e)
        {
            if (Settings.Default.SwitchToNotesEnabled)
            {
                this.tabControl1.SelectedIndex = 1;
            }
        }

        public DialogResult ShowDialog(CallRecord record, bool newRecord)
        {
            _record = record;
            this.detailsGeneral1.CallRecord = record;
            this.detailsNotes1.CallRecord = record;
            this._completeMenuItem.Enabled = record.IsActive;

            if (newRecord)
            {
                this._completeMenuItem.Enabled = false;
                this._closeMenuItem.Text = "Save";
            }

            Cursor.Current = Cursors.Default;
            return base.ShowDialog();
        }

        private void _closeMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        this.detailsGeneral1.Focus();
                    }
                    break;
                case 1:
                    {
                        this.detailsNotes1.Focus();
                    }
                    break;
            }
        }

        private void _completeMenuItem_Click(object sender, EventArgs e)
        {
            _record.IsActive = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void _detailsMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Resources.IsWindowsMobileStandard)
            {
                this.Controls.Clear();
                this.Controls.Add(detailsGeneral1);
                detailsGeneral1.Focus();
            }
            else
            {
                this.tabControl1.TabIndex = 0;
            }
        }

        private void _notesMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Resources.IsWindowsMobileStandard)
            {
                this.Controls.Clear();
                this.Controls.Add(detailsNotes1);
                detailsNotes1.Focus();
            }
            else
            {
                this.tabControl1.TabIndex = 1;
            }
        }
    }
}