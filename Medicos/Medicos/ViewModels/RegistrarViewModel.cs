using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Medicos.Services;
    using System.Collections.ObjectModel;
    using Medicos.Models;
    using System.Text.RegularExpressions;

    public class RegistrarViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private string nombres;
        private string correo;
        private ObservableCollection<Especialidad> especialidades;
        private string telFijo;
        private string telCelular;
        private ObservableCollection<Ciudad> ciudades;
        private string password;
        #endregion

        #region Properties
        public ObservableCollection<Especialidad> Especialidades
        {
            get { return this.especialidades; }
            set { SetValue(ref this.especialidades, value); }
        }
        public Especialidad SelectedEspecialidad { get; set; }
        public ObservableCollection<Ciudad> Ciudades
        {
            get { return this.ciudades; }
            set { SetValue(ref this.ciudades, value); }
        }
        public Ciudad SelectedCiudad { get; set; }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public string TelCelular
        {
            get { return this.telCelular; }
            set { SetValue(ref this.telCelular, value); }
        }
        public string TelFijo
        {
            get { return this.telFijo; }
            set { SetValue(ref this.telFijo, value); }
        }

        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }
        public string Nombres
        {
            get { return this.nombres; }
            set { SetValue(ref this.nombres, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Commands
        public ICommand FinalizarRegistroCommand
        {
            get
            {
                return new RelayCommand(FinalizarRegistro);
            }
        }

        private async void FinalizarRegistro()
        {
            if (string.IsNullOrEmpty(this.Nombres))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar nombres.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Correo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar correo.",
                    "Aceptar");
                return;
            }

            if (this.SelectedCiudad == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes seleccionar una ciudad.",
                    "Aceptar");
                return;
            }

            if (this.SelectedEspecialidad == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes seleccionar una especialidad.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.TelFijo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar teléfono fijo.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.TelCelular))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar teléfono celular.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar contraseña.",
                    "Aceptar");
                return;
            }
        
            if (this.Password.Length <8)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "La contraseña debe ser mínimo de 8 caracteres.",
                    "Aceptar");
                return;
            }

            if (!IsValidPassword(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "La contraseña debe contenter como mínimo una letra mayúscula, una minúscula, un número y un caracter especial.",
                    "Aceptar");
                return;
            }

            var vMainViewModel = MainViewModel.GetInstance();

            Request oRequest = new Request();
            oRequest.NombresApellidos = this.Nombres;
            oRequest.Correo = this.Correo;
            oRequest.Contrasnia = this.Password;
            oRequest.IdCiudad = this.SelectedCiudad.Id_ciudad.ToString();
            oRequest.IdEspecialidad= this.SelectedEspecialidad.Id_especialidad.ToString();
            oRequest.Rol= "Medico";
            oRequest.TelefonoCelular = this.TelCelular;
            oRequest.TelefonoFijo = this.TelFijo;//this.Medicamentos[this.MedicamentosSelectedIndex];

            var request = await this.apiService.Request(oRequest);
            Int32 intMedicoId = 0;

            if (Int32.TryParse(request.Id, out intMedicoId))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Éxito",
                    "El usuario fue solicitado satisfactoriamente.",
                    "Aceptar");

                this.Nombres = string.Empty;
                this.SelectedCiudad = null;
                this.SelectedEspecialidad = null;
                this.TelCelular = string.Empty;
                this.TelFijo = string.Empty;
            }
            else
            {
                if (request != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   request.Id.Replace("Message: ", ""),
                   "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Algo estuvo mal, por favor intente después.",
                   "Aceptar");
                }
            }

            vMainViewModel.Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.\d)(?=.[\u0021-\u002b\u003c-\u0040])(?=.[A-Z])(?=.[a-z])\S{8,16}$");
            Match match = regex.Match(password);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Constructors
        public RegistrarViewModel()
        {
            this.apiService = new ApiService();
            loadData();
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion

        #region Methods
        private async void loadData()
        {
            //medicamentos = await this.apiService.Medicamentos();
            List<Ciudad> _ciudades = await this.apiService.Ciudades("null");
            this.Ciudades = new ObservableCollection<Ciudad>(_ciudades);

            List<Especialidad> _especialidades = await this.apiService.Especialidades();
            this.Especialidades = new ObservableCollection<Especialidad>(_especialidades);
        }
        #endregion
    }
}