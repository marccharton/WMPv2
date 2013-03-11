using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Timers;
using System.Windows.Media;

namespace SlideBarMVVM
{
    class MediaElementBehavior : Behavior<MediaElement>
    {
        private Timer _timer;
        private Boolean _fullScreen;

        public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached("Position", typeof(Double), typeof(MediaElementBehavior), new UIPropertyMetadata(PositionPropertyChanged));
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached("Maximum", typeof(Double), typeof(MediaElementBehavior), new UIPropertyMetadata(MaximumPropertyChanged));
        public static readonly DependencyProperty VolumeProperty = DependencyProperty.RegisterAttached("Volume", typeof(Double), typeof(MediaElementBehavior), new UIPropertyMetadata(VolumePropertyChanged));

        public static Double GetPosition(MediaElement m)
        {
            return (Double)(m.GetValue(PositionProperty));
        }

        public static void SetPosition(MediaElement m, Double d)
        {
            m.SetValue(PositionProperty, d);
        }

        public static void PositionPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
            double tmp = ((MediaElementBehavior)dep).AssociatedObject.Position.TotalMilliseconds - (Double)(((MediaElementBehavior)dep)).GetValue(PositionProperty);

            if (tmp > 1.0 || tmp < -1.0)
                ((MediaElementBehavior)dep).AssociatedObject.Position = TimeSpan.FromMilliseconds((double)ev.NewValue);
        }

        public static Double GetMaximum(MediaElement m)
        {
            return (Double)(m.GetValue(MaximumProperty));
        }

        public static void SetMaximum(MediaElement m, Double d)
        {
            m.SetValue(MaximumProperty, d);
        }

        public static void MaximumPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
        }

        public static Double GetVolume(MediaElement m)
        {
            return (Double)(m.GetValue(VolumeProperty));
        }

        public static void SetVolume(MediaElement m, Double d)
        {
            m.SetValue(VolumeProperty, d);
        }

        public static void VolumePropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
           // MessageBox.Show("Nouvelle value: " + ev.NewValue.ToString() + "Et ancienne value: " + ev.OldValue.ToString());
           // MessageBox.Show(((MediaElementBehaviorTest)dep).AssociatedObject.Balance.ToString());
            ((MediaElementBehavior)dep).AssociatedObject.Volume = (double)ev.NewValue;
          //  MessageBox.Show(((MediaElementBehaviorTest)dep).AssociatedObject.Balance.ToString());
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MediaOpened += new System.Windows.RoutedEventHandler(AssociatedObject_MediaOpened);
            AssociatedObject.MediaEnded += new RoutedEventHandler(AssociatedObject_MediaEnded);
            AssociatedObject.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(AssociatedObject_MediaFailed);
            AssociatedObject.MouseDown += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.Interval = TimeSpan.FromMilliseconds(1000).TotalMilliseconds;
            SetValue(VolumeProperty, 0.5);
            _fullScreen = false;
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            string []files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string s in files) 
            {
                MessageBox.Show(s);
            }
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window w = App.Current.MainWindow;
            if (e.ClickCount == 2)
            {
                if (!_fullScreen)
                {
                    w.WindowStyle = WindowStyle.None;
                    w.WindowState = WindowState.Maximized;
                    _fullScreen = true;
                }
                else
                {
                    w.WindowStyle = WindowStyle.SingleBorderWindow;
                    w.WindowState = WindowState.Normal;
                    _fullScreen = false;
                }
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetValue(PositionProperty, AssociatedObject.Position.TotalMilliseconds);
            }));
        }

        void AssociatedObject_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //MessageBox.Show("FAILED LE MEDIA! FAILED");
            //SetValue(PlaystateProperty, PlayerState.Stop);
            //AssociatedObject.Close();
        }

        void AssociatedObject_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.NaturalDuration.HasTimeSpan)
            {
                _timer.Stop();
                SetPosition(AssociatedObject, 0);
                SetMaximum(AssociatedObject, 0);
            }
        }

        void AssociatedObject_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            AssociatedObject.IsMuted = true;
            AssociatedObject.IsMuted = false;

            //   MessageBox.Show(GetValue(VolumeProperty).ToString() + ";" + AssociatedObject.Volume.ToString());
            _timer.Stop();
            if (AssociatedObject.NaturalDuration.HasTimeSpan)
            {
                _timer.Start();
                SetValue(MaximumProperty, AssociatedObject.NaturalDuration.TimeSpan.TotalMilliseconds);
                SetValue(PositionProperty, 0.0);
                AssociatedObject.Volume = (double)GetValue(VolumeProperty);
            }
            else
            {
                SetValue(MaximumProperty, 0.0);
                SetValue(PositionProperty, 0.0);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MediaOpened -= AssociatedObject_MediaOpened;
            AssociatedObject.MediaEnded -= AssociatedObject_MediaEnded;
            AssociatedObject.MediaFailed -= AssociatedObject_MediaFailed;
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.Drop -= AssociatedObject_Drop;
            _timer.Elapsed -= timer_Elapsed;
        }
    }
}
