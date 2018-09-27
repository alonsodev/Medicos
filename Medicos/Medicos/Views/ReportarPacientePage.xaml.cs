using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Medicos.Views
{
    using Medicos.ViewModels;
    using Syncfusion.SfAutoComplete.XForms;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    public partial class ReportarPacientePage : ContentPage
	{
		public ReportarPacientePage ()
		{
			InitializeComponent ();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vMainViewModel = MainViewModel.GetInstance();
            vMainViewModel.checkExistsSession();
        }
    }
}