using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class PacienteView
    {
        [JsonProperty(PropertyName = "Nombres")]
        public string Nombres { get; set; }

        [JsonProperty(PropertyName = "Ciudad")]
        public string Ciudad { get; set; }

        [JsonProperty(PropertyName = "Enfermera")]
        public string Enfermera { get; set; }

        [JsonProperty(PropertyName = "Eps")]
        public string Eps { get; set; }

        [JsonProperty(PropertyName = "Fecha_Educacion_Tramites_Admin")]
        public string Fecha_Educacion_Tramites_Admin { get; set; }

        [JsonProperty(PropertyName = "Fecha_Entrega_Muestra_Med")]
        public string Fecha_Entrega_Muestra_Med { get; set; }

        [JsonProperty(PropertyName = "Fecha_Ingreso_PSP")]
        public string Fecha_Ingreso_PSP { get; set; }

        [JsonProperty(PropertyName = "Fecha_Inicio_Tratamiento")]
        public string Fecha_Inicio_Tratamiento { get; set; }

        [JsonProperty(PropertyName = "Fecha_Ultimo_Seguimient_Presencial")]
        public string Fecha_Ultimo_Seguimient_Presencial { get; set; }

        [JsonProperty(PropertyName = "Fecha_Ultimo_Seguimiento_Telefonico")]
        public string Fecha_Ultimo_Seguimiento_Telefonico { get; set; }

        [JsonProperty(PropertyName = "Documento")]
        public string Documento { get; set; }

        [JsonProperty(PropertyName = "Medicamento")]
        public string Medicamento { get; set; }
    }
}
