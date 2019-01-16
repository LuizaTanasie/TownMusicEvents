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

        public override bool Equals(object obj)
        {
            Genre g = (Genre)obj;
            return g.Id == this.Id;
        }

        public override int GetHashCode()
        {
            var hashCode = 651533481;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<User>>.Default.GetHashCode(Users);
            return hashCode;
        }
    }
}
