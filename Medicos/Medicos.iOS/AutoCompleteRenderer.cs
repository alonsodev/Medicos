using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreGraphics;
using Medicos.AutoComplete;
using Medicos.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AutoComplete), typeof(AutoCompleteRenderer))]
namespace Medicos.iOS
{
    using Syncfusion.SfAutoComplete.XForms;
    using Syncfusion.SfAutoComplete.XForms.iOS;

    public class AutoCompleteRenderer : SfAutoCompleteRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var view = (SfAutoComplete)Element;
                /*
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;*/

                /*GradientDrawable gd = new GradientDrawable();
                gd.SetShape(ShapeType.Rectangle);
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(DpToPixels(this.Context,
                            18.0f));*/
                //gd.SetStroke(4, Android.Graphics.Color.Gray);
                //view.BackgroundColor = Xamarin.Forms.Color.Transparent;
                /*Control. .GetAutoEditText().SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);*/
            }
        }

    }
}