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

        private ObservableCollection<Ano> anos;
        private ObservableCollection<Mes> meses;
        private string buscar;
        private Mes selectedMes;
        private Ano selectedAno;
        private List<Medicamento> medicamentos;
        private ObservableCollection<PacienteItemViewModel> pacientes;
        #endregion

        #region Properties
        public Ano SelectedAno {
            get { return selectedAno; }
            set
            {
                selectedAno = value;
                this.Search();
            }
        }
        public ObservableCollection<Ano> Anos
        {
            get { return this.anos; }
            set { SetValue(ref this.anos, value); }
        }

        public Mes SelectedMes { get { return selectedMes; }
            set {
                selectedMes = value;
                this.Search();
            }
        }

        public ObservableCollection<Mes> Meses
        {
            get { return this.meses; }
            set { SetValue(ref this.meses, value); }
        }

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
            if (string.IsNullOrEmpty(this.Buscar) && (this.SelectedAno == null || SelectedAno.AnoValor == 0) && (this.SelectedMes == null || SelectedMes.MesValor == 0))
            {
                this.Pacientes = new ObservableCollection<PacienteItemViewModel>(
                    this.ToPacienteItemViewModel());
            }
            else
            {
                this.Pacientes = new ObservableCollection<PacienteItemViewModel>(
                    this.ToPacienteItemViewModel().Where(
                        l => (
                            l.Nombres.ToLower().Contains((string.IsNullOrEmpty(this.Buscar) ? "" : this.Buscar).ToLower()) ||
                             l.Documento.ToLower().Contains((string.IsNullOrEmpty(this.Buscar) ? "" : this.Buscar).ToLower()) ||
                             l.Medicamento.ToLower().Contains((string.IsNullOrEmpty(this.Buscar) ? "" : this.Buscar).ToLower())
                             ) && DateTime.Parse(l.Fecha_Ingreso_PSP).Year == (SelectedAno == null || SelectedAno.AnoValor == 0 ? DateTime.Parse(l.Fecha_Ingreso_PSP).Year : SelectedAno.AnoValor)
                              && DateTime.Parse(l.Fecha_Ingreso_PSP).Month == (SelectedMes == null || SelectedMes.MesValor == 0 ? DateTime.Parse(l.Fecha_Ingreso_PSP).Month : SelectedMes.MesValor)
                             )
                );
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

            List<Ano> _anos = this.Pacientes.GroupBy(p => DateTime.Parse(p.Fecha_Ingreso_PSP).Year).Select(p => new Ano { AnoValor = DateTime.Parse(p.First().Fecha_Ingreso_PSP).Year, AnoStr = DateTime.Parse(p.First().Fecha_Ingreso_PSP).Year.ToString() }).ToList();
            _anos.Add(new Ano { AnoValor = 0, AnoStr = "TODOS" });
            _anos = _anos.OrderBy(p => p.AnoValor).ToList();
            this.Anos = new ObservableCollection<Ano>(_anos);

            List<Mes> _meses = this.Pacientes.GroupBy(p => DateTime.Parse(p.Fecha_Ingreso_PSP).Month).Select(p => new Mes{ MesValor = DateTime.Parse(p.First().Fecha_Ingreso_PSP).Month, MesStr = GetMonthName(DateTime.Parse(p.First().Fecha_Ingreso_PSP).Month) } ).ToList();
            _meses.Add(new Mes { MesValor = 0, MesStr = "TODOS" });
            _meses = _meses.OrderBy(p => p.MesValor).ToList();
            this.Meses = new ObservableCollection<Mes>(_meses);

            this.SelectedAno = new Ano { AnoValor = 0, AnoStr = "TODOS" };
            this.SelectedMes = new Mes { MesStr = "TODOS", MesValor = 0 };

            this.IsRefreshing = false;
        }

        private string GetMonthName(int mes)
        {
            string returnStr = "";
            switch (mes)
            {
                case 1:
                    returnStr = "Enero";
                    break;
                case 2:
                    returnStr = "Febrero";
                    break;
                case 3:
                    returnStr = "Marzo";
                    break;
                case 4:
                    returnStr = "Abril";
                    break;
                case 5:
                    returnStr = "Mayo";
                    break;
                case 6:
                    returnStr = "Junio";
                    break;
                case 7:
                    returnStr = "Julio";
                    break;
                case 8:
                    returnStr = "Agosto";
                    break;
                case 9:
                    returnStr = "Septiembre";
                    break;
                case 10:
                    returnStr = "Octubre";
                    break;
                case 11:
                    returnStr = "Noviembre";
                    break;
                case 12:
                    returnStr = "Diciembre";
                    break;
                default:
                    returnStr = "";
                    break;
            }

            return returnStr;
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
