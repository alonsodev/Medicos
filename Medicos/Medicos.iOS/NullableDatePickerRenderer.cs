using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Medicos.CustomDatePicker;
using Medicos.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(NullableDatePickerRenderer))]
namespace Medicos.iOS
{
    public  class NullableDatePickerRenderer : DatePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && this.Control != null)
            {
                var view = (Medicos.CustomDatePicker.CustomDatePicker)Element;
                this.AddClearButton();

                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;



                /*

                this.Control.BorderStyle = UITextBorderStyle.Line;
                Control.Layer.BorderColor = UIColor.LightGray.CGColor;

                Control.Layer.BorderWidth = 1;
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.MasksToBounds = true;
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    this.Control.Font = UIFont.SystemFontOfSize(25);
                }
*/
                var element = e.NewElement as Medicos.CustomDatePicker.CustomDatePicker;
                if (!string.IsNullOrWhiteSpace(element.PlaceHolder))
                {
                    Control.Text = element.PlaceHolder;
                }

                Control.ShouldEndEditing += (textField) => {
                    var seletedDate = (UITextField)textField;
                    var text = seletedDate.Text;
                    if (text == element.PlaceHolder)
                    {
                        Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    return true;
                };
            }

        }

        private void AddClearButton()
        {
            var originalToolbar = this.Control.InputAccessoryView as UIToolbar;

            if (originalToolbar != null && originalToolbar.Items.Length <= 2)
            {
                var clearButton = new UIBarButtonItem("Limpiar", UIBarButtonItemStyle.Plain, ((sender, ev) =>
                {
                    Medicos.CustomDatePicker.CustomDatePicker baseDatePicker = this.Element as Medicos.CustomDatePicker.CustomDatePicker;
                    this.Element.Unfocus();
                    this.Element.Date = DateTime.Now;
                    baseDatePicker.CleanDate();


                    var element = this.Element as Medicos.CustomDatePicker.CustomDatePicker;
                    if (!string.IsNullOrWhiteSpace(element.PlaceHolder))
                    {
                        Control.Text = element.PlaceHolder;
                    }

                    this.Control.ShouldEndEditing += (textField) => {
                        var seletedDate = (UITextField)textField;
                        var text = seletedDate.Text;
                        if (text == element.PlaceHolder)
                        {
                            Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                        return true;
                    };
                }));

                var newItems = new List<UIBarButtonItem>();
                foreach (var item in originalToolbar.Items)
                {
                    newItems.Add(item);
                }

                newItems.Insert(0, clearButton);

                originalToolbar.Items = newItems.ToArray();
                originalToolbar.SetNeedsDisplay();
            }

        }
        

    }
}