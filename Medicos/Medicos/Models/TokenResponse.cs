using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System;
    using Newtonsoft.Json;

    public class TokenResponse
    {
        #region Properties
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "Rol")]
        public string Rol { get; set; }
        

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty(PropertyName = "NombreApellidos")]
        public string NombreApellidos { get; set; }
        [JsonProperty(PropertyName = "Especialidad")]
        public string Especialidad { get; set; }
        [JsonProperty(PropertyName = "CentroAtencion")]
        public string CentroAtencion { get; set; }
        [JsonProperty(PropertyName = "Ciudad")]
        public string Ciudad { get; set; }
        #endregion
    }
}
