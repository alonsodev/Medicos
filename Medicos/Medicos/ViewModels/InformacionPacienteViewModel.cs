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
    public class InformacionPacienteViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private string nombre;
        private string eps;
        private string medicamento;
        private string enfermera;
        private string ciudad;
        private string fecha;
        private string fechaEducacionTramitesAdmin;
        private string fechaEntregaMuestraMed;
        private string fechaUltimoSeguimientoTelefonico;
        private string fechaUltimoSeguimientoPresencial;
        private string fechaInicioTratamiento;
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
        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
        public string EPS
        {
            get { return this.eps; }
            set { SetValue(ref this.eps, value); }
        }
        public string Medicamento
        {
            get { return this.medicamento; }
            set { SetValue(ref this.medicamento, value); }
        }

        public string Enfermera
        {
            get { return this.enfermera; }
            set { SetValue(ref this.enfermera, value); }
        }

        public string Ciudad
        {
            get { return this.ciudad; }
            set { SetValue(ref this.ciudad, value); }
        }

        public string Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }

        public string FechaEducacionTramitesAdmin
        {
            get { return this.fechaEducacionTramitesAdmin; }
            set { SetValue(ref this.fechaEducacionTramitesAdmin, value); }
        }

        public string FechaEntregaMuestraMed
        {
            get { return this.fechaEntregaMuestraMed; }
            set { SetValue(ref this.fechaEntregaMuestraMed, value); }
        }

        public string FechaUltimoSeguimientoTelefonico
        {
            get { return this.fechaUltimoSeguimientoTelefonico; }
            set { SetValue(ref this.fechaUltimoSeguimientoTelefonico, value); }
        }

        public string FechaUltimoSeguimientoPresencial
        {
            get { return this.fechaUltimoSeguimientoPresencial; }
            set { SetValue(ref this.fechaUltimoSeguimientoPresencial, value); }
        }

        public string FechaInicioTratamiento
        {
            get { return this.fechaInicioTratamiento; }
            set { SetValue(ref this.fechaInicioTratamiento, value); }
        }
        #endregion

        #region Constructors
        public InformacionPacienteViewModel()
        {
            this.IsEnabled = true;
            this.LoadPaciente();
        }
        #endregion

        #region Methods
        private async void LoadPaciente()
        {
            PacienteView p = MainViewModel.GetInstance().PacienteInfo;
            this.Nombre = p.Nombres;
            this.EPS = p.Eps;
            this.Medicamento = p.Medicamento;
            this.Enfermera = p.Enfermera;
            this.Ciudad = p.Ciudad;
            this.Fecha = p.Fecha_Ingreso_PSP;
            this.FechaEducacionTramitesAdmin = p.Fecha_Educacion_Tramites_Admin;
            this.FechaEntregaMuestraMed = p.Fecha_Entrega_Muestra_Med;
            this.FechaUltimoSeguimientoTelefonico = p.Fecha_Ultimo_Seguimiento_Telefonico;
            this.FechaUltimoSeguimientoPresencial = p.Fecha_Ultimo_Seguimient_Presencial;
            this.FechaInicioTratamiento = p.Fecha_Inicio_Tratamiento;
        }
        #endregion
    }
}
