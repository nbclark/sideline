using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MobileSrc.Sideline
{
    public partial class DetailsReminder : UserControl
    {
        private CallRecord _record = null;
        private bool _isLoading = false;

        public DetailsReminder()
        {
            InitializeComponent();

            List<KVP<string, int>> items = new List<KVP<string, int>>();
            items.Add(new KVP<string, int>("Three days before deadline", -3));
            items.Add(new KVP<string, int>("Two days before deadline", -2));
            items.Add(new KVP<string, int>("One day before deadline", -1));
            items.Add(new KVP<string, int>("Day of the deadline", 0));

            _reminderDayComboBox.DataSource = items;
            _reminderDayComboBox.DisplayMember = "Key";
            _reminderDayComboBox.ValueMember = "Value";

            items = new List<KVP<string, int>>();

            DateTime time = DateTime.Now.Date;
            for (int i = 0; i < 24 * 2 * 2; ++i)
            {
                items.Add(new KVP<string, int>(time.ToString("hh:mm tt"), (int)time.TimeOfDay.TotalMinutes));
                time = time.AddMinutes(15);
            }

            _reminderTimeComboBox.DataSource = items;
            _reminderTimeComboBox.DisplayMember = "Key";
            _reminderTimeComboBox.ValueMember = "Value";
        }

        public new bool Focus()
        {
            SetReminderDate();
            return _reminderCheckBox.Focus();
        }

        public void SetRecord(CallRecord record)
        {
            _isLoading = true;
            _record = record;
            panel1.Enabled = _reminderCheckBox.Checked = !_record.WasNotified;

            int dayValue = (int)(_record.NotifyDate.Date.Subtract(_record.DueDate.Date)).TotalDays;
            int timeValue = (int)_record.NotifyDate.TimeOfDay.TotalMinutes;

            foreach (KVP<string, int> item in _reminderDayComboBox.Items)
            {
                if (item.Value == dayValue)
                {
                    _reminderDayComboBox.SelectedItem = item;
                    break;
                }
            }
            foreach (KVP<string, int> item in _reminderTimeComboBox.Items)
            {
                if (item.Value == timeValue)
                {
                    _reminderTimeComboBox.SelectedItem = item;
                    break;
                }
            }
            _isLoading = false;
        }

        private void _reminderCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            panel1.Enabled = _reminderCheckBox.Checked;
            _record.WasNotified = !_reminderCheckBox.Checked;
        }

        private void _reminderDayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetReminderDate();
        }

        private void _reminderTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetReminderDate();
        }

        private void SetReminderDate()
        {
            if (!_isLoading)
            {
                if (null != _record && null != _reminderDayComboBox.SelectedValue)
                {
                    _record.NotifyDate = _record.DueDate.Date.AddDays((int)_reminderDayComboBox.SelectedValue).AddMinutes((int)_reminderTimeComboBox.SelectedValue);
                    _reminderDateLabel.Text = string.Format("Reminder on {0}", _record.NotifyDate.ToString("MM/dd/yyyy @ hh:mm tt"));
                }
            }
        }
    }
}
