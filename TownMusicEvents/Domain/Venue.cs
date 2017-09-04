using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Venue
    {
        [Key, ForeignKey("User")]
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OpenHours { get; set; }
        public string Picture1Url { get; set; }
        public string Picture2Url { get; set; }
        public string Picture3Url { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Concert> Concerts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Venue()
        {
            Artists = new HashSet<Artist>();
            Concerts = new HashSet<Concert>();
            Messages = new HashSet<Message>();
        }
    }
}
