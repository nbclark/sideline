using System;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.Telephony;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MobileSrc.Sideline
{
    public partial class DetailsGeneralLanscape : UserControl
    {
        private CallRecord _record = null;
        private SystemState _activeCallState = null;
        public event EventHandler CallStarted;
        public static Bitmap[][] ColorImages;

        static DetailsGeneralLanscape()
        {
            ColorImages = new Bitmap[][]
                {
                    new Bitmap[] { Properties.Resources.button_blue, Properties.Resources.button_blue_hover },
                    new Bitmap[] { Properties.Resources.button_black, Properties.Resources.button_black_hover },
                    new Bitmap[] { Properties.Resources.button_grey, Properties.Resources.button_grey_hover }
                };
        }

        public DetailsGeneralLanscape()
        {
            InitializeComponent();
            _activeCallState = new SystemState(SystemProperty.PhoneActiveCallCount);

            this._alarmPictureBox.Image = Properties.Resources.notify_large;
            this._alarmPictureBox.PushImage = Properties.Resources.notify_large_hover;
            this._commonPhrasesPictureBox.Image = Properties.Resources.commonphrases;
            this._commonPhrasesPictureBox.PushImage = Properties.Resources.commonphrases_hover;

            this.Disposed += new EventHandler(DetailsGeneral_Disposed);
        }

        public new bool Focus()
        {
            return _dueDateDateTimePicker.Focus();
        }

        void DetailsGeneral_Disposed(object sender, EventArgs e)
        {
            _activeCallState.Dispose();
        }

        public CallRecord CallRecord
        {
            get { return _record; }
            set
            {
                _record = value;

                this._contactPictureBox.Image = _record.GetImage(this._contactPictureBox.Size);

                if (null == this._contactPictureBox.Image)
                {
                    this._contactPictureBox.Image = Properties.Resources.user;
                    this._contactPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                this._nameLabel.DataBindings.Add("Text", _record, "Name");
                this._callReasonTextBox.DataBindings.Add("Text", _record, "Description", false, DataSourceUpdateMode.OnPropertyChanged);
                this._dueDateDateTimePicker.DataBindings.Add("Value", _record, "DueDate", true, DataSourceUpdateMode.OnPropertyChanged);//, string.Empty, "Due: ddd, MMMM dd");
                this._dueDateDateTimePicker.Format = DateTimePickerFormat.Custom;
                this._dueDateDateTimePicker.CustomFormat = "dddd, MMMM dd";

                List<KVP<string, CallPriority>> items = new List<KVP<string, CallPriority>>();

                foreach (CallPriority cp in new CallPriority[] { CallPriority.Normal, CallPriority.High })
                {
                    items.Add(new KVP<string, CallPriority>(string.Format("{0} Priority", cp), cp));

                    if (cp == _record.Priority)
                    {
                        //_priorityComboBox.SelectedIndex = _priorityComboBox.Items.Count - 1;
                    }
                }

                _priorityComboBox.DataSource = items;
                _priorityComboBox.DisplayMember = "Key";
                _priorityComboBox.ValueMember = "Value";

                this._priorityComboBox.DataBindings.Add("SelectedValue", _record, "Priority", true, DataSourceUpdateMode.OnPropertyChanged).ReadValue();

                Dictionary<string, string> phoneNumbers = new Dictionary<string,string>();

                if (!string.IsNullOrEmpty(_record.Contact.BusinessTelephoneNumber))
                {
                    phoneNumbers.Add("Business", _record.Contact.BusinessTelephoneNumber);
                }
                if (!string.IsNullOrEmpty(_record.Contact.HomeTelephoneNumber))
                {
                    phoneNumbers.Add("Home", _record.Contact.HomeTelephoneNumber);
                }
                if (!string.IsNullOrEmpty(_record.Contact.MobileTelephoneNumber))
                {
                    phoneNumbers.Add("Mobile", _record.Contact.MobileTelephoneNumber);
                }

                int tabIndex = _callReasonTextBox.TabIndex + phoneNumbers.Count;

                int index = phoneNumbers.Count - 1;
                int buttonHeight = ColorImages[0][0].Height;
                int spacing = (Properties.Resources.IsVGA ? 7 : 7);
                int totalHeight = spacing + buttonHeight;

                int textBoxHeight = Math.Max(this.Height / 7, (this.Bottom - (totalHeight * (phoneNumbers.Count))) - _callReasonTextBox.Top);
                _callReasonTextBox.Size = new Size(_callReasonTextBox.Width, textBoxHeight);

                buttonHeight = Math.Min(totalHeight, (this.Bottom - _callReasonTextBox.Bottom - spacing) / (phoneNumbers.Count));
                int y = _callReasonTextBox.Bottom + (buttonHeight * (phoneNumbers.Count));

                foreach (string phoneType in phoneNumbers.Keys)
                {
                    ImageButton button = new ImageButton();
                    button.Font = new Font(this.Font.Name, (int)Math.Ceiling(((double)buttonHeight / totalHeight) * 12), FontStyle.Bold);
                    button.BackColor = SystemColors.Window;
                    button.Text = string.Format("{0}: {1}", phoneType, phoneNumbers[phoneType]);
                    button.Tag = phoneNumbers[phoneType];
                    button.Click += new EventHandler(button_Click);

                    button.BorderColor = Color.Black;
                    button.BorderWidth = 2;

                    button.Image = ColorImages[index % ColorImages.Length][0];
                    button.PushImage = ColorImages[index % ColorImages.Length][1];
                    button.IsTransparent = false;

                    button.Size = new Size(this.Width, buttonHeight-spacing);
                    button.Location = new Point(0, y - button.Size.Height);
                    button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    button.TabIndex = tabIndex--;

                    button.ForeColor = Color.White;

                    y = button.Location.Y - spacing;

                    this.Controls.Add(button);
                    index--;
                }
                //_callReasonTextBox.Size = new Size(_callReasonTextBox.Width, y - _callReasonTextBox.Top);
            }
        }

        void binding_Format(object sender, ConvertEventArgs e)
        {
            e.Value = string.Format("{0} Priority", e.Value);
        }

        private bool _inCall = false;

        void button_Click(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            string phoneNum = button.Tag as string;

            try
            {
                _activeCallState.Changed -= new ChangeEventHandler(_activeCallState_Changed);
            }
            catch
            {
            }
            _activeCallState.Changed += new ChangeEventHandler(_activeCallState_Changed);

            Phone phone = new Phone();
            phone.Talk(phoneNum, true);

            if (null != CallStarted)
            {
                CallStarted(this, null);
            }
        }

        void _activeCallState_Changed(object sender, ChangeEventArgs args)
        {
            if (Convert.ToInt32(args.NewValue) > 0)
            {
                _inCall = true;
            }
            else
            {
                if (_inCall)
                {
                    // we made our call and are done
                    this.Focus();
                    if (DialogResult.Yes == MessageBox.Show("Your call has been completed. Would you like to mark the record as completed?", "Sideline", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        _record.IsActive = false;
                        
                        Control c = this.Parent;
                        while (!(c is Form))
                        {
                            c = c.Parent;
                        }
                        ((Form)c).Close();
                    }
                }
                _inCall = false;
                _activeCallState.Changed -= new ChangeEventHandler(_activeCallState_Changed);
            }
        }

        private void _dueDateLinkLabel_Click(object sender, EventArgs e)
        {
            using (ControlEditor te = new ControlEditor())
            {
                using (DateTimePicker tb = new DateTimePicker())
                {
                    tb.Value = this.CallRecord.DueDate;
                    if (DialogResult.OK == te.ShowDialog("Edit Due Date", tb))
                    {
                        this.CallRecord.DueDate = tb.Value;
                    }
                }
            }
        }

        private void _alarmPictureBox_Click(object sender, EventArgs e)
        {
            using (DetailsReminder reminder = new DetailsReminder())
            {
                reminder.SetRecord(_record);

                using (ControlEditor ce = new ControlEditor())
                {
                    ce.ShowDialog("Reminder", reminder);
                }
            }
        }

        private void _commonPhrasesPictureBox_Click(object sender, EventArgs e)
        {
            using (ControlEditor ce = new ControlEditor())
            {
                UserControl uc = new UserControl();
                uc.Dock = DockStyle.Fill;

                ImageButton ib = new ImageButton();
                ib.IsTransparent = false;
                ib.Image = Properties.Resources.add_small;
                ib.PushImage = Properties.Resources.add_small_hover;
                ib.Size = new Size(ib.Image.Height, ib.Image.Height);
                ib.Dock = DockStyle.Right;
                ib.Click += new EventHandler(ib_Click);

                TextBox tb = new TextBox();
                tb.Height = ib.Image.Height;
                tb.BorderStyle = BorderStyle.None;
                tb.Dock = DockStyle.Top;

                Panel panel = new Panel();
                panel.Height = ib.Image.Height;
                panel.BackColor = SystemColors.Window;
                panel.Dock = DockStyle.Top;

                panel.Controls.Add(tb);
                panel.Controls.Add(ib);

                ListBox lb = new ListBox();
                lb.Dock = DockStyle.Fill;
                lb.KeyDown += new KeyEventHandler(lb_KeyDown);

                uc.Controls.Add(lb);
                uc.Controls.Add(panel);

                XmlSerializer xs = new XmlSerializer(typeof(List<string>));
                string file = Settings.GetSettingsPath("phrases.xml");

                if (!File.Exists(file))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(file));
                }
                else
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        List<string> list = xs.Deserialize(fs) as List<string>;

                        if (list != null)
                        {
                            foreach (string str in list)
                            {
                                lb.Items.Add(str);
                            }
                        }
                    }
                }


                if (DialogResult.OK == ce.ShowDialog("Common Phrases", uc))
                {
                    this._callReasonTextBox.Text = lb.Text;

                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    using (StreamWriter sw = File.CreateText(file))
                    {
                        xs.Serialize(sw, lb.Items);
                    }
                }
            }
        }

        void lb_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox lb = (ListBox)sender;

            if (e.KeyCode == Keys.Delete)
            {
                if (lb.SelectedIndex > -1)
                {
                    lb.Items.RemoveAt(lb.SelectedIndex);
                }
            }
        }

        void ib_Click(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            TextBox tb = button.Parent.Controls[0] as TextBox;
            ListBox lb = button.Parent.Parent.Controls[0] as ListBox;

            lb.SelectedIndex = lb.Items.Add(tb.Text);
        }
    }
}
