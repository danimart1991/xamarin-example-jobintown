using System;
using Xamarin.Forms;

namespace JobInTown.Controls
{
    public class PinchToZoomContainer : ContentView
    {
        private double _currentScale;
        private double _startScale;
        private double _xOffset;
        private double _yOffset;
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public PinchToZoomContainer()
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            _width = widthConstraint;
            _height = heightConstraint;

            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX =
                      Math.Max(Math.Min(0, _x + e.TotalX), -Math.Abs(Content.Width - _width));
                    Content.TranslationY =
                      Math.Max(Math.Min(0, _y + e.TotalY), -Math.Abs(Content.Height - _height));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    _x = Content.TranslationX;
                    _y = Content.TranslationY;
                    break;
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                _startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }

            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                _currentScale += (e.Scale - 1) * _startScale;
                _currentScale = Math.Max(1, _currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + _xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * _startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + _yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * _startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = _xOffset - ((originX * Content.Width) * (_currentScale - _startScale));
                double targetY = _yOffset - ((originY * Content.Height) * (_currentScale - _startScale));

                // Apply translation based on the change in origin.
                Content.TranslationX = Clamp(targetX, -Content.Width * (_currentScale - 1), 0);
                Content.TranslationY = Clamp(targetY, -Content.Height * (_currentScale - 1), 0);

                // Apply scale factor.
                Content.Scale = _currentScale;
            }

            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                _xOffset = Content.TranslationX;
                _yOffset = Content.TranslationY;
            }
        }

        private T Clamp<T>(T value, T minimum, T maximum)
            where T : IComparable
        {
            if (value.CompareTo(minimum) < 0)
            {
                return minimum;
            }
            else if (value.CompareTo(maximum) > 0)
            {
                return maximum;
            }
            else
            {
                return value;
            }
        }
    }
}
