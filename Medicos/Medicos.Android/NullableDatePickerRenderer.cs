using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Medicos.CustomDatePicker;
using Medicos.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(NullableDatePickerRenderer))]
namespace Medicos.Droid
{
    public  class NullableDatePickerRenderer : ViewRenderer<Medicos.CustomDatePicker.CustomDatePicker, EditText>
    {
        DatePickerDialog _dialog;

        public NullableDatePickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Medicos.CustomDatePicker.CustomDatePicker> e)
        {
            base.OnElementChanged(e);

            this.SetNativeControl(new Android.Widget.EditText(Context));
            if (Control == null || e.NewElement == null)
                return;

            var entry = (Medicos.CustomDatePicker.CustomDatePicker)this.Element;

            if (entry.IsCurvedCornersEnabled)
            {
                // creating gradient drawable for the curved background
                var _gradientBackground = new GradientDrawable();
                _gradientBackground.SetShape(ShapeType.Rectangle);
                _gradientBackground.SetColor(entry.BackgroundColor.ToAndroid());
                entry.BackgroundColor = Color.Transparent;
                //_gradientBackground.SetColor(view.BackgroundColor.ToAndroid());//view.BackgroundColor.ToAndroid()
                // Thickness of the stroke line
                _gradientBackground.SetStroke(entry.BorderWidth, entry.BorderColor.ToAndroid());

                // Radius for the curves
                _gradientBackground.SetCornerRadius(
                    DpToPixels(this.Context,
                        Convert.ToSingle(entry.CornerRadius)));

                // set the background of the label
                Control.SetBackground(_gradientBackground);
            }

            // Set padding for the internal text from border
            Control.SetPadding(
                (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                Control.PaddingTop,
                (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                Control.PaddingBottom);

            
            this.Control.Click += OnPickerClick;
            if (!entry.NullableDate.HasValue)
            {
                Control.SetTextColor(Color.FromHex(entry.PlaceHolderColor).ToAndroid());
                this.Control.Text = entry.PlaceHolder;
            }
            else
            {
                Control.SetTextColor(entry.TextColor.ToAndroid());
                this.Control.Text = Element.Date.ToString(Element.Format);
            }

            this.Control.Text = !entry.NullableDate.HasValue ? entry.PlaceHolder : Element.Date.ToString(Element.Format);
            this.Control.KeyListener = null;
            this.Control.FocusChange += OnPickerFocusChange;
            this.Control.Enabled = Element.IsEnabled;

        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Xamarin.Forms.DatePicker.DateProperty.PropertyName || e.PropertyName == Xamarin.Forms.DatePicker.FormatProperty.PropertyName)
            {
                var entry = (Medicos.CustomDatePicker.CustomDatePicker)this.Element;

                if (this.Element.Format == entry.PlaceHolder)
                {
                    Control.SetTextColor(Color.FromHex(entry.PlaceHolderColor).ToAndroid());
                    this.Control.Text = entry.PlaceHolder;
                    return;
                }
            }

            base.OnElementPropertyChanged(sender, e);
        }

        void OnPickerFocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ShowDatePicker();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                this.Control.Click -= OnPickerClick;
                this.Control.FocusChange -= OnPickerFocusChange;

                if (_dialog != null)
                {
                    _dialog.Hide();
                    _dialog.Dispose();
                    _dialog = null;
                }
            }

            base.Dispose(disposing);
        }

        void OnPickerClick(object sender, EventArgs e)
        {
            ShowDatePicker();
        }

        void SetDate(DateTime date)
        {
            var entry = (Medicos.CustomDatePicker.CustomDatePicker)this.Element;
            Control.SetTextColor(entry.TextColor.ToAndroid());
            this.Control.Text = date.ToString(Element.Format);
            Element.Date = date;
        }

        private void ShowDatePicker()
        {
            CreateDatePickerDialog(this.Element.Date.Year, this.Element.Date.Month - 1, this.Element.Date.Day);
            _dialog.Show();
        }

        void CreateDatePickerDialog(int year, int month, int day)
        {
            Medicos.CustomDatePicker.CustomDatePicker view = Element;
            _dialog = new DatePickerDialog(Context, (o, e) =>
            {
                view.Date = e.Date;
                ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                Control.ClearFocus();

                _dialog = null;
            }, year, month, day);

            _dialog.SetButton("Hecho", (sender, e) =>
            {
                this.Element.Format = this.Element._originalFormat;
                SetDate(_dialog.DatePicker.DateTime);
                this.Element.AssignValue();
            });
            _dialog.SetButton2("Limpiar", (sender, e) =>
            {
                this.Element.CleanDate();
                Control.Text = this.Element.Format;
            });
        }
    }
}