using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileSrc.Sideline
{
    public partial class ControlEditor : Form
    {
        public ControlEditor()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string title, Control control)
        {
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
            this.Text = title;

            return base.ShowDialog();
        }

        private void _saveMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void _cancelMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}