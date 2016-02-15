using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace MobileSrc.Sideline
{
    public partial class Notifier : Form
    {
        private CallRecord _callRecord = null;
        public Notifier()
        {
            this.Closing += new CancelEventHandler(Notifier_Closing);
            InitializeComponent();
            Dictionary<string, int> snoozeAmounts = new Dictionary<string, int>();
            snoozeAmounts.Add("5 Minutes", 5);
            snoozeAmounts.Add("15 Minutes", 15);
            snoozeAmounts.Add("30 Minutes", 30);
            snoozeAmounts.Add("1 Hour", 60);
            snoozeAmounts.Add("3 Hours", 60 * 3);
            snoozeAmounts.Add("6 Hours", 60 * 6);
            snoozeAmounts.Add("12 Hours", 60 * 12);
            snoozeAmounts.Add("1 Day", 60 * 24);

            foreach (string snoozeAmount in snoozeAmounts.Keys)
            {
                TaggedMenuItem item = new TaggedMenuItem(snoozeAmount, snoozeAmounts[snoozeAmount]);
                item.Click += new EventHandler(item_Click);

                _snoozeMenuItem.MenuItems.Add(item);
            }
            this._viewDetailsbutton.Image = Properties.Resources.button_blue;
            this._viewDetailsbutton.PushImage = Properties.Resources.button_blue_hover;
            this._dueLabel.PushImage = Properties.Resources.button_black;
            this._dueLabel.Image = Properties.Resources.button_black;
        }

        void Notifier_Closing(object sender, CancelEventArgs e)
        {
            this.timer1.Enabled = false;
        }

        public DialogResult ShowDialog(CallRecord record)
        {
            timer1.Enabled = true;
            _callRecord = record;

            if (DateTime.Now > record.DueDate)
            {
                this._dueLabel.Text = "Overdue";
            }
            else
            {
                string dueDate = string.Empty;

                if (record.DueDate.Date == DateTime.Now.Date)
                {
                    dueDate = "Today";
                }
                else if (record.DueDate.Date == DateTime.Now.AddDays(1).Date)
                {
                    dueDate = "Tomorrow";
                }
                else
                {
                    dueDate = record.DueDate.ToShortDateString();
                }

                this._dueLabel.Text = string.Format("Due {0}", dueDate);
            }
            this._callReasonLabel.Text = record.Description;
            this._contactPictureBox.Image = record.GetImage(this._contactPictureBox.Size);
            this._nameLabel.Text = record.Name;

            if (null == this._contactPictureBox.Image)
            {
                this._contactPictureBox.Image = Properties.Resources.user;
                this._contactPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            Led vib = new Led();
            vib.SetLedStatus(1, Led.LedState.On);
            System.Threading.Thread.Sleep(200);
            vib.SetLedStatus(1, Led.LedState.Off);
            System.Threading.Thread.Sleep(200);
            vib.SetLedStatus(1, Led.LedState.On);
            System.Threading.Thread.Sleep(200);
            vib.SetLedStatus(1, Led.LedState.Off); 

            return base.ShowDialog();
        }

        private void item_Click(object sender, EventArgs e)
        {
            TaggedMenuItem menuItem = (TaggedMenuItem)sender;
            _callRecord.WasNotified = false;
            _callRecord.NotifyDate = DateTime.Now.AddMinutes((int)menuItem.Tag);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _dismissMenu_Click(object sender, EventArgs e)
        {
            _callRecord.WasNotified = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void _viewDetailsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
            Program._callerQ.ShowDetails(_callRecord, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            _callRecord.NotifyDate = DateTime.Now.AddMinutes(30);
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }

    internal class TaggedMenuItem : MenuItem
    {
        public TaggedMenuItem()
        {
            //
        }
        public TaggedMenuItem(string text, int tag)
            : base()
        {
            this.Text = text;
            this.Tag = tag;
        }
        public object Tag
        {
            get;
            set;
        }
    }
}