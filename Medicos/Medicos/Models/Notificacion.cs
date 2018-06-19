using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Notificacion
    {
        [JsonProperty(PropertyName = "Fecha")]
        public string Fecha { get; set; }
        [JsonProperty(PropertyName = "Mensaje")]
        public string Mensaje { get; set; }
    }
}
