using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Medicos.CustomSearchBar;
using Medicos.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace Medicos.Droid
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (Medicos.CustomSearchBar.CustomSearchBar)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());
                    view.BackgroundColor = Color.Transparent;
                    //_gradientBackground.SetColor(view.BackgroundColor.ToAndroid());//view.BackgroundColor.ToAndroid()
                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);
                }

                // Set padding for the internal text from border
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingBottom);
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}