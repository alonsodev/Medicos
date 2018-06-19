using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Paciente
    {
        [JsonProperty(PropertyName = "id_paciente_reportado")]
        public int Id_paciente_reportado { get; set; }
        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "apellido")]
        public string Apellido { get; set; }

        [JsonProperty(PropertyName = "tipo_documento")]
        public string Tipo_documento { get; set; }

        [JsonProperty(PropertyName = "documento")]
        public string Documento { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        [JsonProperty(PropertyName = "telefono_celular")]
        public string Telefono_celular { get; set; }

        [JsonProperty(PropertyName = "correo")]
        public string Correo { get; set; }

        [JsonProperty(PropertyName = "id_ciudad")]
        public string Id_ciudad { get; set; }

        [JsonProperty(PropertyName = "id_medicamento")]
        public string Id_medicamento { get; set; }

        [JsonProperty(PropertyName = "id_eps")]
        public string Id_eps { get; set; }

        [JsonProperty(PropertyName = "formula_img")]
        public string Formula_img { get; set; }

        [JsonProperty(PropertyName = "cuidador")]
        public string Cuidador { get; set; }

        [JsonProperty(PropertyName = "tipo_documento_cuidador")]
        public string Tipo_documento_cuidador { get; set; }

        [JsonProperty(PropertyName = "documento_cuidador")]
        public string Documento_cuidador { get; set; }

        [JsonProperty(PropertyName = "parentesco")]
        public string Parentesco { get; set; }

        [JsonProperty(PropertyName = "telefono_cuidador")]
        public string Telefono_cuidador { get; set; }

        [JsonProperty(PropertyName = "centro_atencion")]
        public string Centro_atencion { get; set; }

        [JsonProperty(PropertyName = "consentimiento_pdf")]
        public string Consentimiento_pdf { get; set; }

        [JsonProperty(PropertyName = "usuario")]
        public string Usuario { get; set; }
        [JsonProperty(PropertyName = "fecha_registro")]
        public DateTime Fecha_registro { get; set; }

        [JsonProperty(PropertyName = "id_canal")]
        public int Id_canal { get; set; }

        [JsonProperty(PropertyName = "fecha_nacimiento")]
        public string Fecha_nacimiento { get; set; }
        [JsonProperty(PropertyName = "id_patologia")]
        public string Id_patologia { get; set; }
        public string CiudadStr { get; set; }
        public string PatologiaStr { get; set; }

        public string EpsStr { get; set; }
        public string MedicamentoStr { get; set; }
    }
}
