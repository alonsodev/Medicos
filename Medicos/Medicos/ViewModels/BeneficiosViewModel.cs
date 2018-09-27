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

    public class BeneficiosViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private string beneficioStr;
        #endregion

        #region Properties
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

        public string BeneficioStr
        {
            get { return this.beneficioStr; }
            set { SetValue(ref this.beneficioStr, value); }
        }
        #endregion

        #region Constructors
        public BeneficiosViewModel()
        {

            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadBeneficio();
        }
        #endregion

        #region Methods
        private async void LoadBeneficio()
        {
            List<Beneficio> oBeneficio = await this.apiService.Beneficio();
            this.BeneficioStr = oBeneficio[0].BeneficioStr;//loadBeneficio();//

        }
        #endregion

        #region Commands
        public ICommand EncuestaPaso1Command
        {
            get
            {
                return new RelayCommand(EncuestaPaso1);
            }
        }

        private async void EncuestaPaso1()
        {
            MainViewModel.GetInstance().EncuestaPaso1 = new EncuestaPaso1ViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new EncuestaPaso1Page());
        }

        # endregion


        private string loadBeneficio()
        {
            return "Acompañamiento presencial y/o telefónico dureante el tratamiento. Asesoramiento en trámites para la obtención del medicamento. Soporte telefónico dirigido por profesionales de la salud, Entrega de material informativo, Educación en el manejo de la patología y el medicamento. Kit de inicio de tratamiento, Actividades lúdicas y grupales, Apoyo de servicio de transporte. Servicio de aplicación domiciliaria de medicamento. Consulta y kit dermatológico.";
        }
    }
}
