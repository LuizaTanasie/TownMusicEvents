using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rating : Idable
    {
        public int FanId { get; set; }
        public int ArtistId { get; set; }
        public virtual Fan Fan { get; set; }
        public virtual Artist Artist { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public override bool Equals(object obj)
        {
            Rating rating = ((Rating)obj);
            return FanId == rating.FanId && ArtistId == rating.ArtistId;
        }

        public override int GetHashCode()
        {
            var hashCode = -447199517;
            hashCode = hashCode * -1521134295 + FanId.GetHashCode();
            hashCode = hashCode * -1521134295 + ArtistId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Fan>.Default.GetHashCode(Fan);
            hashCode = hashCode * -1521134295 + EqualityComparer<Artist>.Default.GetHashCode(Artist);
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            return hashCode;
        }
    }
}
