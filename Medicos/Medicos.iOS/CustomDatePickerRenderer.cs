using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Medicos.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Medicos.CustomDatePicker;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace Medicos.iOS
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (Medicos.CustomDatePicker.CustomDatePicker)Element;
                /*
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;
                Control.Layer.MasksToBounds = true;*/
            }
        }
    }
}