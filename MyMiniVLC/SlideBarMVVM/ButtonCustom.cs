using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Timers;
using System.Windows;

namespace SlideBarMVVM
{
    public class ButtonCustom : Button
    {
        private Timer _timer;
        private Boolean _passed;
        private Command commandTest;
        private Double _speedIncrease;

        public ButtonCustom() 
        {
            base.Initialized += new EventHandler(ButtonCustom_Initialized);
            this.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ButtonCustom_PreviewMouseLeftButtonDown);
            this.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(ButtonCustom_PreviewMouseLeftButtonUp);
            this._timer = new Timer();
            this._timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            this._timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            this._passed = false;
        }

        void ButtonCustom_Initialized(object sender, EventArgs e)
        {
            if ((String)this.Content == ">>")
            {
                this.commandTest = new Command(new Action(() =>
                {
                    CurrentList curList = CurrentList.getInstance();
                    if (curList.getSize() > 0)
                    {
                        CurrentList.getInstance().setIsPlaying(false, CurrentList.getInstance().getCurrentElementIdx());
                        curList.moveToNextElement();
                        curList.ChangedEvent(this, null);
                    }
                }));
                this._speedIncrease = 2.0;
            }
            else 
            {
                this.commandTest = new Command(new Action(() =>
                {
                    CurrentList curList = CurrentList.getInstance();
                    if (curList.getSize() > 0)
                    {
                        CurrentList.getInstance().setIsPlaying(false, CurrentList.getInstance().getCurrentElementIdx());
                        curList.moveToPrevElement();
                        curList.ChangedEvent(this, null);
                    }
                }));
                this._speedIncrease = (-0.1);
            }
        }

        void ButtonCustom_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._timer.Stop();
            if (!this._passed)
                this.commandTest.Execute(this);
            else
                CurrentList.getInstance().Speed = 1.0;   
            this._passed = false;
        }

        void ButtonCustom_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._passed = false;
            this._timer.Start();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {        
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (CurrentList.getInstance().Speed + this._speedIncrease > 0.1)
                    CurrentList.getInstance().Speed += this._speedIncrease;
                this._passed = true;
            }));
        }
    }
}
