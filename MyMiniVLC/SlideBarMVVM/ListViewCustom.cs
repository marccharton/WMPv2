﻿using System;
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
        }

        private void ElementFound() 
        {
            this._isDragging = true;
            this._dragElementFound = true;
            this.originalItemIndex = this.SelectedIndex;
            ListBoxItem listBoxItem = (ListBoxItem)ItemContainerGenerator.ContainerFromIndex(SelectedIndex);
            this._dropAborn = new DropAdorner((UIElement)this, listBoxItem);
            this.AdornerLayer.Add(this._dropAborn);
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
                String bordel = this.getElementNameHit(this, e);
                if (bordel != null)
                {
                    this.SelectedIndex = CurrentList.getInstance().getAllElement().FindIndex(song => Path.GetFileName(song) == bordel);
                    this.ElementFound();
                    this._startPos = e.GetPosition(this);
                }
            }
        }

        private void ListViewCustom_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            String endText;

            if (this._isDragging && this._dragElementFound)
            {
                this._isDragging = false;
                this._dragElementFound = false;

                this._adornLay.Remove(this._dropAborn);

                object originalItem = this.Items[this.originalItemIndex];
                endText = this.getElementNameHit(this, e);
                if (endText != null)
                {
                    String toAdd = CurrentList.getInstance().getAllElement().Find(song => Path.GetFileName(song) == originalItem.ToString());
                    String before = CurrentList.getInstance().getAllElement().Find(song => Path.GetFileName(song) == endText);
                    if (toAdd != before)
                    {
                        CurrentList.getInstance().getAllElement().Remove(toAdd);
                        CurrentList.getInstance().InsertAfter(before, toAdd);
                    }
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
            CurrentList curList = CurrentList.getInstance();

            curList.moveToIdx(this.SelectedIndex);
            curList.DropEvent(this, null);
        }

    }
}