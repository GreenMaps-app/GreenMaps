using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenMapsApp.Model
{
    public class RemoveFormat
    {
        [JsonProperty("username")]
        public int id { get; set; }
        [JsonProperty("password")]
        public bool status { get; set; }
    }
}
