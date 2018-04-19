using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class ReportarPacienteViewModel
    {
        public ReportarPacienteViewModel()
        {
        }

        #region Commands
        public ICommand ConsentimientoInformadoCommand
        {
            get
            {
                return new RelayCommand(ConsentimientoInformado);
            }
        }

        private async void ConsentimientoInformado()
        {
            MainViewModel.GetInstance().ConsentimientoInformado = new ConsentimientoInformadoViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ConsentimientoInformadoPage());
        }

        #endregion
    }
}
