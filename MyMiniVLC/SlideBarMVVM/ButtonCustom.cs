using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SlideBarMVVM
{
    public class ButtonCustom : Button
    {
        public ButtonCustom() 
        {
            this.MouseDown += new System.Windows.Input.MouseButtonEventHandler(ButtonCustom_MouseDown);
            this.MouseUp += new System.Windows.Input.MouseButtonEventHandler(ButtonCustom_MouseUp);
        }

        void ButtonCustom_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
            
        }

        void ButtonCustom_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
