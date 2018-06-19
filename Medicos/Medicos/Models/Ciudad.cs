using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Ciudad
    {
        [JsonProperty(PropertyName = "id_ciudad")]
        public int Id_ciudad { get; set; }

        [JsonProperty(PropertyName = "id_departamento")]
        public string Id_departamento { get; set; }

        [JsonProperty(PropertyName = "ciudad")]
        public string CiudadStr { get; set; }
    }
}
