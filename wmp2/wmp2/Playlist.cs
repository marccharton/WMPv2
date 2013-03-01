using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace wmp2
{
    [Serializable]
    public class Playlist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Like { get; set; }

        public List<string> Songs { get; set; }

        public Playlist()
        {
            CreationDate = DateTime.Now;
            Songs = new List<string>();
        }

        public bool AddSong(Song sg)
        {
            Songs.Add(sg.Path);
            return true;
        }

        public void Unserialize(string name)
        {
            Playlist tmp = new Playlist();
            
            using (var fs = new FileStream("Playlists/" + name, FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Playlist));
                tmp = xml.Deserialize(fs) as Playlist;
            }
            this.Name = tmp.Name;
            this.Description = tmp.Description;
            this.CreationDate = tmp.CreationDate;
            this.Like = tmp.Like;
        }

        public void Serialize()
        {
            Serializer.Serialize(this, @"Playlists/" + this.Name + ".xml", FileMode.OpenOrCreate, typeof(Playlist));
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Playlist]\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Description: " + Description + "\n");
            sb.Append("Like: " + Like + " / 5\n");

            if (Songs != null)
            {
                sb.Append("{Songs}\n");
                foreach (string sg in Songs)
                {
                    sb.Append(sg);
                }
            }
            return sb.ToString();
        }
    }
}
