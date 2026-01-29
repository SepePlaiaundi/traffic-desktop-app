namespace TrafficDesktopApp.Models
{
    public static class IncidenceTypes
    {
        public const string SeguridadVial = "Seguridad vial";
        public const string OtrasIncidencias = "Otras incidencias";
        public const string VialidadInvernal = "Vialidad invernal tramos";
        public const string PuertosMontana = "Puertos de montaña";
        public const string Obras = "Obras";
        public const string Accidente = "Accidente";

        public static readonly string[] All =
        {
            SeguridadVial,
            OtrasIncidencias,
            VialidadInvernal,
            PuertosMontana,
            Obras,
            Accidente
        };
    }
}
