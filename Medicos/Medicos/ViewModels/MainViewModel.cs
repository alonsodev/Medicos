using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login
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
        
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
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
