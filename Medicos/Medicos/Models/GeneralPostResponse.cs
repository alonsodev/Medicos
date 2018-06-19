using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System;
    using Newtonsoft.Json;
    public class GeneralPostResponse
    {
        #region Properties
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        #endregion
    }
}
