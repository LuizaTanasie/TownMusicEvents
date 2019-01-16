using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class AstistWithRatingModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Score { get; set; }
    }
}