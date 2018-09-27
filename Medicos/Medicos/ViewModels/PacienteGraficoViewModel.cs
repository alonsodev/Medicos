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

    public class PacienteGraficoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;

        private ObservableCollection<PacienteItemViewModel> pacientes;
        private ObservableCollection<PacienteMes> data;
        #endregion

        #region Properties
        public ObservableCollection<PacienteMes> Data {
            get { return this.data; }
            set { SetValue(ref this.data, value); }
        }

        public ObservableCollection<PacienteItemViewModel> Pacientes
        {
            get { return this.pacientes; }
            set { SetValue(ref this.pacientes, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructors
        public PacienteGraficoViewModel()
        {

            this.apiService = new ApiService();

            //this.IsRemembered = true;
            this.IsEnabled = true;
            this.LoadPacientes();
            this.LoadCantidadPacientes();
            /*this.Data = new ObservableCollection<PacienteMes>()
            {
                new PacienteMes { Mes = "Ene", Cantidad = 180 },
                new PacienteMes { Mes = "Feb", Cantidad = 170 },
                new PacienteMes { Mes = "Mar", Cantidad = 160 },
                new PacienteMes { Mes = "Abr", Cantidad = 182 }
            };*/
        }
        #endregion
        #region Methods

        private async void LoadCantidadPacientes()
        {
            List<PacienteMes> _cantidadPacientes = await this.apiService.GetCantidadesPacientes();
            this.Data = new ObservableCollection<PacienteMes>(_cantidadPacientes);

        }
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


        #region Commands
        public ICommand InformacionPacienteCommand
        {
            get
            {
                return new RelayCommand<object>(InformacionPaciente);
            }
        }
        public ICommand BuscarPacienteCommand
        {
            get
            {
                return new RelayCommand(BuscarPaciente);
            }
        }

        private async void InformacionPaciente(object e)
        {
            PacienteItemViewModel oItem = (PacienteItemViewModel)e;
            MainViewModel.GetInstance().PacienteInfo = oItem;
            MainViewModel.GetInstance().InformacionPaciente = new InformacionPacienteViewModel();

            await Application.Current.MainPage.Navigation.PushAsync(new InformacionPacientePage());
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
