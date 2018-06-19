using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using System.Linq;
    using Medicos.Services;
    using System.Collections.ObjectModel;
    using Medicos.Models;

    public class BuscarPacienteViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        
        private string buscar;
        private List<Medicamento> medicamentos;
        private ObservableCollection<PacienteItemViewModel> pacientes;
        #endregion

        #region Properties

        public ObservableCollection<PacienteItemViewModel> Pacientes
        {
            get { return this.pacientes; }
            set { SetValue(ref this.pacientes, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Buscar
        {
            get { return this.buscar; }
            set {
                SetValue(ref this.buscar, value);
                this.Search();
            }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        

        #endregion

        #region Constructors
        public BuscarPacienteViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadPacientes();
        }
        #endregion

        #region Methods

        #endregion

        #region Commands
        public ICommand InformacionPacienteCommand
        {
            get
            {
                return new RelayCommand<object>(InformacionPaciente);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPacientes);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Buscar))
            {
                this.Pacientes = new ObservableCollection<PacienteItemViewModel>(
                    this.ToPacienteItemViewModel());
            }
            else
            {
                this.Pacientes = new ObservableCollection<PacienteItemViewModel>(
                    this.ToPacienteItemViewModel().Where(
                        l => l.Nombres.ToLower().Contains(this.Buscar.ToLower()) ||
                             l.Documento.ToLower().Contains(this.Buscar.ToLower()) ||
                             l.Medicamento.ToLower().Contains(this.Buscar.ToLower())));
            }
        }

        private async void InformacionPaciente(object e)
        {
            PacienteItemViewModel oItem = (PacienteItemViewModel)e;
            MainViewModel.GetInstance().PacienteInfo = oItem;
            MainViewModel.GetInstance().InformacionPaciente = new InformacionPacienteViewModel();
            
            await Application.Current.MainPage.Navigation.PushAsync(new InformacionPacientePage());
        }

        # endregion

        #region Methods
        private async void LoadPacientes()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetPaciente(
                MainViewModel.GetInstance().Usuario,
                "0");

            if (response == null)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                return;
            }

            MainViewModel.GetInstance().PacientesList = (List<PacienteView>)response;
            this.Pacientes = new ObservableCollection<PacienteItemViewModel>(
                this.ToPacienteItemViewModel());
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private IEnumerable<PacienteItemViewModel> ToPacienteItemViewModel()
        {
            return MainViewModel.GetInstance().PacientesList.Select(l => new PacienteItemViewModel
            {
                Nombres = l.Nombres,
                Documento = l.Documento,
                Medicamento = l.Medicamento,
                Ciudad = l.Ciudad,
                Enfermera = l.Enfermera,
                Eps = l.Eps,
                Fecha_Educacion_Tramites_Admin = l.Fecha_Educacion_Tramites_Admin,
                Fecha_Entrega_Muestra_Med = l.Fecha_Entrega_Muestra_Med,
                Fecha_Ingreso_PSP = l.Fecha_Ingreso_PSP,
                Fecha_Inicio_Tratamiento = l.Fecha_Inicio_Tratamiento,
                Fecha_Ultimo_Seguimiento_Telefonico = l.Fecha_Ultimo_Seguimiento_Telefonico,
                Fecha_Ultimo_Seguimient_Presencial = l.Fecha_Ultimo_Seguimient_Presencial
            });
        }

        #endregion
    }
}
