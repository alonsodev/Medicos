using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Ano
    {
        [JsonProperty(PropertyName = "anovalor")]
        public int AnoValor { get; set; }

        [JsonProperty(PropertyName = "anostr")]
        public string AnoStr { get; set; }
    }
}
