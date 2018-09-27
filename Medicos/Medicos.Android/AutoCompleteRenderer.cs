using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Medicos.AutoComplete;
using Medicos.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AutoComplete), typeof(AutoCompleteRenderer))]
namespace Medicos.Droid
{
    using Syncfusion.SfAutoComplete.XForms;
    using Syncfusion.SfAutoComplete.XForms.Droid;
    using Xamarin.Forms.Platform.Android;
    using Android.Graphics.Drawables;
    using Android.Graphics;
    using Android.Util;

    public class AutoCompleteRenderer : SfAutoCompleteRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var view = (SfAutoComplete)Element;
                GradientDrawable gd = new GradientDrawable();
                gd.SetShape(ShapeType.Rectangle);
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(DpToPixels(this.Context,
                            18.0f));
                gd.SetStroke(4, Android.Graphics.Color.Gray);
                view.BackgroundColor = Xamarin.Forms.Color.Transparent;
                Control.GetAutoEditText().SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}