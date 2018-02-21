using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Website { get; set; }
        public string YouTube { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Picture1Url { get; set; }
        public string Picture2Url { get; set; }
        public string Picture3Url { get; set; }
        public string Picture4Url { get; set; }
        public string Picture5Url { get; set; }
    }
}