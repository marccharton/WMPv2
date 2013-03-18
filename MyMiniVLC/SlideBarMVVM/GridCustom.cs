using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Timers;
using System.Windows;

namespace SlideBarMVVM
{
    class GridCustom : Grid
    {
        private Timer _timer;

        public GridCustom() 
        {
            this.Initialized += new EventHandler(GridCustom_Initialized);
        }

        void GridCustom_Initialized(object sender, EventArgs e)
        {
            this._timer = new Timer();
            this._timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            this._timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
            this.MouseEnter += new System.Windows.Input.MouseEventHandler(GridCustom_MouseEnter);
            this.MouseLeave += new System.Windows.Input.MouseEventHandler(GridCustom_MouseLeave);
            //this._timer.Start();
        }

        void GridCustom_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // MessageBox.Show("Au revoir");
            this._timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
            this._timer.Start();
        }

        void GridCustom_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //MessageBox.Show("YO!");
            this._timer.Stop();
            if (this.Opacity != 1.0)
                this.Opacity = 1.0;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //MessageBox.Show("coucou");
            Dispatcher.Invoke(new Action(() =>
            {
                if (this._timer.Interval == TimeSpan.FromSeconds(2).TotalMilliseconds)
                    this._timer.Interval = TimeSpan.FromMilliseconds(50).TotalMilliseconds;
                if (this.Opacity - 0.1 > 0.0)
                    this.Opacity -= 0.1;
                else
                    this._timer.Stop();
            }));
        }
    }
}
