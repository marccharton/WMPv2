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
        public static readonly DependencyProperty PlayProperty = DependencyProperty.RegisterAttached("Play", typeof(PlayerState), typeof(MediaElementCustom), new UIPropertyMetadata(PlayPropertyChanged));

        public PlayerState Play 
        {
            get { return ((PlayerState)GetValue(PlayProperty)); }
            set { SetValue(PlayProperty, value); }
        }

        public static void PlayPropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
        {
            //MessageBox.Show(((MediaElementCustom)dep).GetValue(ev.Property).ToString());
            try
            {
                if ((PlayerState)((MediaElementCustom)dep).GetValue(ev.Property) == PlayerState.Play)
                    ((MediaElementCustom)dep).Play();
                else if ((PlayerState)((MediaElementCustom)dep).GetValue(ev.Property) == PlayerState.Pause)
                {
                    ((MediaElementCustom)dep).Pause();
                    //MessageBox.Show("Pause!");
                }
                else if ((PlayerState)((MediaElementCustom)dep).GetValue(ev.Property) == PlayerState.Stop)
                {
                     //MessageBox.Show("Stop");
                    ((MediaElementCustom)dep).Stop();
                    ((MediaElementCustom)dep).Close();
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
