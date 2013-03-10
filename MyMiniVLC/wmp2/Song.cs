using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace wmp2
{
    [Serializable]
    public class Song
    {
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public string Title { get; set; }
        [XmlIgnore]
        public Artist Artist { get; set; }
        [XmlIgnore]
        public Album Album { get; set; }
        [XmlIgnore]
        public string Genre { get; set; }
        [XmlIgnore]
        public int Like { get; set; }
        [XmlIgnore]
        public int Duration { get; set; }
        [XmlIgnore]
        public string Lyrics { get; set; }
        [XmlIgnore]
        public int Track { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Song]\n");
            sb.Append("Numero: " + Track + "\n");
            sb.Append("Title: " + Title + "\n");
            if (Artist != null)
                sb.Append("Artist: " + Artist.Name + "\n");
            if (Album != null)
                sb.Append("Album: " + Album.Name + "\n");
            sb.Append("Genre: " + Genre + "\n");
            sb.Append("Duration: " + Duration + "\n");
            sb.Append("Like: " + Like + " / 5\n");
            sb.Append("Path: " + Path + "\n");
            
            return sb.ToString();
        }
    };
}
