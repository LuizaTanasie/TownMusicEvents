using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Concert : Idable
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string PhotoUrl { get; set; }
        public string BuyTicketsLink { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public virtual ICollection<Fan> Fans { get; set; }
        public Concert()
        {
            Fans = new HashSet<Fan>();
        }
    }
}
