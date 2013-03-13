using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;

namespace SlideBarMVVM
{
    class GridBehavior : Behavior<Grid>
    {
        private Boolean _fullScreen;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
            _fullScreen = false;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window w = App.Current.MainWindow;
            if (e.ClickCount == 2)
            {
                if (!_fullScreen)
                {
                    w.WindowStyle = WindowStyle.None;
                    w.WindowState = WindowState.Maximized;
                    _fullScreen = true;
                }
                else
                {
                    w.WindowStyle = WindowStyle.SingleBorderWindow;
                    w.WindowState = WindowState.Normal;
                    _fullScreen = false;
                }
            }
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            CurrentList curList = CurrentList.getInstance();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null)
            {
                curList.ResetList();
                foreach (string s in files)
                {
                    curList.addElement(s);
                    //CurrentList.DropEvent(this, null);
                }
            }
        }

    }
}
