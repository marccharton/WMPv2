using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace SlideBarMVVM
{
    class TextBlockCustomEffect
    {

        private static TextBlockCustomEffect    _textBCE = null;
        public static TextBlockCustomEffect getInstance()
        {
            if (_textBCE == null)
                _textBCE = new TextBlockCustomEffect();
            return (_textBCE);
        }

        private Timer _timer;
        private int _textBlockCount;
        private int _maxLengthContent;
        private List<int> _size;
        private Thickness _thickness;
        private bool _left;

        public Thickness Thick
        {
            get { return (this._thickness); } 
        }
        public EventHandler EventTime;
        public Dispatcher Dispatch { get; set; }

        public TextBlockCustomEffect() 
        {
            _timer = new Timer();
            _timer.Interval = TimeSpan.FromMilliseconds(20).TotalMilliseconds;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _textBlockCount = 0;
            _maxLengthContent = 0;
            _thickness = new Thickness(0, 0, 0, 0);
            _left = true;
            _size = new List<int>();
            this.Dispatch = Dispatcher.CurrentDispatcher;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           Dispatch.BeginInvoke(new Action(() =>
            {
                if (_left)
                {
                    if (this._maxLengthContent * 2 > -this._thickness.Left)
                        _thickness.Left--;
                    else
                    {
                        _left = false;
                        _thickness.Left++;
                    }
                }
                else 
                {
                    if (this._thickness.Left < 0)
                        _thickness.Left++;
                    else 
                    {
                        _left = true;
                        _thickness.Left--;
                    }
                }
                this.EventTime(this, null);
            }));
        }

        public void addElement(int length) 
        {
            _size.Add(length);
            if (length > _maxLengthContent)
                _maxLengthContent = length;
            _textBlockCount++;
            if (_maxLengthContent > 35)
                _timer.Start();
        }

        public void removeElement(int length) 
        {
            _timer.Stop();
            _size.Remove(length);
            _maxLengthContent = 0;
            foreach (var item in _size)
            {
                if (item > _maxLengthContent)
                    _maxLengthContent = item;
            }
            _textBlockCount--;
            if (_maxLengthContent > 35)
                _timer.Start();
            else
            {
                this._thickness.Left = 0;
                this.EventTime(this, null);
            }
        }

    }
}
