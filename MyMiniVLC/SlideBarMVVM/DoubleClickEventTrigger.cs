using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace SlideBarMVVM
{
    public class DoubleClickEventTrigger : System.Windows.Interactivity.EventTrigger
    {
        public DoubleClickEventTrigger() : base("MouseDown")
        {
        }

        protected override void OnEvent(EventArgs eventArgs)
        {
            var tmp = eventArgs as MouseButtonEventArgs;
            if (tmp != null && tmp.ClickCount == 2)
                this.InvokeActions(eventArgs);
        }
    }
}
