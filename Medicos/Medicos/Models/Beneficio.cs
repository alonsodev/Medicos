using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Beneficio
    {
        [JsonProperty(PropertyName = "Beneficio")]
        public string BeneficioStr { get; set; }
    }
}
