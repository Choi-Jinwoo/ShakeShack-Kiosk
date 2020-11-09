using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ShakeShack_Kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static String time;
        Stopwatch watch = new Stopwatch();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            watch.Start();
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan now = watch.Elapsed;
            time = String.Format("{0:00}:{1:00}:{2:00}", now.Hours, now.Minutes, now.Seconds);
        }
    }
}
