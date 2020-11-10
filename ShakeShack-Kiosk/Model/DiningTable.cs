using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ShakeShack_Kiosk.Model
{

    static class Constants
    {
        public const int EXPIRE_SECOND = 60;
    }

    class DiningTable : INotifyPropertyChanged
    {
        public int Number { get; set; }
        private DateTime paidAt { get; set; }
        public DateTime PaidAt { 
            get => paidAt;
            set
            {
                paidAt = value;
                OnPropertyChanged("PaidAt");
                ExpireAt = PaidAt.AddSeconds(Constants.EXPIRE_SECOND);
                RemainSeconds = Constants.EXPIRE_SECOND;
                IsUsing = true;
                SetRemainTimerEvent();
            }
        }
        private DateTime expireAt { get; set; }
        public DateTime ExpireAt {
            get => expireAt;
            set
            {
                expireAt = value;
                OnPropertyChanged("ExpireAt");
            }
        }
        private int remainSeconds { get; set; }
        public int RemainSeconds
        {
            get => remainSeconds;
            set
            {
                remainSeconds = value;
                OnPropertyChanged("RemainSeconds");
            }
        }
        private bool isUsing { get; set; } = false;
        public bool IsUsing 
        {
            get => isUsing;
            set
            {
                isUsing = value;
                OnPropertyChanged("IsUsing");
            }
        }

        private void SetRemainTimerEvent()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler((object sender, EventArgs e) => {
                if (RemainSeconds <= 0)
                {
                    timer.Stop();
                    IsUsing = false;
                }
                RemainSeconds -= 1;
            });
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
