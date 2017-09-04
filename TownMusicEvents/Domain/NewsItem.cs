using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class NewsItem : Idable
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string PhotoUrl { get; set; }
        public int Type { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
