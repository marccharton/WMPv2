using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Timers;
using System.Windows.Interactivity;

namespace SlideBarMVVM
{
    public class SlideBarViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand commandUp { get; set; }
        public ICommand commandDown { get; set; }

        private Timer _timer;

        private Double _value;
        public Double Value { 
            get 
            {
                return (this._value);
            } 
            set 
            {
                if (this._value != value && value <= this._maximum && value >= this._minimum) 
                {
                    this._value = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        private Double _maximum;
        public Double Maximum
        {
            get
            {
                return (this._maximum);
            }
            set
            {
                if (this._maximum != value)
                {
                    this._maximum = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Maximum"));
                }
            }
        }

        private Double _minimum;
        public Double Minimum
        {
            get
            {
                return (this._minimum);
            }
            set
            {
                if (this._minimum != value)
                {
                    this._minimum = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Minimum"));
                }
            }
        }

        public SlideBarViewModel() 
        {
            //MessageBox.Show("lalalal");

            this.Maximum = 1000;
            this.Minimum = 0;

            this._timer = new Timer();
            this._timer.Interval = TimeSpan.FromMilliseconds(100).TotalMilliseconds;
            this._timer.Elapsed += new ElapsedEventHandler(timerElapsed);
            //this._timer.Start();

            this.commandUp = new Command((() =>
            {
                this.Value++;
            }));

            this.commandDown = new Command((() => {
                this.Value--;
            }));
        }

        void timerElapsed(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (this.Value <= this.Maximum)
                    this.Value++;
                else
                    this.Value--;
            }));
        }
        
    }
}
