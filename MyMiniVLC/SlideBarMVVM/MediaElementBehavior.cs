using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace SlideBarMVVM
{
    public static class MediaElementBehavior
    {
          //public static readonly DependencyProperty PlaystateProperty = DependencyProperty.RegisterAttached("Playstate", typeof(PlayerState), typeof(MediaElementBehavior), new UIPropertyMetadata(PlaystatePropertyChanged));

          //public static PlayerState GetPlaystate(MediaElement m)
          //{
          //    return (PlayerState)(m.GetValue(PlaystateProperty));
          //}

          //public static void SetPlaystate(MediaElement m, PlayerState ps)
          //{
          //    m.SetValue(PlaystateProperty, ps);
          //}

          //public static void PlaystatePropertyChanged(DependencyObject dep, DependencyPropertyChangedEventArgs ev)
          //{
          //    try
          //    {
          //        if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Play)
          //            ((MediaElement)dep).Play();
          //        else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Pause)
          //            ((MediaElement)dep).Pause();
          //        else if ((PlayerState)((MediaElement)dep).GetValue(ev.Property) == PlayerState.Stop)
          //            ((MediaElement)dep).Stop();
          //    }
          //    catch (Exception ex)
          //    {
          //        MessageBox.Show(ex.Message);
          //    }
          //}
    }
}
