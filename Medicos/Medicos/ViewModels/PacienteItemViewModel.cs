using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class PacienteItemViewModel : PacienteView
    {
        

        #region Commands
        public ICommand SelectPacienteCommand
        {
            get
            {
                return new RelayCommand(SelectPaciente);
            }
        }

        private async void SelectPaciente()
        {
            /*MainViewModel.GetInstance().Land = new LandViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());*/
        }
        #endregion
    }
}
