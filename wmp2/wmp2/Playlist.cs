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

        public bool AddSong(Song song)
        {
            // Song (path) exists ?
            IEnumerable<string> sgs = from sg in Songs
                                      where sg == song.Path
                                      select sg;
            // -> Yes
            if (sgs.Any())
            {   Console.WriteLine("La chanson existe deja..."); return false; }
            // -> No
            if (song != null)
                Songs.Add(song.Path);
            else
                Console.WriteLine("[Playlist] Tentative d'ajout d'une Song null");
            return true;
        }

        public void Unserialize(string name)
        {
            Playlist tmp = Serializer.Deserialize<Playlist>(Tools.DefaultPathFolderPlaylist + name, FileMode.Open) as Playlist;
            if (tmp == null)
                return;
            this.Name = tmp.Name;
            this.Description = tmp.Description;
            this.CreationDate = tmp.CreationDate;
            this.Like = tmp.Like;
            this.Songs = tmp.Songs;
        }

        public void Serialize()
        {
            Serializer.Serialize(this, Tools.DefaultPathFolderPlaylist + this.Name + ".xml", FileMode.OpenOrCreate, typeof(Playlist));
        }

        public override string ToString()
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
                    sb.Append(sg + "\n");
                }
            }
            return sb.ToString();
        }
    }
}
