using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class VerPacienteMenuViewModel
    {

        #region Commands
        public ICommand BuscarPacienteCommand
        {
            get
            {
                return new RelayCommand(BuscarPaciente);
            }
        }

        private async void BuscarPaciente()
        {
            MainViewModel.GetInstance().BuscarPaciente = new BuscarPacienteViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BuscarPacientePage());
        }

        public ICommand VerInformeConsolidadoCommand
        {
            get
            {
                return new RelayCommand(VerInformeConsolidado);
            }
        }

        private async void VerInformeConsolidado()
        {
            MainViewModel.GetInstance().InformeConsolidado = new InformeConsolidadoViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new InformeConsolidadoPage());
        }
        

        #endregion
    }
}
