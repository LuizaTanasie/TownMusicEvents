using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Fan
    {
        [Key, ForeignKey("User")]
        public int FanId { get; set; }
        public string About { get; set; }
        public string PhotoUrl { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public Fan()
        {
            Ratings = new HashSet<Rating>();
        }

    }
}
