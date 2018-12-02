using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System.Threading.Tasks;

    public class MainMenuViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isVisible;
        
        #endregion

        #region Properties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        #endregion

        #region Constructors
        public MainMenuViewModel()
        {
            

            if (MainViewModel.GetInstance().Token.Rol == "Enfermero")
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }
        }
        #endregion

        #region Commands
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

        public ICommand SignoutCommand
        {
            get
            {
                return new RelayCommand(Signout);
            }
        }
        

        public ICommand NotificacionesCommand
        {
            get
            {
                return new RelayCommand(Notificaciones);
            }
        }

        private async void Signout()
        {
            this.IsRunning = true;
            MainViewModel.GetInstance().Usuario = null;
            MainViewModel.GetInstance().Token = null;
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

            this.IsRunning = false;

        }

        private async void Notificaciones()
        {
            this.IsRunning = true;
            MainViewModel.GetInstance().Notificaciones = new NotificacionesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
            this.IsRunning = false;
        }

        private async void Beneficios()
        {
            this.IsRunning = true;
            MainViewModel.GetInstance().Beneficios = new BeneficiosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BeneficiosPage());
            this.IsRunning = false;
        }

        private async void VerPaciente()
        {
            this.IsRunning = true;
            MainViewModel.GetInstance().PacienteGrafico = new PacienteGraficoViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PacienteGraficoPage());
            this.IsRunning = false;
        }

        private async void ReportarPaciente()
        {
            this.IsRunning = true;
            MainViewModel.GetInstance().ReportarPaciente = new ReportarPacienteViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ReportarPacientePage());
            this.IsRunning = false;
        }
        #endregion
    }
}
