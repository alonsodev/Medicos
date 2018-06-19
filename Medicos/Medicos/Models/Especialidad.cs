using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Especialidad
    {
        [JsonProperty(PropertyName = "id_especialidad")]
        public int Id_especialidad { get; set; }

        [JsonProperty(PropertyName = "especialidad")]
        public string EspecialidadStr { get; set; }
    }
}
