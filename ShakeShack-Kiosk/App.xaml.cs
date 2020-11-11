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
        Stopwatch watch = new Stopwatch();
        int now = 0;
        static String path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Config/test.txt";
        // String textValue = System.IO.File.ReadAllText(path);


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            watch.Start();
            Timer.Start();
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            
            FileInfo fileInfo = new FileInfo(path);


            if (fileInfo.Exists)
            {
                System.IO.File.WriteAllText(path, time.ToString());
            }
            else
            {
                System.IO.File.Create(path);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            now++;
            time = now + int.Parse("1");

        }
    }
}


