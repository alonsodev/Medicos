using Medicos.Models;
using Medicos.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Medicos.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public List<PacienteView> PacientesList
        {
            get;
            set;
        }

        public PacienteView PacienteInfo
        {
            get;
            set;
        }

        public List<Notificacion> NotificacionesList
        {
            get;
            set;
        }

        public Paciente Paciente
        {
            get;
            set;
        }
        public TokenResponse Token
        {
            get;
            set;
        }

        public String Usuario
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public PdfViewerViewModel PdfViewer
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public RegistrarViewModel Registrar
        {
            get;
            set;
        }

        public MainMenuViewModel MainMenu
        {
            get;
            set;
        }

        public ReportarPacienteViewModel ReportarPaciente
        {
            get;
            set;
        }

        public PacienteGraficoViewModel PacienteGrafico
        {
            get;
            set;
        }
        

        public ConsentimientoInformadoViewModel ConsentimientoInformado
        {
            get;
            set;
        }

        public ConsentimientoInformadoFirmaViewModel ConsentimientoInformadoFirma
        {
            get;
            set;
        }

        public VerPacienteMenuViewModel VerPacienteMenu
        {
            get;
            set;
        }

        public BuscarPacienteViewModel BuscarPaciente
        {
            get;
            set;
        }
        public InformeConsolidadoViewModel InformeConsolidado
        {
            get;
            set;
        }

        public InformacionPacienteViewModel InformacionPaciente
        {
            get;
            set;
        }

        public BeneficiosViewModel Beneficios
        {
            get;
            set;
        }

        public EncuestaPaso1ViewModel EncuestaPaso1
        {
            get;
            set;
        }

        public NotificacionesViewModel Notificaciones
        {
            get;
            set;
        }


        #endregion

        #region Methods
        public void checkExistsSession()
        {
            if (instance.Token == null || instance.Usuario == null)
            {
                instance.Login = new LoginViewModel();
                Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Paciente = null;
            this.Login = new LoginViewModel();
        }


        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
