using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Medicos.Views
{
    using Medicos.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    public partial class MainMenuPage : ContentPage
	{
		public MainMenuPage ()
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