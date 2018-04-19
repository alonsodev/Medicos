using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class BuscarPacienteViewModel
    {
        #region Commands
        public ICommand InformacionPacienteCommand
        {
            get
            {
                return new RelayCommand(InformacionPaciente);
            }
        }

        private async void InformacionPaciente()
        {
            MainViewModel.GetInstance().InformacionPaciente = new InformacionPacienteViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new InformacionPacientePage());
        }

        # endregion
    }
}
