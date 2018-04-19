using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class BeneficiosViewModel
    {
        #region Commands
        public ICommand EncuestaPaso1Command
        {
            get
            {
                return new RelayCommand(EncuestaPaso1);
            }
        }

        private async void EncuestaPaso1()
        {
            MainViewModel.GetInstance().EncuestaPaso1 = new EncuestaPaso1ViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new EncuestaPaso1Page());
        }

        # endregion
    }
}
