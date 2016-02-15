using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Drawing;
using Microsoft.WindowsMobile.PocketOutlook;

namespace MobileSrc.Sideline
{
    public enum FilterSettings : int
    {
        None = 0,
        Active = 1,
        Overdue = 2,
        Completed = 4,
        All = -1
    }

    public enum CallPriority : short
    {
        Normal = 0,
        High = 1
    }

    public class Settings
    {
        private static Settings _instance = null;
        private static XmlSerializer _serializer = new XmlSerializer(typeof(Settings));

        public Settings()
        {
            this.SortProperty = "DueDate";
            this.SortDirection = ListSortDirection.Ascending;
            this.Filter = FilterSettings.Active | FilterSettings.Overdue;
            this.RemindersEnabled = true;
            this.IntegrationEnabled = true;
            this.SwitchToNotesEnabled = true;
            this.DefaultReminderDay = 0;
            this.DefaultReminderTime = 8 * 2;
            this.Queue = new SidelineQueue();
            this.View = TileSizeMode.Small;
            this.ShowGroups = true;
            this.FirstRun = true;
        }

        private static string SettingsPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"mobileSRC\Sideline\settings.xml");
            }
        }

        public static string GetSettingsPath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), string.Concat(@"mobileSRC\Sideline\", filename));
        }

        public static Settings Default
        {
            get
            {
                if (_instance == null)
                {
                    if (!File.Exists(SettingsPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                        _instance = new Settings();
                    }
                    else
                    {
                        using (FileStream fs = File.OpenRead(SettingsPath))
                        {
                            _instance = (Settings)_serializer.Deserialize(fs);
                        }
                    }
                }
                return _instance;
            }
        }

        public static void Save()
        {
            if (File.Exists(SettingsPath))
            {
                File.Delete(SettingsPath);
            }
            using (FileStream fs = File.OpenWrite(SettingsPath))
            {
                _serializer.Serialize(fs, Default);
            }
        }

        public bool FirstRun
        {
            get;
            set;
        }
        public string SortProperty
        {
            get;
            set;
        }
        public ListSortDirection SortDirection
        {
            get;
            set;
        }
        [XmlIgnore]
        public FilterSettings Filter
        {
            get { return (FilterSettings)this.FilterInt; }
            set { this.FilterInt = (int)value; }
        }
        public int FilterInt
        {
            get;
            set;
        }
        public bool ShowGroups
        {
            get;
            set;
        }
        public TileSizeMode View
        {
            get;
            set;
        }
        public SidelineQueue Queue
        {
            get;
            set;
        }
        public int DefaultReminderDay
        {
            get;
            set;
        }
        public int DefaultReminderTime
        {
            get;
            set;
        }
        public bool RemindersEnabled
        {
            get;
            set;
        }
        public bool IntegrationEnabled
        {
            get
            {
                return (0 != Utils.GetOption("IntegrationEnabled", 1));
            }
            set
            {
                Utils.SetOption("IntegrationEnabled", value ? 1 : 0);
            }
        }
        public bool SwitchToNotesEnabled
        {
            get;
            set;
        }
    }

    public class SidelineQueue : SortableList<CallRecord>
    {
        public SidelineQueue()
        {
            //
        }
    }

    public class CallRecord
    {
        private Image _image = null;
        private Size _size = Size.Empty;
        private Contact _contact = null;

        public CallRecord()
        {
            //
        }
        CallRecord(string description, DateTime dueDate, CallPriority priority)
        {
            this.Description = description;
            this.DueDate = dueDate;
            this.NotifyDate = dueDate.Date.AddDays(Settings.Default.DefaultReminderDay).AddMinutes(Settings.Default.DefaultReminderTime);
            this.Priority = priority;
            this.Notes = string.Empty;
            this.IsActive = true;
            this.WasNotified = !Settings.Default.RemindersEnabled;
        }
        public CallRecord(Contact contact, string description, DateTime dueDate, CallPriority priority)
            : this(description, dueDate, priority)
        {
            this.Name = contact.FileAs;
            this.OID = Convert.ToInt32(contact.ItemId.ToString());
            this.Contact = contact;
        }
        public int OID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Notes
        {
            get;
            set;
        }
        public DateTime DueDate
        {
            get;
            set;
        }
        public bool WasNotified
        {
            get;
            set;
        }
        public DateTime NotifyDate
        {
            get;
            set;
        }
        public CallPriority Priority
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        [XmlIgnore()]
        public Contact Contact
        {
            get
            {
                if (null == _contact && this.OID != 0)
                {
                    try
                    {
                        _contact = new Contact(new ItemId(this.OID));
                    }
                    catch
                    {
                        _contact = null;
                    }
                }
                return _contact;
            }
            set
            {
                _contact = value;
            }
        }

        public Image GetImage(Size size)
        {
            if (null != Contact)
            {
                if (Size.Empty == _size || _size != size)
                {
                    if (_image != null)
                    {
                        _image.Dispose();
                    }
                    _size = new Size(size.Width, size.Height);
                    _image = GetImage(Contact.Picture, size);
                }
            }
            return _image;
        }

        public static Image GetImage(Image picture, Size size)
        {
            Image img = null;
            if (null != (img = picture))
            {
                Size imgSize = ScaleProportional(size.Width, size.Height, picture);

                Bitmap bmp = new Bitmap(imgSize.Width, imgSize.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(img, new Rectangle(0, 0, imgSize.Width, imgSize.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                }
                img = bmp;
            }
            return img;
        }

        static  int MulDiv(int a, int b, int c)
        {
            return (a * b) / c;
        }

        static Size ScaleProportional(int uFitToWidth, int uFitToHeight, Image image)
        {
            // Scale (*puWidthToScale, *puHeightToScale) to fit within (uFitToWidth, uFitToHeight), while
            // maintaining the aspect ratio
            int nScaledWidth = MulDiv(image.Width, uFitToHeight, image.Height);
            int uWidthToScale, uHeightToScale;

            // If we didn't overflow and the scaled width does not exceed bounds
            if (nScaledWidth >= 0 && nScaledWidth <= (int)uFitToWidth)
            {
                uWidthToScale = nScaledWidth;
                uHeightToScale = uFitToHeight;
            }
            else
            {
                uHeightToScale = MulDiv(image.Height, uFitToWidth, image.Width);
                uWidthToScale = uFitToWidth;
            }

            return new Size(uWidthToScale, uHeightToScale);
        }
    }

    public class SortableList<T> : List<T>
    {
        public ListChangedEventHandler ListChanged;

        public void Sort(string property, ListSortDirection direction)
        {
            base.Sort(new SortComparer(property, direction));
        }

        public new void Add(T item)
        {
            base.Add(item);

            if (null != ListChanged)
            {
                ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, -1));
            }
        }

        public new void Remove(T item)
        {
            base.Remove(item);

            if (null != ListChanged)
            {
                ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, -1));
            }
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);

            if (null != ListChanged)
            {
                ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, -1));
            }
        }

        protected class SortComparer : Comparer<T>
        {
            private PropertyInfo _propertyInfo;
            private ListSortDirection _direction;

            public SortComparer(string property, ListSortDirection direction)
            {
                _propertyInfo = typeof(T).GetProperty(property);
                _direction = direction;
            }

            public override int Compare(T x, T y)
            {
                object xObj = _propertyInfo.GetValue(x, null);
                object yObj = _propertyInfo.GetValue(y, null);

                int result = 0;
                if (xObj is IComparable)
                {
                    IComparable xComp = (IComparable)xObj;
                    IComparable yComp = (IComparable)yObj;

                    result = xComp.CompareTo(yComp);
                }
                if (_direction == ListSortDirection.Descending)
                {
                    result = -result;
                }

                return result;
            }
        }
    }
}