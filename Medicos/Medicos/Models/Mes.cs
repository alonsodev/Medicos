using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Mes
    {
        [JsonProperty(PropertyName = "mesvalor")]
        public int MesValor { get; set; }

        [JsonProperty(PropertyName = "messtr")]
        public string MesStr { get; set; }
    }
}
