using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : Idable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public User()
        {
            Notifications = new HashSet<Notification>();
            Genres = new HashSet<Genre>();
        }
    }
}
