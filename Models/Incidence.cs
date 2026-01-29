using System;
using Newtonsoft.Json;
using TrafficDesktopApp.Helpers;

namespace TrafficDesktopApp.Models
{
    public class Incidence
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("recurso")]
        public Recurso Recurso { get; set; }

        [JsonIgnore]
        public string Source => Recurso?.DescEs;

        [JsonProperty("provincia")]
        public string Province { get; set; }

        [JsonProperty("ciudad")]
        public string City { get; set; }

        [JsonProperty("carretera")]
        public string Road { get; set; }

        [JsonProperty("direccion")]
        public string Direction { get; set; }

        [JsonProperty("causa")]
        public string Cause { get; set; }

        [JsonProperty("fecIni")]
        public DateTime StartDate { get; set; }

        [JsonProperty("fecFin")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("latitud")]
        public double Latitude { get; set; }

        [JsonProperty("longitud")]
        public double Longitude { get; set; }

        [JsonProperty("nivel")]
        public string Level { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("descripcion")]
        public string Description { get; set; }

        [JsonProperty("primeraInsercion")]
        public DateTime FirstInserted { get; set; }

        [JsonProperty("ultimaActualizacion")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("new")]
        public bool IsNew { get; set; }
    }

    public class Recurso
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("descEs")]
        public string DescEs { get; set; }

        [JsonProperty("descEu")]
        public string DescEu { get; set; }
    }
}
