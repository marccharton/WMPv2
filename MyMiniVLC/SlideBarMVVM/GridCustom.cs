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

        void ResetTime() 
        {
            this._timer.Stop();
            this.Opacity = 1.0;
            this._timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
            this._timer.Start();
        }

        void GridCustom_Initialized(object sender, EventArgs e)
        {
            this.DragOver += new DragEventHandler(GridCustom_DragOver);
            this._timer = new Timer();
            this._timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            this._timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
            this.MouseMove += new System.Windows.Input.MouseEventHandler(GridCustom_MouseEnter);
            this.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(GridCustom_MouseDown);
            this.MouseUp += new System.Windows.Input.MouseButtonEventHandler(GridCustom_MouseUp);
            this.ResetTime();
        }

        void GridCustom_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ResetTime();
        }

        void GridCustom_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ResetTime();
            this._timer.Stop();
        }


        void GridCustom_DragOver(object sender, DragEventArgs e)
        {
            this.ResetTime();
        }

        void GridCustom_DragEnter(object sender, DragEventArgs e)
        {
            this.ResetTime();
        }

        void GridCustom_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.ResetTime();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
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
