using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Message : Idable
    {
        public string Text { get; set; }
        public DateTime DateAndTimeSent { get; set; }
        public int ArtistId { get; set; }
        public int VenueId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
