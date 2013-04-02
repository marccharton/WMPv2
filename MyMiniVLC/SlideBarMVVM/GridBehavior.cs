using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace SlideBarMVVM
{
    class GridBehavior : Behavior<Grid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= AssociatedObject_Drop;
        }
            
        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            CurrentList curList = CurrentList.getInstance();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null)
            {
                curList.ResetList();
                foreach (string s in files)
                    curList.addElement(s);
                curList.DropEvent(this, null);
            }
        }

    }
}
