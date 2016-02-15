using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileSrc.Sideline
{
    public partial class SettingsEditor : Form
    {
        public SettingsEditor()
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
            for (int i = 0; i < 24 * 2; ++i)
            {
                items.Add(new KVP<string, int>(time.ToString("hh:mm tt"), (int)time.TimeOfDay.TotalMinutes));
                time = time.AddMinutes(30);
            }

            _reminderTimeComboBox.DataSource = items;
            _reminderTimeComboBox.DisplayMember = "Key";
            _reminderTimeComboBox.ValueMember = "Value";
        }

        public new DialogResult ShowDialog()
        {
            panel1.Enabled = _remindersCheckBox.Checked = Settings.Default.RemindersEnabled;
            int dayValue = Settings.Default.DefaultReminderDay;
            int timeValue = Settings.Default.DefaultReminderTime;

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
            this._displayNotesCheckBox.DataBindings.Add("Checked", Settings.Default, "SwitchToNotesEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            this._integrateCheckBox.DataBindings.Add("Checked", Settings.Default, "IntegrationEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            this._remindersCheckBox.DataBindings.Add("Checked", Settings.Default, "RemindersEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            this._reminderDayComboBox.DataBindings.Add("SelectedValue", Settings.Default, "DefaultReminderDay", false, DataSourceUpdateMode.OnPropertyChanged);
            this._reminderTimeComboBox.DataBindings.Add("SelectedValue", Settings.Default, "DefaultReminderTime", false, DataSourceUpdateMode.OnPropertyChanged);

            return base.ShowDialog();
        }

        private void _saveMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void _cancelMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _remindersCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            Settings.Default.RemindersEnabled = panel1.Enabled = _remindersCheckBox.Checked;
        }

        private void _integrateCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            Settings.Default.IntegrationEnabled = _integrateCheckBox.Checked;
        }
    }
}