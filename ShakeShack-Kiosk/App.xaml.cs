using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using static System.IO.Directory;
using System.ComponentModel;

namespace ShakeShack_Kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static int TotalTime { get; set; } = 0;
        public static bool AutoLogin { get; set; } = false;

        public static string timePath = @"C:\time.txt";
        public static string autoSavePath = @"C:\auto_save.txt";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (File.Exists(timePath))
            {
                TotalTime = Convert.ToInt32(File.ReadAllText(timePath));
            } else
            {
                File.Create(timePath).Close();
                File.WriteAllText(timePath, "0");
            }

            if (File.Exists(autoSavePath))
            {
                if (File.ReadAllText(autoSavePath) == "0")
                {
                    AutoLogin = false;
                } else
                {
                    AutoLogin = true;
                }
            } else
            {
                File.Create(autoSavePath).Close();
                File.WriteAllText(autoSavePath, "0");
            }

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            File.WriteAllText(timePath, TotalTime.ToString());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TotalTime += 1;
        }
    }
}


