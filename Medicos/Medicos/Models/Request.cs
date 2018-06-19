using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Request
    {
        [JsonProperty(PropertyName = "nombres_apellidos")]
        public string NombresApellidos { get; set; }
        [JsonProperty(PropertyName = "correo")]
        public string Correo { get; set; }
        [JsonProperty(PropertyName = "id_especialidad")]
        public string IdEspecialidad { get; set; }
        [JsonProperty(PropertyName = "telefono_fijo")]
        public string TelefonoFijo { get; set; }
        [JsonProperty(PropertyName = "telefono_celular")]
        public string TelefonoCelular { get; set; }
        [JsonProperty(PropertyName = "id_ciudad")]
        public string IdCiudad { get; set; }
        [JsonProperty(PropertyName = "rol")]
        public string Rol { get; set; }
        [JsonProperty(PropertyName = "contrasnia")]
        public string Contrasnia { get; set; }
    }
}
