using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDesktopApp.Models
{
    public class Camera
    {
        public string CameraId { get; set; }
        public string CameraName { get; set; }
        public string Road { get; set; }
        public string Kilometer { get; set; }
        public string Address { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string SourceId { get; set; }
        public string UrlImage { get; set; }
    }
}
