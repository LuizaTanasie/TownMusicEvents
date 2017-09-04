using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Notification : Idable
    {
        public int Type { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
