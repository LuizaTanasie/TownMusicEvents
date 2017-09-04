using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Genre : Idable
    {
        public string Name{ get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Genre()
        {
            Users = new HashSet<User>();
        }
    }
}
