using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Documents;
using System.IO;

namespace SlideBarMVVM
{
    class ListViewCustom : ListView
    {

        private Point _startPos;
        private bool _isDragging;
        private bool _dragElementFound;
        private AdornerLayer _adornLay;
        private DropAdorner _dropAborn;
        private int originalItemIndex;

        public int OriginalItemIndex
        {
            get { return this.originalItemIndex; }
            set { this.originalItemIndex = value; }
        }
        public AdornerLayer AdornerLayer
        {
            get
            {
                if (this._adornLay != null)
                {
                    return this._adornLay;
                }
                else
                {
                    this._adornLay = AdornerLayer.GetAdornerLayer((Visual)this);
                    return AdornerLayer;
                }
            }
        }

        public ListViewCustom()
        {
            this.Initialized += new EventHandler(ListViewCustom_Initialized);
            this._isDragging = false;
        }

        void ListViewCustom_Initialized(object sender, EventArgs e)
        {
            this.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(ListViewCustom_MouseDoubleClick);
            this.AllowDrop = true;
            this.Drop += new DragEventHandler(ListViewCustom_Drop);
            this.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ListViewCustom_PreviewMouseLeftButtonDown);
            this.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(ListViewCustom_PreviewMouseLeftButtonUp);
            this.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(ListViewCustom_PreviewMouseMove);
            this.PreviewKeyDown += new KeyEventHandler(ListViewCustom_PreviewKeyDown);
        }

        void ListViewCustom_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                CurrentList curList = CurrentList.getInstance();

                curList.removeElement(this.SelectedIndex);
            }
        }

        private void ElementFound() 
        {
            this._isDragging = true;
            this._dragElementFound = true;
            this.originalItemIndex = this.SelectedIndex;
            ListBoxItem listBoxItem = (ListBoxItem)ItemContainerGenerator.ContainerFromIndex(this.originalItemIndex);
            this._dropAborn = new DropAdorner((UIElement)this, listBoxItem);
            this.AdornerLayer.Add(this._dropAborn);
        }

        private int getIdxHit(object sender, MouseButtonEventArgs e) 
        {
            Point pt = e.GetPosition((UIElement)sender);
            HitTestResult result = VisualTreeHelper.HitTest(this, pt);
            int ret = -1;

            if (result != null) 
            {
                DependencyObject obj = result.VisualHit;
                while (!(obj is ListView) && obj != null) 
                {
                    obj = VisualTreeHelper.GetParent(obj);
                    if (obj is ListViewItem) 
                    {
                        ListViewItem tmp = obj as ListViewItem;
                        for (int i = 0; i < this.Items.Count; ++i)
                        {
                            if (tmp == this.ItemContainerGenerator.ContainerFromIndex(i))
                            {
                                ret = i;
                                break;
                            }
                        }                    
                    }
                }
            }
            return (ret);
        }

        private String getElementNameHit(object sender, MouseButtonEventArgs e) 
        {
            Point pt = e.GetPosition((UIElement)sender);
            HitTestResult result = VisualTreeHelper.HitTest(this, pt);
            String bordel = null;

            if (result != null)
            {
                DependencyObject obj = result.VisualHit;
                while (!(obj is ListView) && obj != null)
                {
                    obj = VisualTreeHelper.GetParent(obj);
                    if (obj is ListViewItem)
                    {
                        ListViewItem tmp = obj as ListViewItem;
                        bordel = tmp.Content.ToString();
                        break;
                    }
                }
                // MessageBox.Show(bordel);
            }
            return (bordel);
        }

        private void ListViewCustom_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount != 2)
            {
                int idx = this.getIdxHit(this, e);
                if (idx != -1)
                {
                    this.SelectedIndex = idx;
                    this.ElementFound();
                    this._startPos = e.GetPosition(this);
                }
            }
        }

        private void ListViewCustom_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int endIdx;

            if (this._isDragging && this._dragElementFound)
            {
                this._isDragging = false;
                this._dragElementFound = false;

                this._adornLay.Remove(this._dropAborn);

                endIdx = this.getIdxHit(this, e);
                if (endIdx >= 0 && endIdx != this.originalItemIndex)
                {
                    String tmp = this.Items[this.originalItemIndex].ToString();
                    CurrentList.getInstance().RemoveAt(this.originalItemIndex);
                    CurrentList.getInstance().InsertAt(endIdx, tmp);
                }
            }
        }

        private void ListViewCustom_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this._isDragging)
            {
                if (this._dropAborn != null)
                {
                    Point currentPosition = e.GetPosition(this);
                    this._dropAborn.Left = currentPosition.X;
                    this._dropAborn.Top = currentPosition.Y;
                }
            }
        }

        void ListViewCustom_Drop(object sender, DragEventArgs e)
        {
            CurrentList curList = CurrentList.getInstance();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null)
            {
                foreach (string s in files)
                    CurrentList.getInstance().addElement(s);
            }
        }

        void ListViewCustom_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (this.getElementNameHit(this, e) != null)
            {
                CurrentList curList = CurrentList.getInstance();

                curList.moveToIdx(this.SelectedIndex);
                curList.DropEvent(this, null);
            }
        }

    }
}
