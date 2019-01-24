using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Medicos.Services;
    using Medicos.Models;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
    using Syncfusion.SfAutoComplete.XForms;

    public class ReportarPacienteViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Ciudad> ciudades;
        //private ObservableCollection<Patologia> patologias;
        private string nombres;
        private string apellidos;
        private string nombresApellidos;
        private string numIdentificacion;
        private string telefono;
        private string telefonoCelular;
        private string formulaImg;
        private DateTime? fechaNacimiento = null;
        private string ciudadText;
        private string medicamentoText;
        private string epsText;

        //private int tiposDocumentoSelectedIndex = -1;
        /*private List<string> tiposDocumento = new List<string>
        {
            "Cédula",
            "Carnet de Extranjería",
            "Pasaporte",
        };*/
        private int medicamentosSelectedIndex = -1;
        private ObservableCollection<Medicamento> medicamentos;

        private int epssSelectedIndex = -1;
        private ObservableCollection<Eps> epss;
        #endregion

        #region Properties

        //public List<string> TiposDocumento => tiposDocumento;
        //public List<string> Medicamentos => medicamentos;
        //public List<string> Epss => epss;
        public string CiudadText
        {
            get { return this.ciudadText; }
            set
            {
                SetValue(ref this.ciudadText, value);
                if (string.IsNullOrEmpty(value))
                {
                    SelectedCiudad = null;
                }
            }
        }
        
        public string EpsText
        {
            get { return this.epsText; }
            set
            {
                SetValue(ref this.epsText, value);
                if (string.IsNullOrEmpty(value))
                {
                    SelectedEps = null;
                }
            }
        }
        
        public string MedicamentoText
        {
            get { return this.medicamentoText; }
            set
            {
                SetValue(ref this.medicamentoText, value);
                if (string.IsNullOrEmpty(value)){
                    SelectedMedicamento = null;
                }
            }
        }
        
        public ObservableCollection<Ciudad> Ciudades
        {
            get { return this.ciudades; }
            set { SetValue(ref this.ciudades, value); }
        }

        public Ciudad SelectedCiudad { get; set; }

        public ObservableCollection<Medicamento> Medicamentos {
            get { return this.medicamentos; }
            set
            {
                SetValue(ref this.medicamentos, value);
            }
        }

        private Medicamento selectedMedicamento;

        public Medicamento SelectedMedicamento {
            get {
                return selectedMedicamento;
            }
            set {
                selectedMedicamento = value;
                /*if(selectedMedicamento != null)
                {
                    loadPatologias(selectedMedicamento.Id_medicamento.ToString());
                }*/
                
            }
        }
        public Eps SelectedEps { get; set; }
        public ObservableCollection<Eps> Epss
        {
            get { return this.epss; }
            set { SetValue(ref this.epss, value); }
        }

        /*public Patologia SelectedPatologia { get; set; }
        public ObservableCollection<Patologia> Patologias
        {
            get { return this.patologias; }
            set { SetValue(ref this.patologias, value); }
        }*/
        public string FormulaImg
        {
            get;
            set;
        }
        
        public int MedicamentosSelectedIndex
        {
            get
            {
                return medicamentosSelectedIndex;
            }
            set
            {
                if (medicamentosSelectedIndex != value)
                {
                    medicamentosSelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(MedicamentosSelectedIndex));
                    //SelectedCountry = Countries[countriesSelectedIndex];
                }
            }
        }

        public int EpssSelectedIndex
        {
            get
            {
                return epssSelectedIndex;
            }
            set
            {
                if (epssSelectedIndex != value)
                {
                    epssSelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(EpssSelectedIndex));
                    //SelectedCountry = Countries[countriesSelectedIndex];
                }
            }
        }
        public DateTime? FechaNacimientoSelected
        {
            get {
                return this.fechaNacimiento; }
            set {
                SetValue(ref this.fechaNacimiento, value); }
        }
        
        public string NombresApellidos
        {
            get { return this.nombresApellidos; }
            set { SetValue(ref this.nombresApellidos, value); }
        }

        public string NumIdentificacion
        {
            get { return this.numIdentificacion; }
            set { SetValue(ref this.numIdentificacion, value); }
        }

        public string Telefono
        {
            get { return this.telefono; }
            set { SetValue(ref this.telefono, value); }
        }

        public string TelefonoCelular
        {
            get { return this.telefonoCelular; }
            set { SetValue(ref this.telefonoCelular, value); }
        }

        public string Apellidos
        {
            get { return this.apellidos; }
            set { SetValue(ref this.apellidos, value); }
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

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Constructors
        public ReportarPacienteViewModel()
        {

            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.loadData();
        }
        #endregion

        #region Methods
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } =
            (text, values) => values
                .Where(x => x.ToLower().StartsWith(text.ToLower()))
                .OrderBy(x => x)
                .ToList();

        /*private async void loadPatologias(string id_medicamento)
        {
            List<Patologia> _patologias = await this.apiService.Patologias(id_medicamento);
            this.Patologias = new ObservableCollection<Patologia>(_patologias);
        }*/
        private async void loadData()
        {
            var vMainViewModel = MainViewModel.GetInstance();

            if (vMainViewModel.Paciente == null)
            {
                vMainViewModel.Paciente = new Paciente();
            }

            List<Medicamento> _medicamentos = await this.apiService.Medicamentos();
            this.Medicamentos = new ObservableCollection<Medicamento>(_medicamentos);

            List<Eps> _epss = await this.apiService.Eps();
            this.Epss = new ObservableCollection<Eps>(_epss);

            List<Ciudad> _ciudades = await this.apiService.Ciudades("null");
            this.Ciudades = new ObservableCollection<Ciudad>(_ciudades);

            //this.NombresApellidos = 
            this.NombresApellidos = vMainViewModel.Paciente.Nombre;
            //this.Apellidos = vMainViewModel.Paciente.Apellido;
            if(vMainViewModel.Paciente.Id_ciudad != "N/A")
            {
                this.SelectedCiudad = _ciudades.Find(l => l.Id_ciudad.ToString() == vMainViewModel.Paciente.Id_ciudad);
            }
            if (vMainViewModel.Paciente.Id_eps != "N/A")
            {
                this.SelectedEps = _epss.Find(l => l.Id_eps.ToString() == vMainViewModel.Paciente.Id_eps);
            }
            if (vMainViewModel.Paciente.Id_medicamento != "N/A")
            {
                this.SelectedMedicamento = _medicamentos.Find(l => l.Id_medicamento.ToString() == vMainViewModel.Paciente.Id_medicamento);
            }

            if (vMainViewModel.Paciente.Fecha_nacimiento == "N/A" || string.IsNullOrEmpty(vMainViewModel.Paciente.Fecha_nacimiento))
            {
                this.FechaNacimientoSelected = null;
            }
            else
            {
                this.FechaNacimientoSelected = DateTime.ParseExact(vMainViewModel.Paciente.Fecha_nacimiento, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            this.NumIdentificacion = vMainViewModel.Paciente.Documento == "N/A" ? "" : vMainViewModel.Paciente.Documento;
            this.Telefono = vMainViewModel.Paciente.Telefono == "N/A" ? "" : vMainViewModel.Paciente.Telefono;
            this.TelefonoCelular = vMainViewModel.Paciente.Telefono_celular == "N/A" ? "" : vMainViewModel.Paciente.Telefono_celular;
        }
        #endregion

        #region Commands
        public ICommand ConsentimientoInformadoCommand
        {
            get
            {
                return new RelayCommand(ConsentimientoInformado);
            }
        }

        public ICommand TakePhotoCommand
        {
            get
            {
                return new RelayCommand(TakePhoto);
            }
        }

        public ICommand PickPhotoCommand
        {
            get
            {
                return new RelayCommand(PickPhoto);
            }
        }

        public ICommand ReportarPacienteCommand
        {
            get
            {
                return new RelayCommand(ReportarPaciente);
            }
        }

        private async void ReportarPaciente()
        {


            if (string.IsNullOrEmpty(this.NombresApellidos) || this.NombresApellidos.IndexOf(" ") <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar nombres y apellidos.",
                    "Aceptar");
                return;
            }

            /*
            if (string.IsNullOrEmpty(this.Apellidos))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar apellidos.",
                    "Aceptar");
                return;
            }
            */
            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar un teléfono.",
                    "Aceptar");
                return;
            }
            
            if(this.SelectedMedicamento == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes escoger un medicamento.",
                    "Aceptar");
                return;
            }

            if (this.SelectedEps == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes escoger una EPS.",
                    "Aceptar");
                return;
            }

            /*if (this.MedicamentosSelectedIndex == -1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes escoger un medicamento.",
                    "Aceptar");
                return;
            }*/

            /* if (this.TiposDocumentoSelectedIndex == -1)
             {
                 await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Tú debes escoger un tipo de documento.",
                     "Aceptar");
                 return;
             }*/

            if (string.IsNullOrEmpty(this.NumIdentificacion))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar un número de identificación.",
                    "Aceptar");
                return;
            }

            if (this.FechaNacimientoSelected == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar una fecha de nacimiento.",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }

            var vMainViewModel = MainViewModel.GetInstance();

            FillPacienteEntity();

            /*var paciente = await this.apiService.ReportarPaciente(vMainViewModel.Paciente);
            Int32 intPacienteId = 0;
            if (paciente.Id.Contains("id:"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Éxito",
                    "El paciente fue reportado satisfactoriamente.",
                    "Aceptar");

                vMainViewModel.MainMenu = new MainMenuViewModel();

                await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage());

                this.Nombres = string.Empty;
                this.Apellidos = string.Empty;
                this.Telefono = string.Empty;
                this.TelefonoCelular = string.Empty;
                this.NumIdentificacion = string.Empty;
            }
            else
            {
                if(paciente != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   paciente.Id,
                   "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Algo estuvo mal, por favor intente después.",
                   "Aceptar");
                }
            }*/

            vMainViewModel.ConsentimientoInformadoFirma = new ConsentimientoInformadoFirmaViewModel();

            await Application.Current.MainPage.Navigation.PushAsync(new ConsentimientoInformadoFirmaPage());

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        private void FillPacienteEntity()
        {
            var vMainViewModel = MainViewModel.GetInstance();

            Paciente oPaciente = vMainViewModel.Paciente;
            oPaciente.Nombre = this.NombresApellidos;
            oPaciente.Apellido = "";//"N/A";
            oPaciente.Telefono = this.Telefono;
            oPaciente.Tipo_documento = "";//"N/A";
            oPaciente.Documento = this.NumIdentificacion;
            oPaciente.Id_medicamento = this.SelectedMedicamento == null ? "N/A" : this.SelectedMedicamento.Id_medicamento.ToString();//this.Medicamentos[this.MedicamentosSelectedIndex];
            oPaciente.MedicamentoStr = this.SelectedMedicamento == null ? "N/A" : this.SelectedMedicamento.MedicamentoStr;
            oPaciente.Id_eps = this.SelectedEps == null ? "N/A" : this.SelectedEps.Id_eps.ToString(); //this.Epss[this.EpssSelectedIndex];
            oPaciente.EpsStr = this.SelectedEps == null ? "N/A" : SelectedEps.EpsStr;
            oPaciente.Formula_img = (string.IsNullOrEmpty(this.FormulaImg) ? "N/A" : this.FormulaImg);
            oPaciente.Correo = "";//"N/A";
            oPaciente.Usuario = vMainViewModel.Usuario;
            oPaciente.Id_canal = 100;
            oPaciente.Telefono_celular = this.TelefonoCelular;//(string.IsNullOrEmpty(this.TelefonoCelular) ? "N/A" : this.TelefonoCelular);
            oPaciente.Telefono_cuidador = "";//"N/A";
            oPaciente.Cuidador = "";//"N/A";
            oPaciente.Id_ciudad = this.SelectedCiudad != null ? this.SelectedCiudad.Id_ciudad.ToString() : "N/A";
            oPaciente.CiudadStr = this.SelectedCiudad == null ? "N/A" : SelectedCiudad.CiudadStr;
            oPaciente.Tipo_documento_cuidador = "";//"N/A";
            oPaciente.Documento_cuidador = "";//"N/A";
            oPaciente.Parentesco = "";//"N/A";
            oPaciente.Centro_atencion = "";//"N/A";
            oPaciente.Consentimiento_pdf = "";//"N/A";
            oPaciente.Id_patologia = "";//"N/A";
            oPaciente.PatologiaStr = "";//"N/A";
            oPaciente.Fecha_nacimiento = this.FechaNacimientoSelected.Value.ToString("yyyy-MM-dd");
        }

        private async void PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Fotos no compatibles.", ":( Permiso no otorgado a las fotos.", "Aceptar");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,

            });


            if (file == null)
                return;
            
            //var stream = file.GetStream();
            //file.Dispose();

            string strBase64 = "";
            using (var fs = new FileStream(file.Path, FileMode.Open, FileAccess.Read))
            {
                var imageData = new byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
                strBase64 = Convert.ToBase64String(imageData);
            }
            /*

            StreamReader reader = new StreamReader(stream);
            
            byte[] bytedata = System.Text.Encoding.Default.GetBytes(reader.ReadToEnd());

            string strBase64 = Convert.ToBase64String(bytedata);
            */
            this.FormulaImg = strBase64;
            file.Dispose();

            await Application.Current.MainPage.DisplayAlert(
                    "Éxito",
                    "La foto fue cargada satisfactoriamente.",
                    "Aceptar");
            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });*/

            /*
            string Path = @"C:\Backup\android.png";
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    string base64String = Convert.ToBase64String(m.ToArray());
                    Console.WriteLine(base64String);
                    Console.ReadLine();
                }
            }
            */

        }

        private async void TakePhoto()
        {
            
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No hay cámara.", ":( No hay cámara disponible.", "Aceptar");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 100,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });

            if (file == null)
                return;

            //var stream = file.GetStream();
            //file.Dispose();

            string strBase64 = "";
            using (var fs = new FileStream(file.Path, FileMode.Open, FileAccess.Read))
            {
                var imageData = new byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
                strBase64 = Convert.ToBase64String(imageData);
            }
            /*
            StreamReader reader = new StreamReader(stream);

            byte[] bytedata = System.Text.Encoding.Default.GetBytes(reader.ReadToEnd());

            string strBase64 = Convert.ToBase64String(bytedata);
            */
            this.FormulaImg = strBase64;
            file.Dispose();

            await Application.Current.MainPage.DisplayAlert(
                    "Éxito",
                    "La foto fue cargada satisfactoriamente.",
                    "Aceptar");

            //await Application.Current.MainPage.DisplayAlert("Ubicación de archivo", file.Path, "Aceptar");

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });*/
        }

        private async void ConsentimientoInformado()
        {
            if (string.IsNullOrEmpty(this.NombresApellidos) || this.NombresApellidos.IndexOf(" ") <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar nombres y apellidos.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar un teléfono.",
                    "Aceptar");
                return;
            }

            if (this.SelectedMedicamento == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes escoger un medicamento.",
                    "Aceptar");
                return;
            }

            if (this.SelectedEps == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes escoger una EPS.",
                    "Aceptar");
                return;
            }


            if (string.IsNullOrEmpty(this.NumIdentificacion))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar un número de identificación.",
                    "Aceptar");
                return;
            }

            if (this.FechaNacimientoSelected == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tú debes ingresar una fecha de nacimiento.",
                    "Aceptar");
                return;
            }


            this.IsRunning = true;
            this.IsEnabled = false;

            FillPacienteEntity();

            MainViewModel.GetInstance().ConsentimientoInformado = new ConsentimientoInformadoViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ConsentimientoInformadoPage());

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        #endregion
    }
}
