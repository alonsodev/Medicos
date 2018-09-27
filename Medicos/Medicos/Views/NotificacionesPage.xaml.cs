using Medicos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medicos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificacionesPage : ContentPage
	{
		public NotificacionesPage ()
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