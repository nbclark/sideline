using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MobileSrc.Sideline
{
    public partial class DetailsNotes : UserControl
    {
        private CallRecord _record = null;

        public DetailsNotes()
        {
            InitializeComponent();
        }

        public new bool Focus()
        {
            return _callNotesTextBox.Focus();
        }

        public CallRecord CallRecord
        {
            get { return _record; }
            set
            {
                _record = value;

                this._callNotesTextBox.DataBindings.Add("Text", _record, "Notes", false, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
    }
}
