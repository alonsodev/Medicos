using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Medicamento
    {
        [JsonProperty(PropertyName = "id_medicamento")]
        public int Id_medicamento { get; set; }

        [JsonProperty(PropertyName = "medicamento")]
        public string MedicamentoStr { get; set; }
    }
}
