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
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
        #endregion
    }
}
