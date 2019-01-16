using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class VisitModel
    {
        public int FanId { get; set; }
        public int ArtistId { get; set; }
        public bool HasClickedALink { get; set; }
        public DateTime Date { get; set; }
    }
}