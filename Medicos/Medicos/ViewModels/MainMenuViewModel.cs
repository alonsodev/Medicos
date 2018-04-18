using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class MainMenuViewModel
    {

        #region Constructors
        public MainMenuViewModel()
        {
        }
        #endregion
        public ICommand ReportarPacienteCommand
        {
            get
            {
                return new RelayCommand(ReportarPaciente);
            }
        }

        private async void ReportarPaciente()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ReportarPacientePage());
        }
    }
}
