using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ArtistStatisticsModel
    {
        public int HowMany { get; set; }
        public int ArtistId { get; set; }
        public int When { get; set; }
    }
}