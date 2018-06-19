using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos
{
    public class GlobalSetting
    {
        public const string AzureTag = "Azure";
        public const string MockTag = "Mock";
        public const string DefaultEndpoint = "https://ast1.solutions-app.com";

        private string _baseEndpoint;
        private static readonly GlobalSetting _instance = new GlobalSetting();

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";
            BaseEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string BaseEndpoint
        {
            get { return _baseEndpoint; }
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string AuthToken { get; set; }

        public string ReportarPacienteEndpoint { get; set; }

        public string GetPacienteEndpoint { get; set; }

        public string MedicamentosEndpoint { get; set; }
        public string EpsEndpoint { get; set; }
        public string CiudadesEndpoint { get; set; }

        public string TokenEndpoint { get; set; }
        public string RequestEndpoint { get; set; }
        public string EspecialidadesEndpoint { get; set; }
        public string NotificacionesEndpoint { get; set; }

        public string GetUltimosPacientesEndpoint { get; set; }
        public string BeneficiosEndpoint { get; set; }
        public string CantidadesEndpoint { get; set; }
        public string PatologiasEndpoint { get; set; }
  
        private void UpdateEndpoint(string baseEndpoint)
        {
            var wsBaseEndpoint = "WS";
            TokenEndpoint = $"{wsBaseEndpoint}/token";

            var apiBaseEndpoint = $"{wsBaseEndpoint}/api";
            ReportarPacienteEndpoint = $"{apiBaseEndpoint}/Pacientes/Reportar";
            GetPacienteEndpoint = $"{apiBaseEndpoint}/Pacientes/GetPacientes";
            MedicamentosEndpoint = $"{apiBaseEndpoint}/Catalogos/Medicamentos";
            EpsEndpoint = $"{apiBaseEndpoint}/Catalogos/Eps";
            CiudadesEndpoint = $"{apiBaseEndpoint}/Catalogos/Ciudades";
            RequestEndpoint = $"{apiBaseEndpoint}/Account/_Request";
            EspecialidadesEndpoint = $"{apiBaseEndpoint}/Catalogos/Especialidades";
            NotificacionesEndpoint = $"{apiBaseEndpoint}/Notificaciones/Notificacion";
            GetUltimosPacientesEndpoint = $"{apiBaseEndpoint}/Pacientes/GetUltimosPacientes";
            BeneficiosEndpoint = $"{apiBaseEndpoint}/Catalogos/Beneficios";
            PatologiasEndpoint = $"{apiBaseEndpoint}/Catalogos/Patologias";
            CantidadesEndpoint = $"{apiBaseEndpoint}/Pacientes/Cantidades";
        }
    }
}
