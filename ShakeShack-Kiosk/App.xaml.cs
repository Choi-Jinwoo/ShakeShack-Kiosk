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

namespace ShakeShack_Kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static int time;
        int now = 0;
        static String path = AppDomain.CurrentDomain.BaseDirectory + @"\time.txt";
        String textValue = "";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                textValue = System.IO.File.ReadAllText(path);
            } else
            {
                //CreateDirectory(FolderPath);
                System.IO.File.Create(path);
            }

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            this.Exit += App_Exit;
            
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                File.WriteAllText(path, time.ToString());
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            now++;
<<<<<<< HEAD
            time = now + int.Parse("1");
=======
            if (textValue == "")
            {
                time = now;
            }
            else
            {
                time = now + int.Parse(textValue);
            }
>>>>>>> 98ec37657ce55f1b0250157745d6d2664bdb09a5

        }
    }
}


