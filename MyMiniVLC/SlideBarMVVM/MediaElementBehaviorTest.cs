using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Timers;

namespace SlideBarMVVM
{
    class MediaElementBehaviorTest : Behavior<MediaElement>
    {
        private Timer _timer;

        public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached("Position", typeof(Double), typeof(MediaElementBehaviorTest), new UIPropertyMetadata(PositionPropertyChanged));
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached("Maximum", typeof(Double), typeof(MediaElementBehaviorTest), new UIPropertyMetadata(MaximumPropertyChanged));

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
            double tmp = ((MediaElementBehaviorTest)dep).AssociatedObject.Position.TotalMilliseconds - (Double)(((MediaElementBehaviorTest)dep)).GetValue(PositionProperty);

            if (tmp > 1.0 || tmp < -1.0)
                ((MediaElementBehaviorTest)dep).AssociatedObject.Position = TimeSpan.FromMilliseconds((double)ev.NewValue);
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


        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MediaOpened += new System.Windows.RoutedEventHandler(AssociatedObject_MediaOpened);
            AssociatedObject.MediaEnded += new RoutedEventHandler(AssociatedObject_MediaEnded);
            AssociatedObject.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(AssociatedObject_MediaFailed);
            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.Interval = TimeSpan.FromMilliseconds(1000).TotalMilliseconds;
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
            _timer.Stop();
            SetPosition(AssociatedObject, 0);
            SetMaximum(AssociatedObject, 0);
        }

        void AssociatedObject_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            _timer.Start();
            SetValue(MaximumProperty, AssociatedObject.NaturalDuration.TimeSpan.TotalMilliseconds);
            SetValue(PositionProperty, 0.0);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MediaOpened -= AssociatedObject_MediaOpened;
            AssociatedObject.MediaEnded -= AssociatedObject_MediaEnded;
            AssociatedObject.MediaFailed -= AssociatedObject_MediaFailed;
            _timer.Elapsed -= timer_Elapsed;
        }

    }
}
