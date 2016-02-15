using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.WindowsCE.Forms;

namespace MobileSrc.CallerQ
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            IntPtr hwnd = Win32.FindWindow(null, "sidelineMsg");

            // create process or send message here
            if (IntPtr.Zero != hwnd)
            {
                Message msg = new Message();
                msg.HWnd = hwnd;
                msg.Msg = 1;

                MessageWindow.PostMessage(ref msg);
            }
            else
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName), "callerq.exe"), "");
                p.Start();
            }
        }
    }
}