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

        void TextBlockCustom_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!this.IsVisible)
                    TextBlockCustomEffect.getInstance().removeElement(this.Text.Length);
            }));
        }

        void TextBlockCustom_Initialized(object sender, EventArgs e)
        {
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(TextBlockCustom_IsVisibleChanged);
            TextBlockCustomEffect.getInstance().EventTime += new EventHandler(EventTime_Changed);
            TextBlockCustomEffect.getInstance().addElement(this.Text.Length);
        }

        private void EventTime_Changed(object sender, EventArgs e)
        {
            this.Margin = TextBlockCustomEffect.getInstance().Thick;
        }
    }
}
