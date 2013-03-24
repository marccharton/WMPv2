using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wmp2
{
    public class Artist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public String Genre { get; set; }
        public List<Song> Songs { get; set; }
        public List<Album> Albums { get; set; }

        public Artist()
        {
            Songs = new List<Song>();
            Albums = new List<Album>();
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Artist]\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Description: " + Description + "\n");
            sb.Append("Like: " + Like + " / 5\n");
            if (Albums != null)
            {
                sb.Append("{Albums}\n");
                foreach (Album alb in Albums)
                    sb.Append(alb.ToString() + "\n");
            }

            if (Songs != null)
            {
                sb.Append("{Songs}\n");
                foreach (Song song in Songs)
                    sb.Append("  " + song.ToString() + "\n");
            }
            return sb.ToString();
        }
    };
}
