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
        public static readonly DependencyProperty PlayCustomProperty = DependencyProperty.RegisterAttached("PlayCustom", typeof(PlayerState), typeof(MediaElementCustom), new UIPropertyMetadata(PlayPropertyChanged));

        public PlayerState PlayCustom
        {
            get { return ((PlayerState)GetValue(PlayCustomProperty)); }
            set { SetValue(PlayCustomProperty, value); }
        }

        public static void PlayPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
            //MessageBox.Show(((MediaElementCustom)dep).GetValue(ev.Property).ToString());
            try
            {
                if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Play)
                    ((MediaElement)dep).Play();
                else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Pause)
                {
                    ((MediaElementCustom)dep).Pause();
                    //MessageBox.Show("Pause!");
                }
                else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Stop)
                {
                     //MessageBox.Show("Stop");
                    ((MediaElement)dep).Stop();
                    ((MediaElement)dep).Close();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
