using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using Microsoft.WindowsMobile.PocketOutlook;

namespace MobileSrc.Sideline
{
    static class Program
    {
        static int _oid = 0;
        public static Sideline _callerQ = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            if (Settings.Default.FirstRun)
            {
                if (!Utils.CheckRegistration())
                {
                    using (Register reg = new Register())
                    {
                        if (DialogResult.OK == reg.ShowDialog())
                        {
                            // all good here
                        }
                    }
                }
                Settings.Default.FirstRun = false;
            }
            _callerQ = new Sideline();

            IntPtr hwnd = Win32.FindWindow(null, "sidelineMsg");
            Win32.SetSystemPowerState(null, Win32.POWER_STATE_ON, Win32.POWER_FORCE);
            using (CustomMessageWindow window = new CustomMessageWindow())
            {
                window.Text = "sidelineMsg";
                _callerQ.Load += new EventHandler(_callerQ_Load_Notify);
                
                for (int i = 0; i < args.Length; i+=2)
                {
                    string arg = args[i].ToLower();

                    if (args.Length > i+1 && arg.Length > 1 && (arg[0] == '-' || arg[0] == '/'))
                    {
                        string command = arg.Substring(1);
                        string value = args[i + 1];

                        switch (command)
                        {
                            case "oid":
                                {
                                    _oid = Convert.ToInt32(value);
                                    _callerQ.Load += new EventHandler(_callerQ_Load_Oid);
                                }
                                break;
                            //case "notify":
                            //    {
                            //        _callerQ.Load += new EventHandler(_callerQ_Load_Notify);
                            //    }
                            //    break;
                        }
                    }
                }

                Application.Run(_callerQ);
                SetNotification();
            }
        }

        static void _callerQ_Load_Notify(object sender, EventArgs e)
        {
            SortableList<CallRecord> overdueItems = new SortableList<CallRecord>();

            foreach (CallRecord record in Settings.Default.Queue)
            {
                if (record.NotifyDate < DateTime.Now.AddMinutes(2) && !record.WasNotified && record.IsActive)
                {
                    overdueItems.Add(record);
                }
            }

            if (overdueItems.Count > 0)
            {
                overdueItems.Sort("NotifyDate", System.ComponentModel.ListSortDirection.Ascending);

                foreach (CallRecord record in overdueItems)
                {
                    using (Notifier notifier = new Notifier())
                    {
                        notifier.ShowDialog(record);
                    }
                }
            }
            Settings.Save();
            SetNotification();
        }

        static void _callerQ_Load_Oid(object sender, EventArgs e)
        {
            if (_oid != 0)
            {
                try
                {
                    ItemId id = new ItemId(_oid);
                    _callerQ.AddCall(new Contact(id));
                    _callerQ.BringToFront();
                }
                catch
                {
                }
            }
        }

        public static void SetNotification()
        {
            DateTime minDate = DateTime.MaxValue;

            foreach (CallRecord record in Settings.Default.Queue)
            {
                if (record.NotifyDate > DateTime.MinValue && !record.WasNotified && record.IsActive)
                {
                    if (record.NotifyDate < minDate)
                    {
                        minDate = record.NotifyDate;
                    }
                }
            }

            if (minDate != DateTime.MaxValue)
            {
                if (minDate < DateTime.Now)
                {
                    minDate = DateTime.Now.AddMinutes(1);
                }
                SetNotification(minDate);
            }
            else
            {
                SetNotification(null);
            }
        }

        public static void SetNotification(DateTime launchDate)
        {
            SetNotification(new SystemTime(launchDate).ToByteArray());
        }

        public static void SetNotification(byte[] date)
        {
            Win32.CeRunAppAtTime(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName), "sidelinenotify.exe"), date);
        }

        private class CustomMessageWindow : MessageWindow
        {
            protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message m)
            {
                // Check for notification message here

                if (m.Msg == 0x401)
                {
                    _oid = m.LParam.ToInt32();
                    m.Result = IntPtr.Zero;

                    _callerQ_Load_Oid(_callerQ, null);
                    return;
                }
                else if (m.Msg == 0x402)
                {
                    m.Result = IntPtr.Zero;

                    _callerQ_Load_Notify(_callerQ, null);
                    return;
                }

                base.WndProc(ref m);
            }
        }
    }
}