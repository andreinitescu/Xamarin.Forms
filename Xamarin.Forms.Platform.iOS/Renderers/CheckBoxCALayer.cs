using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace Xamarin.Forms.Platform.iOS
{
	public class CheckBoxCALayer : CALayer
	{
		const float _containerLineWidth = 1f;
		const float _checkSize = 25f;
		readonly UIColor _checkBorderStrokeColor = null;
		readonly UIColor _checkBorderFillColor = null;
		readonly UIColor _checkMarkFillColor = null;

		readonly UIButton _nativeControl;
		readonly CheckBox _checkBox;
		readonly CAShapeLayer _checkLayer = new CAShapeLayer();
		readonly CAShapeLayer _containerLayer = new CAShapeLayer();

		public CheckBoxCALayer(CheckBox checkBox, UIButton nativeControl)
		{
			NeedsDisplayOnBoundsChange = true;
			_checkBox = checkBox;
			_nativeControl = nativeControl;
			InitializeLayers();
		}

		public override void Display()
		{
			base.Display();
			ColorLayers();
		}

		public override void LayoutSublayers()
		{
			base.LayoutSublayers();
			LayoutLayers();
		}

		void InitializeLayers()
		{
			_nativeControl.Layer.AddSublayer(_containerLayer);
			_nativeControl.Layer.AddSublayer(_checkLayer);
		}

		void ColorLayers()
		{
			if (_nativeControl.Enabled)
			{
				if (_checkBox.IsChecked)
				{
					_containerLayer.StrokeColor = _checkBorderStrokeColor?.CGColor ?? _nativeControl.TintColor.CGColor;
					_containerLayer.FillColor = _checkBorderFillColor?.CGColor ?? _nativeControl.TintColor.CGColor;
					_checkLayer.FillColor = _checkMarkFillColor?.CGColor ?? UIColor.White.CGColor;
				}
				else
				{
					_containerLayer.StrokeColor = _checkBorderStrokeColor?.CGColor ?? _nativeControl.TintColor.CGColor;
					_containerLayer.FillColor = UIColor.Clear.CGColor;
					_checkLayer.FillColor = UIColor.Clear.CGColor;
				}
			}
			else
			{
				if (_checkBox.IsChecked)
				{
					_containerLayer.StrokeColor = _nativeControl.TintColor.CGColor;
					_containerLayer.FillColor = _nativeControl.TintColor.CGColor;
					_checkLayer.FillColor = UIColor.White.CGColor;
				}
				else
				{
					_containerLayer.StrokeColor = _nativeControl.TintColor.CGColor;
					_containerLayer.FillColor = UIColor.Clear.CGColor;
					_checkLayer.FillColor = UIColor.Clear.CGColor;
				}
			}
		}

		void LayoutLayers()
		{
			CGRect checkFrame = GetCheckFrame(_nativeControl.Bounds);
			_containerLayer.Frame = _nativeControl.Bounds;
			_containerLayer.LineWidth = _containerLineWidth;
			_containerLayer.Path = GetContainerPath(checkFrame);

			_checkLayer.Frame = _nativeControl.Bounds;
			_checkLayer.Path = GetCheckPath(checkFrame);
		}

		protected virtual CGRect GetCheckFrame(CGRect bounds)
		{
			return new CGRect(0, bounds.Height / 2 - _checkSize / 2, _checkSize, _checkSize).Inset(1, 1);
		}

		protected virtual CGPath GetContainerPath(CGRect frame)
		{
			return UIBezierPath.FromOval(frame).CGPath;
		}

		protected virtual CGPath GetCheckPath(CGRect frame)
		{
			var checkPath = new UIBezierPath();
			checkPath.MoveTo(new CGPoint(frame.Left + 0.76208f * frame.Width, frame.Top + 0.26000f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.38805f * frame.Width, frame.Top + 0.62670f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.23230f * frame.Width, frame.Top + 0.47400f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.18000f * frame.Width, frame.Top + 0.52527f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.36190f * frame.Width, frame.Top + 0.70360f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.38805f * frame.Width, frame.Top + 0.72813f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.41420f * frame.Width, frame.Top + 0.70360f * frame.Height));
			checkPath.AddLineTo(new CGPoint(frame.Left + 0.81437f * frame.Width, frame.Top + 0.31127f * frame.Height));
			checkPath.ClosePath();
			return checkPath.CGPath;
		}
	}
}