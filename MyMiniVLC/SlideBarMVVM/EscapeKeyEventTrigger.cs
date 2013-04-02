using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SlideBarMVVM
{
    public class EscapeKeyEventTrigger : System.Windows.Interactivity.EventTrigger
    {
        public EscapeKeyEventTrigger() : base("KeyDown") 
        {
        }

        protected override void OnEvent(EventArgs eventArgs)
        {
            var evt = eventArgs as KeyEventArgs;
            if (evt != null && evt.Key == Key.Escape)
                this.InvokeActions(eventArgs);
        }
    }
}
