using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Medicos.Services;
    using Medicos.Models;
    using System.Collections.ObjectModel;

    public class ConsentimientoInformadoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Ciudad> ciudades;
        private string correo;
        private string cuidador;
        private string cuidadorDocumento;
        private string parentesco;
        private string cuidadorTelefono;
        private string centroAtencion;
        #endregion

        #region Properties
        public ObservableCollection<Ciudad> Ciudades
        {
            get { return this.ciudades; }
            set { SetValue(ref this.ciudades, value); }
        }

        public Ciudad SelectedCiudad { get; set; }

        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }

        public string Cuidador
        {
            get { return this.cuidador; }
            set { SetValue(ref this.cuidador, value); }
        }

        public string CuidadorDocumento
        {
            get { return this.cuidadorDocumento; }
            set { SetValue(ref this.cuidadorDocumento, value); }
        }

        public string Parentesco
        {
            get { return this.parentesco; }
            set { SetValue(ref this.parentesco, value); }
        }

        public string CuidadorTelefono
        {
            get { return this.cuidadorTelefono; }
            set { SetValue(ref this.cuidadorTelefono, value); }
        }

        public string CentroAtencion
        {
            get { return this.centroAtencion; }
            set { SetValue(ref this.centroAtencion, value); }
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
        public ConsentimientoInformadoViewModel()
        {

            this.apiService = new ApiService();
            this.loadData();
            this.IsEnabled = true;
            
        }
        #endregion

        #region Methods
        private async void loadData()
        {
            var vMainViewModel = MainViewModel.GetInstance();

            Paciente oPaciente = vMainViewModel.Paciente;

            this.Correo = oPaciente.Correo;
            //oPaciente.Id_ciudad = this.SelectedCiudad != null ? this.SelectedCiudad.Id_ciudad.ToString() : "N/A";
            this.Cuidador = oPaciente.Cuidador;
            this.CuidadorDocumento = oPaciente.Documento_cuidador;
            this.Parentesco = oPaciente.Parentesco;
            this.CentroAtencion = oPaciente.Centro_atencion;
            this.CuidadorTelefono = oPaciente.Telefono_cuidador;

            //medicamentos = await this.apiService.Medicamentos();
            /*List<Ciudad> _ciudades = await this.apiService.Ciudades("null");
            this.Ciudades = new ObservableCollection<Ciudad>(_ciudades);*/
        
        }
        #endregion

        #region Commands
        public ICommand SiguienteCommand
        {
            get
            {
                return new RelayCommand(Siguiente);
            }
        }

        private async void Siguiente()
        {
            FillPacienteEntity();

            MainViewModel.GetInstance().ConsentimientoInformadoFirma = new ConsentimientoInformadoFirmaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ConsentimientoInformadoFirmaPage());
        }

        private void FillPacienteEntity()
        {
            var vMainViewModel = MainViewModel.GetInstance();

            Paciente oPaciente = vMainViewModel.Paciente;

            oPaciente.Tipo_documento = "";
            oPaciente.Correo = this.Correo;
            //oPaciente.Id_ciudad = this.SelectedCiudad != null ? this.SelectedCiudad.Id_ciudad.ToString() : "N/A";
            oPaciente.Cuidador = this.Cuidador;
            oPaciente.Tipo_documento_cuidador = "";
            oPaciente.Documento_cuidador = this.CuidadorDocumento;
            oPaciente.Parentesco = this.Parentesco;
            oPaciente.Centro_atencion = this.CentroAtencion;
            oPaciente.Consentimiento_pdf = "";
            oPaciente.Telefono_cuidador = this.CuidadorTelefono;
        }

        #endregion
    }


}
