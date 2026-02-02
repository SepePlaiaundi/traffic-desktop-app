using System;

namespace TrafficDesktopApp.Helpers
{
    /// <summary>
    /// Utilidad para la conversión de coordenadas geográficas.
    /// </summary>
    public static class CoordinateConverter
    {
        // Bizkaia/Basque Country is usually in UTM Zone 30 North
        private const double EarthRadius = 6378137;

        /// <summary>
        /// Convierte coordenadas UTM (Easting/Northing) a WGS84 (Latitud/Longitud).
        /// Específicamente configurado para la Zona UTM 30N (Estándar en el País Vasco).
        /// </summary>
        /// <param name="x">Coordenada X (Easting).</param>
        /// <param name="y">Coordenada Y (Northing).</param>
        /// <returns>Una tupla con la Latitud y Longitud calculadas.</returns>
        public static (double Latitude, double Longitude) UtmToWgs84(double x, double y)
        {
            // Specific parameters for ETRS89 / UTM zone 30N (Basque Country standard)
            int zone = 30;

            double c_sa = 6378137.000000;
            double c_sb = 6356752.314245;
            double e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            double e2cuadrada = Math.Pow(e2, 2);
            double c = Math.Pow(c_sa, 2) / c_sb;
            double x_final = x - 500000;
            double y_final = y;
            double s = ((zone * 6.0) - 183.0);
            double lat = y_final / (6366197.724 * 0.9996);
            double v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            double a = x_final / v;
            double a1 = Math.Sin(2 * lat);
            double a2 = a1 * Math.Pow(Math.Cos(lat), 2);
            double j2 = lat + (a1 / 2.0);
            double j4 = ((3 * j2) + a2) / 4.0;
            double j6 = ((5 * j4) + (a2 * Math.Pow(Math.Cos(lat), 2))) / 3.0;
            double alfa = (3.0 / 4.0) * e2cuadrada;
            double beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            double gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            double bm = 0.9996 * c * (lat - (alfa * j2) + (beta * j4) - (gama * j6));
            double b = (y_final - bm) / v;
            double epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow(Math.Cos(lat), 2);
            double eps = a * (1 - (epsi / 3.0));
            double nab = (b * (1 - epsi)) + lat;
            double senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            double delta = Math.Atan(senoheps / Math.Cos(nab));
            double tao = Math.Atan(Math.Cos(delta) * Math.Tan(nab));

            double longitude = (delta * (180.0 / Math.PI)) + s;
            double latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI));

            return (latitude, longitude);
        }
    }
}