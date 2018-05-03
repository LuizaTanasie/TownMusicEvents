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
        public string Spotify { get; set; }
        public string SoundCloud { get; set; }
        public string PictureUrl { get; set; }
        public string LastFmId { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<NewsItem> NewsItems { get; set; }
        public virtual User User { get; set; }

        public Artist()
        {
            Ratings = new HashSet<Rating>();
            NewsItems = new HashSet<NewsItem>();
        }

        public override bool Equals(object obj)
        {
            Artist artist = (Artist)obj;
            return artist.ArtistId == this.ArtistId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1585815464;
            hashCode = hashCode * -1521134295 + ArtistId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Biography);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Website);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(YouTube);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Facebook);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Instagram);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Twitter);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PictureUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Rating>>.Default.GetHashCode(Ratings);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<NewsItem>>.Default.GetHashCode(NewsItems);
            hashCode = hashCode * -1521134295 + EqualityComparer<User>.Default.GetHashCode(User);
            return hashCode;
        }
    }


}
