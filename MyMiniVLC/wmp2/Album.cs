using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wmp2
{
    public class Album
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public String Genre { get; set; }
        public Artist Artist { get; set; }
        public List<Song> Songs { get; set; }

        public Album()
        {
            Songs = new List<Song>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Album]\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Description: " + Description + "\n");
            sb.Append("Like: " + Like + " / 5\n");
            if (Songs != null)
            {
                sb.Append("{Songs}");
                foreach (Song song in Songs)
                    sb.Append("  " + song.ToString());
            }
            return sb.ToString();
        }
    };
}
