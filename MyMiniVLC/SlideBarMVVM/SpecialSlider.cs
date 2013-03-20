using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SlideBarMVVM
{
    public class SpecialSlider : Slider
    {
        public SpecialSlider()
        {
            //this.MouseMove += OnMouseMove;
            //this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(SpecialSlider_PreviewMouseLeftButtonUp);
            //this.MouseLeave += new MouseEventHandler(SpecialSlider_MouseLeave);
            //this.LostMouseCapture += new MouseEventHandler(SpecialSlider_LostMouseCapture);
        }

        private Track track;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            track = Template.FindName("PART_Track", this) as Track;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && track != null)
                Value = track.ValueFromPoint(e.GetPosition(track));
        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            CurrentList.getInstance().IsMovingPosition = true;
            base.OnPreviewMouseDown(e);
            ((UIElement)e.OriginalSource).CaptureMouse();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            CurrentList.getInstance().IsMovingPosition = false;
            if (this.Value + 0.0001 < this.Maximum)
                this.Value += 0.0001;
            else
                this.Value -= 0.0001;
            base.OnPreviewMouseUp(e);
            ((UIElement)e.OriginalSource).ReleaseMouseCapture();
        }
    }
}
