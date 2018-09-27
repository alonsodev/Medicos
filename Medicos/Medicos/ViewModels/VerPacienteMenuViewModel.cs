using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class VerPacienteMenuViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        #endregion

        #region Constructors
        public VerPacienteMenuViewModel()
        {

        }
        #endregion

    #region Properties
    public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

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
            this.IsRunning = true;
            MainViewModel.GetInstance().BuscarPaciente = new BuscarPacienteViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BuscarPacientePage());
            this.IsRunning = false;
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
            this.IsRunning = true;
            MainViewModel.GetInstance().InformeConsolidado = new InformeConsolidadoViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new InformeConsolidadoPage());
            this.IsRunning = false;
        }
        

        #endregion
    }
}
