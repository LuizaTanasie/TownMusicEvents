using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RecommendedArtist: Idable
    {
        public string Name { get; set; }
        public string Why { get; set; }
        public double Score { get; set; }
        public string PictureUrl { get; set; }

        public override bool Equals(object obj)
        {
            RecommendedArtist r = (RecommendedArtist)obj;
            return r.Id == this.Id;
        }

        public override int GetHashCode()
        {
            var hashCode = 1290730678;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Why);
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PictureUrl);
            return hashCode;
        }
    }
}
