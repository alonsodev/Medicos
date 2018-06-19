using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class PacienteCantidad
    {
        private string _mes;
        [JsonProperty(PropertyName = "Mes")]
        public string Mes
        {
            get
            {
                return GetMonthName(_mes);
            }
            set
            {
                this._mes = value;
            }
        }
        [JsonProperty(PropertyName = "Cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty(PropertyName = "Anio")]
        public int Año { get; set; }

        private string GetMonthName(string mes)
        {
            switch (mes)
            {
                case "1":
                    return "Ene";
                case "2":
                    return "Feb";
                case "3":
                    return "Mar";
                case "4":
                    return "Abr";
                case "5":
                    return "May";
                case "6":
                    return "Jun";
                case "7":
                    return "Jul";
                case "8":
                    return "Ago";
                case "9":
                    return "Sep";
                case "10":
                    return "Oct";
                case "11":
                    return "Nov";
                case "12":
                    return "Dic";
                default:
                    return "Ene";
            }
        }
    }
}
