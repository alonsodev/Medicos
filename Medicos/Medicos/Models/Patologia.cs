using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Patologia
    {
        [JsonProperty(PropertyName = "id_patologia")]
        public int id_patologia { get; set; }

        [JsonProperty(PropertyName = "patologia")]
        public string PatologiaStr { get; set; }
        [JsonProperty(PropertyName = "id_medicamento")]
        public string Id_medicamento { get; set; }
    }
}
