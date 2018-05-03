using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class RatingModel
    {
        public int FanId { get; set; }
        public int ArtistId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}