using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.WindowsMobile.Forms;
using Microsoft.WindowsMobile.PocketOutlook;

namespace MobileSrc.Sideline
{
    internal partial class Sideline : Form
    {
        private string _sortProperty = "DueDate";
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private DateFilter _dateFilter = new DateFilter(FilterSettings.All);
        private static readonly int MaxItemCount = 4;

        public Sideline()
        {
            InitializeComponent();
            /*
            for (int i = 0; i < 5; ++i)
            {
                Settings.Default.Queue.Add(new CallRecord(new Microsoft.WindowsMobile.PocketOutlook.Contact(new Microsoft.WindowsMobile.PocketOutlook.ItemId(-2147483647)), "1call to do something...", DateTime.Now.AddDays(-1), CallPriority.High));
            }
            */
            foreach (CallRecord record in Settings.Default.Queue)
            {
                _contactListBox.Items.Add(record);
            }

            _sortProperty = Settings.Default.SortProperty;
            _sortDirection = Settings.Default.SortDirection;
            _dateFilter.Filter = Settings.Default.Filter;
            _contactListBox.UseGrouping = Settings.Default.ShowGroups;

            _showActiveMenuItem.Checked = 0 != (Settings.Default.Filter & FilterSettings.Active);
            _showOverdueMenuItem.Checked = 0 != (Settings.Default.Filter & FilterSettings.Overdue);
            _showCompletedMenuItem.Checked = 0 != (Settings.Default.Filter & FilterSettings.Completed);

            _contactListBox.ItemActivate += new EventHandler(_contactListBox_ItemActivate);
            _contactListBox.EmptySetText = "-- No Matching Items --";

            SetSort(string.IsNullOrEmpty(Settings.Default.SortProperty) ? Settings.Default.SortProperty : _sortProperty, Settings.Default.SortDirection);
            SetFilter();
            SetView(Settings.Default.View);

            this.Closing += new CancelEventHandler(CallerQ_Closing);
            this.Resize += new EventHandler(Sideline_Resize);

            this.panel1.Visible = Properties.Resources.IsTouchScreen;

            if (Utils.CheckRegistration())
            {
                this._menuMenuItem.MenuItems.Remove(_registerMenuItem);
            }
        }

        void Sideline_Resize(object sender, EventArgs e)
        {
            if (Properties.Resources.IsVGA)
            {
                //
            }
            else
            {
                foreach (Control c in this.panel1.Controls)
                {
                    if (c.Width == panel1.Height)
                    {
                        c.Width = 40;
                    }
                }
                this.panel1.Height = 40;
            }
        }

        void CallerQ_Closing(object sender, CancelEventArgs e)
        {
            Settings.Save();
        }

        void _contactListBox_ItemActivate(object sender, EventArgs e)
        {
            ShowSelected();
        }

        private void _detailsMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelected();
        }

        private void ShowSelected()
        {
            if (_contactListBox.SelectedIndex >= 0 && _contactListBox.SelectedIndex < _contactListBox.FilteredItems.Count)
            {
                ShowDetails(_contactListBox.FilteredItems[_contactListBox.SelectedIndex], false);
            }
        }

        public void ShowDetails(CallRecord record, bool newRecord)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (DetailsView details = new DetailsView())
            {
                details.ShowDialog(record, newRecord);
                SetFilter();
            }
            Cursor.Current = Cursors.Default;
        }

        private class DateFilter : IEquatable<CallRecord>
        {
            public FilterSettings Filter = FilterSettings.All;

            public DateFilter(FilterSettings filter)
            {
                Filter = filter;
            }

            public bool Equals(CallRecord other)
            {
                if (0 == (Filter & FilterSettings.Overdue) && DateTime.Now > other.DueDate)
                {
                    return false;
                }
                if (0 == (Filter & FilterSettings.Active) && other.IsActive)
                {
                    return false;
                }
                if (0 == (Filter & FilterSettings.Completed) && !other.IsActive)
                {
                    return false;
                }
                return true;
            }
        }

        private void _dueDateMenuItem_Click(object sender, EventArgs e)
        {
            SetSort("DueDate");
        }

        private void _priorityMenuItem_Click(object sender, EventArgs e)
        {
            SetSort("Priority");
        }

        private void SetSort(string property)
        {
            ListSortDirection direction = (property == _sortProperty) ? (ListSortDirection.Descending ^ _sortDirection) : ListSortDirection.Ascending;
            SetSort(property, direction);
        }

        private void SetSort(string property, ListSortDirection direction)
        {
            Settings.Default.SortProperty = _sortProperty = property;
            Settings.Default.SortDirection = _sortDirection = direction;

            if (_sortProperty == "DueDate")
            {
                _contactListBox.Grouping = new DueDateGrouper();
                _dueDateMenuItem.Checked = true;
                _priorityMenuItem.Checked = false;
            }
            else if (_sortProperty == "Priority")
            {
                _contactListBox.Grouping = new PriorityGrouper();
                _dueDateMenuItem.Checked = false;
                _priorityMenuItem.Checked = true;
            }
            else
            {
                _contactListBox.Grouping = null;
            }

            _contactListBox.SortDirection = direction;
            _contactListBox.SortProperty = _sortProperty;
            _contactListBox.Refresh();
        }

        private class PriorityGrouper : IGrouper<CallRecord>
        {
            //
            #region IGrouper<CallRecord> Members

            string IGrouper<CallRecord>.GetGroupText(CallRecord o)
            {
                return string.Format("{0} Priority", o.Priority.ToString());
            }

            #endregion
        }

        private class DueDateGrouper : IGrouper<CallRecord>
        {
            #region IGrouper<CallRecord> Members

            public string GetGroupText(CallRecord o)
            {
                string dueDate = "Two Weeks or More";
                if (DateTime.Now.Date > o.DueDate.Date)
                {
                    dueDate = "Overdue";
                }
                else
                {
                    if (o.DueDate.Date == DateTime.Now.Date)
                    {
                        dueDate = "Today";
                    }
                    else if (o.DueDate.Date == DateTime.Now.AddDays(1).Date)
                    {
                        dueDate = "Tomorrow";
                    }
                    else if (o.DueDate.Date < DateTime.Now.Date.AddDays(7 - (int)DateTime.Now.Date.DayOfWeek))
                    {
                        dueDate = "This Week";
                    }
                    else if (o.DueDate.Date < DateTime.Now.Date.AddDays(15 - (int)DateTime.Now.Date.DayOfWeek))
                    {
                        dueDate = "Next Week";
                    }
                }
                return dueDate;
            }

            #endregion
        }

        private void filterMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            menuItem.Checked = !menuItem.Checked;
            SetFilter();
        }

        private void _settingsMenuItem_Click(object sender, System.EventArgs e)
        {
            using (SettingsEditor settings = new SettingsEditor())
            {
                settings.ShowDialog();
                Settings.Save();
            }
        }

        private void SetFilter()
        {
            FilterSettings settings = FilterSettings.None;
            if (_showActiveMenuItem.Checked)
            {
                settings = FilterSettings.Active | settings;
            }
            if (_showOverdueMenuItem.Checked)
            {
                settings = FilterSettings.Overdue | settings;
            }
            if (_showCompletedMenuItem.Checked)
            {
                settings = FilterSettings.Completed | settings;
            }
            Settings.Default.Filter = _dateFilter.Filter = settings;
            _contactListBox.Filter = _dateFilter;

            _contactListBox.Invalidate();
            _contactListBox.Update();
        }

        private void _addCallMenuItem_Click(object sender, EventArgs e)
        {
            using (ChooseContactDialog ccd = new ChooseContactDialog())
            {
                ccd.ChooseContactOnly = true;
                if (DialogResult.OK == ccd.ShowDialog())
                {
                    AddCall(ccd.SelectedContact);
                }
            }
        }

        public void AddCall(Contact contact)
        {
            if (!Utils.CheckRegistration() && Settings.Default.Queue.Count >= MaxItemCount)
            {
                MessageBox.Show(string.Format("You may only add up to {0} items before registering.", MaxItemCount), "Sideline", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                CallRecord record = new CallRecord(contact, string.Empty, DateTime.Now.AddDays(1), CallPriority.Normal);
                Settings.Default.Queue.Add(record);
                ShowDetails(record, true);
                _contactListBox.Items.Add(record);

                SetFilter();
            }
        }

        private void _registerMenuItem_Click(object sender, EventArgs e)
        {
            using (Register reg = new Register())
            {
                if (DialogResult.OK == reg.ShowDialog())
                {
                    // all good here
                    if (Utils.CheckRegistration())
                    {
                        this._menuMenuItem.MenuItems.Remove(_registerMenuItem);
                    }
                    else
                    {
                        this._menuMenuItem.MenuItems.Remove(_registerMenuItem);
                        this._menuMenuItem.MenuItems.Add(_registerMenuItem);
                    }
                }
            }
        }

        private void _exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _deleteCallMenuItem_Click(object sender, EventArgs e)
        {
            if (this._contactListBox.SelectedIndex >= 0 && this._contactListBox.SelectedIndex < this._contactListBox.FilteredItems.Count)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this call?", "Sideline", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    CallRecord record = this._contactListBox.FilteredItems[this._contactListBox.SelectedIndex];
                    Settings.Default.Queue.Remove(record);
                    this._contactListBox.FilteredItems.Remove(record);
                    this._contactListBox.Items.Remove(record);

                    SetFilter();
                }
            }
        }

        void _contactViewCardMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this._contactListBox.SelectedIndex >= 0 && this._contactListBox.SelectedIndex < this._contactListBox.FilteredItems.Count)
            {
                CallRecord record = this._contactListBox.FilteredItems[this._contactListBox.SelectedIndex];
                record.Contact.ShowDialog();
            }
        }

        void _contactCompleteMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this._contactListBox.SelectedIndex >= 0 && this._contactListBox.SelectedIndex < this._contactListBox.FilteredItems.Count)
            {
                CallRecord record = this._contactListBox.FilteredItems[this._contactListBox.SelectedIndex];
                record.IsActive = false;

                SetFilter();
            }
        }

        void _detailsButton_Click(object sender, System.EventArgs e)
        {
            Settings.Default.ShowGroups = _contactListBox.UseGrouping = !_contactListBox.UseGrouping;
            _contactListBox.Refresh();
        }

        private void _viewMenuItem_Click(object sender, EventArgs e)
        {
            SetView((TileSizeMode)((TaggedMenuItem)sender).Tag);
        }

        private void SetView(TileSizeMode viewMode)
        {
            foreach (TaggedMenuItem item in _viewMenuItem.MenuItems)
            {
                item.Checked = ((TileSizeMode)(item.Tag)) == viewMode;
            }

            Settings.Default.View = viewMode;
            this._contactListBox.TileSize = Settings.Default.View;
        }
    }
}