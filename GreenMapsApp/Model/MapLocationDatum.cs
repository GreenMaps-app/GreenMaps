using System;

namespace GreenMapsApp.Model
{
    public class MapLocationDatum
    {
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public string ipAddress { get; set; }
        public string title { get; set; }
        public bool resolved { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string message { get; set; }
        public string severity { get; set; }
    }
}
