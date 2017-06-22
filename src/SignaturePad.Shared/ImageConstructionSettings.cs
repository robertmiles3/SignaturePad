using System;

#if __ANDROID__
using NativeSize = System.Drawing.SizeF;
using NativeColor = Android.Graphics.Color;
using NativeNullableColor = System.Nullable<Android.Graphics.Color>;
#elif __IOS__
using NativeSize = CoreGraphics.CGSize;
using NativeColor = UIKit.UIColor;
using NativeNullableColor = UIKit.UIColor;
#elif WINDOWS_PHONE
using NativeSize = System.Windows.Size;
using NativeColor = System.Windows.Media.Color;
using NativeNullableColor = System.Nullable<System.Windows.Media.Color>;
#elif WINDOWS_UWP || WINDOWS_APP
using NativeSize = Windows.Foundation.Size;
using NativeColor = Windows.UI.Color;
using NativeNullableColor = System.Nullable<Windows.UI.Color>;
#elif WINDOWS_PHONE_APP
using NativeSize = Windows.Foundation.Size;
using NativeColor = Windows.UI.Color;
using NativeNullableColor = System.Nullable<Windows.UI.Color>;
#endif

namespace Xamarin.Controls
{
	public enum SizeOrScaleType
	{
		Size,
		Scale
	}

	public struct SizeOrScale
	{
		public SizeOrScale(float xy, SizeOrScaleType type)
		{
			X = xy;
			Y = xy;
			Type = type;
			KeepAspectRatio = true;
		}

		public SizeOrScale(float xy, SizeOrScaleType type, bool keepAspectRatio)
		{
			X = xy;
			Y = xy;
			Type = type;
			KeepAspectRatio = keepAspectRatio;
		}

		public SizeOrScale(NativeSize size, SizeOrScaleType type)
		{
			X = (float)size.Width;
			Y = (float)size.Height;
			Type = type;
			KeepAspectRatio = true;
		}

		public SizeOrScale(NativeSize size, SizeOrScaleType type, bool keepAspectRatio)
		{
			X = (float)size.Width;
			Y = (float)size.Height;
			Type = type;
			KeepAspectRatio = keepAspectRatio;
		}

		public SizeOrScale(float x, float y, SizeOrScaleType type)
		{
			X = x;
			Y = y;
			Type = type;
			KeepAspectRatio = true;
		}

		public SizeOrScale(float x, float y, SizeOrScaleType type, bool keepAspectRatio)
		{
			X = x;
			Y = y;
			Type = type;
			KeepAspectRatio = keepAspectRatio;
		}

		public float X { get; set; }

		public float Y { get; set; }

		public SizeOrScaleType Type { get; set; }

		public bool KeepAspectRatio { get; set; }

		public bool IsValid => X > 0 && Y > 0;

		public NativeSize GetScale(float width, float height)
		{
			if (Type == SizeOrScaleType.Scale)
			{
				return new NativeSize(X, Y);
			}
			else
			{
				return new NativeSize(X / width, Y / height);
			}
		}

		public NativeSize GetSize(float width, float height)
		{
			if (Type == SizeOrScaleType.Scale)
			{
				return new NativeSize(width * X, height * Y);
			}
			else
			{
				return new NativeSize(X, Y);
			}
		}

		public static implicit operator SizeOrScale(float scale)
		{
			return new SizeOrScale(scale, SizeOrScaleType.Scale);
		}

		public static implicit operator SizeOrScale(NativeSize size)
		{
			return new SizeOrScale((float)size.Width, (float)size.Height, SizeOrScaleType.Size);
		}
	}

	public struct ImageConstructionSettings
	{
#if __IOS__ || __ANDROID__
		internal static readonly NativeColor Black = NativeColor.Black;
		internal static readonly NativeColor Transparent = new NativeColor(0, 0, 0, 0);
#elif WINDOWS_PHONE || WINDOWS_UWP || WINDOWS_PHONE_APP || WINDOWS_APP
		internal static readonly NativeColor Black = NativeColor.FromArgb (255, 0, 0, 0);
		internal static readonly NativeColor Transparent = NativeColor.FromArgb (0, 0, 0, 0);
#endif

		public NullableBoolean ShouldCrop { get; set; }

		public NullableSizeOrScale DesiredSizeOrScale { get; set; }

		public NativeNullableColor StrokeColor { get; set; }

		public NativeNullableColor BackgroundColor { get; set; }

		public NullableSingle StrokeWidth { get; set; }

		internal void ApplyDefaults()
		{
			ApplyDefaults(1f, Black);
		}

		internal void ApplyDefaults(float strokeWidth, NativeColor strokeColor)
		{
			ShouldCrop = new NullableBoolean(ShouldCrop.HasValue ? ShouldCrop.Value : true);
			DesiredSizeOrScale = new NullableSizeOrScale(DesiredSizeOrScale.HasValue ? DesiredSizeOrScale.Value : 1f);
			StrokeColor = StrokeColor ?? strokeColor;
			BackgroundColor = BackgroundColor ?? Transparent;
			StrokeWidth = new NullableSingle(StrokeWidth.HasValue ? StrokeWidth.Value : strokeWidth);
		}
	}

	public struct NullableSingle
	{
		private float value;
		private bool hasValue;

		public NullableSingle(float value)
		{
			hasValue = true;
			this.value = value;
		}

		public bool HasValue => hasValue;

		public float Value
		{
			get
			{
				if (!hasValue)
					throw new InvalidOperationException("Nullable object must have a value.");
				return value;
			}
		}

		public static implicit operator NullableSingle(float value)
		{
			return new NullableSingle(value);
		}
	}

	public struct NullableSizeOrScale
	{
		private SizeOrScale value;
		private bool hasValue;

		public NullableSizeOrScale(SizeOrScale value)
		{
			hasValue = true;
			this.value = value;
		}

		public bool HasValue => hasValue;

		public SizeOrScale Value
		{
			get
			{
				if (!hasValue)
					throw new InvalidOperationException("Nullable object must have a value.");
				return value;
			}
		}

		public static implicit operator NullableSizeOrScale(SizeOrScale value)
		{
			return new NullableSizeOrScale(value);
		}
	}

	public struct NullableBoolean
	{
		private bool value;
		private bool hasValue;

		public NullableBoolean(bool value)
		{
			hasValue = true;
			this.value = value;
		}

		public bool HasValue => hasValue;

		public bool Value
		{
			get
			{
				if (!hasValue)
					throw new InvalidOperationException("Nullable object must have a value.");
				return value;
			}
		}

		public static implicit operator NullableBoolean(bool value)
		{
			return new NullableBoolean(value);
		}
	}
}
