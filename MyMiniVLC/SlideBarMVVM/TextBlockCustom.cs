using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Timers;

namespace SlideBarMVVM
{
    class TextBlockCustom : TextBlock
    {

        public TextBlockCustom()
        {
            this.Initialized += new EventHandler(TextBlockCustom_Initialized);
        }

        void TextBlockCustom_Initialized(object sender, EventArgs e)
        {
            TextBlockCustomEffect.getInstance().EventTime += new EventHandler(EventTime_Changed);
            this.Unloaded += new RoutedEventHandler(TextBlockCustom_Unloaded);
            this.Loaded += new RoutedEventHandler(TextBlockCustom_Loaded);
        }

        void TextBlockCustom_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                TextBlockCustomEffect.getInstance().addElement(this.Text.Length);
            }));
        }

        void TextBlockCustom_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                TextBlockCustomEffect.getInstance().removeElement(this.Text.Length);
            }));
        }

        private void EventTime_Changed(object sender, EventArgs e)
        {
            this.Margin = TextBlockCustomEffect.getInstance().Thick;
        }
    }
}
