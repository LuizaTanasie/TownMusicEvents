using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Artist
    {
        [Key, ForeignKey("User")]
        public int ArtistId { get; set; }
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
        public virtual ICollection<Venue> Venues { get; set; }
        public virtual ICollection<Fan> Fans { get; set; }
        public virtual ICollection<Concert> Concerts { get; set; }
        public virtual ICollection<NewsItem> NewsItems { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual User User { get; set; }

        public Artist()
        {
            Venues = new HashSet<Venue>();
            Fans = new HashSet<Fan>();
            Concerts = new HashSet<Concert>();
            NewsItems = new HashSet<NewsItem>();
        }

        public override bool Equals(object obj)
        {
            Artist artist = (Artist)obj;
            return artist.ArtistId == this.ArtistId;
        }
    }


}
