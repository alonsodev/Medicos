using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Eps
    {
        [JsonProperty(PropertyName = "id_eps")]
        public int Id_eps { get; set; }

        [JsonProperty(PropertyName = "eps")]
        public string EpsStr { get; set; }
    }
}
