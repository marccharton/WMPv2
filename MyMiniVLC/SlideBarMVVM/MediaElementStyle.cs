using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SlideBarMVVM
{
    public static class MediaElementStyle
    {
        public static readonly DependencyProperty PlayProperty = DependencyProperty.RegisterAttached("Play", typeof(PlayerState), typeof(MediaElementStyle), new UIPropertyMetadata(PlayPropertyChanged));

        public static PlayerState GetPlay(MediaElement m)
        {
            return (PlayerState)(m.GetValue(PlayProperty));
        }

        public static void SetPlay(MediaElement m, PlayerState ps)
        {
            m.SetValue(PlayProperty, ps);
        }

        public static void PlayPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
            try
            {
                if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Play)
                    ((MediaElement)dep).Play();
                else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Pause)
                    ((MediaElement)dep).Pause();
                else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Stop)
                {
                    ((MediaElement)dep).Stop();
                    ((MediaElement)dep).Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
