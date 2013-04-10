using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace SlideBarMVVM
{
    public class MediaElementCustom : MediaElement
    {
        public static readonly DependencyProperty PlayCustomProperty = DependencyProperty.RegisterAttached("PlayCustom", typeof(PlayerState), typeof(MediaElementCustom), new UIPropertyMetadata(PlayerState.None, PlayPropertyChanged));

        public PlayerState PlayCustom
        {
            get { return ((PlayerState)GetValue(PlayCustomProperty)); }
            set { SetValue(PlayCustomProperty, value); }
        }

        public static void PlayPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
            MediaElementCustom mec = dep as MediaElementCustom;
            PlayerState ps = (PlayerState)ev.NewValue;

            if (mec != null)
            {
                if (ps == PlayerState.Play)
                    mec.Play();
                else if (ps == PlayerState.Pause)
                    mec.Pause();
                else if (ps == PlayerState.Stop)
                {
                    mec.Stop();
                    mec.Close();
                    mec.Position = TimeSpan.Zero;
                }
            }
        }
    }
}
