using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace SlideBarMVVM
{
    public class DropAdorner : Adorner
    {
        private Rectangle _child;
        private double _left;
        public double Left
        {
            get
            {
                return this._left;
            }

            set
            {
                this._left = value;
                this.UpdatePosition();
            }
        }
        private double _top;
        public double Top
        {
            get
            {
                return this._top;
            }

            set
            {
                this._top = value;
                this.UpdatePosition();
            }
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public DropAdorner(UIElement adornedElement, UIElement adorningElement) : base(adornedElement)
        {
            VisualBrush brush = new VisualBrush(adorningElement);
            this._child = new Rectangle();
            this._child.Width = adorningElement.RenderSize.Width;
            this._child.Height = adorningElement.RenderSize.Height;
            this._child.Fill = brush;
            this._child.IsHitTestVisible = false;
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(this.Left, this.Top));
            return result;
        }

        protected override Size MeasureOverride(Size s)
        {
            this._child.Measure(s);
            return this._child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size s)
        {
            this._child.Arrange(new Rect(s));
            return s;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this._child;
        }

        private void UpdatePosition()
        {
            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
                adornerLayer.Update(AdornedElement);
        }       
    }
}
