using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;

namespace SlideBarMVVM
{
    class SliderBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += new System.Windows.Input.MouseButtonEventHandler(toto);
        }

        void toto(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        //    e.GetPosition
        }
    }
}
