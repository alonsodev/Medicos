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

        public ICommand VerPacienteCommand
        {
            get
            {
                return new RelayCommand(VerPaciente);
            }
        }

        public ICommand BeneficiosCommand
        {
            get
            {
                return new RelayCommand(Beneficios);
            }
        }

        private async void Beneficios()
        {
            MainViewModel.GetInstance().Beneficios = new BeneficiosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BeneficiosPage());
        }

        private async void VerPaciente()
        {
            MainViewModel.GetInstance().VerPacienteMenu = new VerPacienteMenuViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VerPacienteMenuPage());
        }

        private async void ReportarPaciente()
        {
            MainViewModel.GetInstance().ReportarPaciente = new ReportarPacienteViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ReportarPacientePage());
        }
    }
}
