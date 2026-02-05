using System;
using Newtonsoft.Json;
using TrafficDesktopApp.Helpers;

namespace TrafficDesktopApp.Models
{
    /// <summary>
    /// Representa una incidencia de tráfico en el sistema.
    /// </summary>
    public class Incidence
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// ID externo de la incidencia (usado para la restricción única en BD).
        /// </summary>
        [JsonProperty("externalId")]
        public int ExternalId { get; set; }

        /// <summary>
        /// Recurso asociado a la incidencia (fuente de información).
        /// </summary>
        [JsonProperty("recurso", NullValueHandling = NullValueHandling.Ignore)]
        public Recurso Recurso { get; set; }

        public bool ShouldSerializeRecurso() => false;

        /// <summary>
        /// ID del recurso para creación/edición (backend only field).
        /// </summary>
        [JsonProperty("recursoId", NullValueHandling = NullValueHandling.Ignore)]
        public int? RecursoId { get; set; }

        [JsonIgnore]
        public string Source => Recurso?.DescEs;

        [JsonProperty("provincia")]
        public string Province { get; set; }

        [JsonProperty("ciudad")]
        public string City { get; set; }

        /// <summary>
        /// Carretera donde se localiza la incidencia.
        /// </summary>
        [JsonProperty("carretera")]
        public string Road { get; set; }

        [JsonProperty("direccion")]
        public string Direction { get; set; }

        [JsonProperty("causa")]
        public string Cause { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la incidencia.
        /// </summary>
        [JsonProperty("fecIni", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartDate { get; set; }

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

        /// <summary>
        /// Descripción detallada de la incidencia.
        /// </summary>
        [JsonProperty("descripcion")]
        public string Description { get; set; }

        [JsonProperty("primeraInsercion")]
        public DateTime FirstInserted { get; set; }

        [JsonProperty("ultimaActualizacion")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("new")]
        public bool IsNew { get; set; }
    }

    /// <summary>
    /// Representa el recurso o fuente de donde proviene la información de la incidencia.
    /// </summary>
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
