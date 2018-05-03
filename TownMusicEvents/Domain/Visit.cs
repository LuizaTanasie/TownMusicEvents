using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Visit : Idable
    {
        public int FanId { get; set; }
        public int ArtistId { get; set; }
        public virtual Fan Fan { get; set; }
        public virtual Artist Artist { get; set; }
        public bool HasClickedALink { get; set; }
        public DateTime Date { get; set; }
    }
}
