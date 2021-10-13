using System;
using System.Collections.Generic;

namespace MapAPI.Models
{
    public class Datapoint
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public string IPAddress { get; set; }
        public string Title { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Message { get; set; }
    }
}
