using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Helpers
{
    /// <summary>
    /// Resuelve la ruta del icono correspondiente a un tipo de incidencia.
    /// </summary>
    public static class IncidenceIconResolver
    {
        public static string GetIcon(string type)
        {
            switch (type)
            {
                case IncidenceTypes.SeguridadVial:
                    return "pack://application:,,,/Assets/seguridad_vial.png";

                case IncidenceTypes.OtrasIncidencias:
                    return "pack://application:,,,/Assets/otras_incidencias.png";

                case IncidenceTypes.VialidadInvernal:
                    return "pack://application:,,,/Assets/vialidad_invernal_tramos.png";

                case IncidenceTypes.PuertosMontana:
                    return "pack://application:,,,/Assets/puertos_de_montaña.png";

                case IncidenceTypes.Obras:
                    return "pack://application:,,,/Assets/obras.png";

                case IncidenceTypes.Accidente:
                    return "pack://application:,,,/Assets/accidente.png";

                default:
                    return "pack://application:,,,/Assets/otras_incidencias.png";
            }
        }
    }
}
