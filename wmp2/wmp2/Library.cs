using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace wmp2
{
    public class Library
    {
        public string Name { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
        public List<Playlist> Playlists { get; set; }
        
        public List<string> SongPaths;
        List<string> validPaths;

        private string PathOfLibFile;

        public Library(string path)
        {
            PathOfLibFile = path;
            Name = "My Library";
            Songs = new List<Song>();
            Artists = new List<Artist>();
            Albums = new List<Album>();
            Playlists = new List<Playlist>();
            SongPaths = new List<string>();
            validPaths = new List<string>();
        }

        ~Library()
        {
            Serialize();
        }

        // Load Existing Files in xml file in Library
        public bool Init()
        {
           Unserialize();

           foreach (string path in SongPaths)
               Songs.Add(CreateSong(path));
            
            return true;
        }

        public bool Serialize()
        {
            Serializer.Serialize(SongPaths, PathOfLibFile, FileMode.OpenOrCreate, typeof(List<string>));
            return true;
        }

        public bool Unserialize()
        {   
            if (File.Exists(PathOfLibFile))
                using (var fs = new FileStream(PathOfLibFile, FileMode.Open))
                {
                    try
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<string>));
                        SongPaths = xml.Deserialize(fs) as List<string>;
                    }
                    catch
                    {
                        return false;
                    }
                }
            return true;
        }

        public Song CreateSong(string songPath)
        {
            // Je recupere mes supers Tags
            Tag songTag = new Tag(songPath);
            
            // Je rempli un objet Song avec les infos basiques
            Song song = new Song()
            { 
                Title = songTag.Title,
                Track = (int)songTag.Track,
                Genre = songTag.Genre,
                Path = songPath
            };

            #region Match with Artist

            Artist curArt = new Artist();
            
            // Does this artist already exist in list ?
            IEnumerable<Artist> artists = from a in Artists
                                          where a.Name == songTag.Artist
                                          select a;
            

            // yes -> Catch it and match with my current song
            if (artists.Any())
            {
                foreach (Artist art in artists)
                {
                    song.Artist = art;
                    art.Songs.Add(song);
                    curArt = art;
                    //Console.WriteLine("J'ai trouve l'artist il s'appelle bien : " + art.Name);
                }
            }
            // no -> Create a new artist et push it in my artists List
            else
            {
                Artist art = new Artist() { Name = songTag.Artist };
                Artists.Add(art);
                song.Artist = art;
                art.Songs.Add(song);
                curArt = art;
                //Console.WriteLine("L'artiste n'existait pas, j'ai donc du le creer : " + art.Name);
            }

            #endregion

            #region Match with Album

            Album curAlb = new Album();

            // Does this album already exist in list ?
            IEnumerable<Album> album = from a in Albums
                                       where a.Name == songTag.Album
                                       select a;

            // yes -> Catch it and match with my current song
            if (album.Any())
            {
                foreach (Album alb in album)
                {
                    song.Album = alb;
                    alb.Songs.Add(song);
                    curAlb = alb;
                    //Console.WriteLine("J'ai trouve l'album il s'appelle bien : " + alb.Name);
                }
            }
            // no -> Create a new album et push it in my albums List
            else
            {
                Album alb = new Album() { Name = songTag.Album };
                Albums.Add(alb);
                song.Album = alb;
                alb.Songs.Add(song);
                curAlb = alb;
                //Console.WriteLine("L'album n'existait pas, j'ai donc du le creer : " + alb.Name);
            }
            #endregion

            curArt.Albums.Add(curAlb);

            // peutetre idem avec genre
            return song;
        }

        public List<string> ScanRep(string path)
        {
            scanRep(path, 0);
            return validPaths;
        }
        private void scanRep(string path, int level)
        {
            String[] files = Directory.GetFiles(path);
            String[] reps = Directory.GetDirectories(path);
            
            foreach (String rep in reps)
            {
                //for (int i = 0; i < level; ++i)
                //    Console.Out.Write("  ");
                //Console.Out.WriteLine(Path.GetFileName(rep));
                scanRep(rep, level + 1);
            }

            foreach (String file in files)
            {
                if (Tools.checkFormat(Path.GetFileName(file)))
                {
                    //for (int i = 0; i < level; ++i)
                    //    Console.Out.Write("  ");
                    //Console.WriteLine(Path.GetFileName(file));
                    validPaths.Add(file);
                }
            }
        }

        public bool ImportDir(string pathToImport)
        {
            foreach (string path in ScanRep(pathToImport))
            {
                ImportFile(path);
            }
            return true;
        }

        public bool ImportFile(string path)
        {
            // If path already Exists ?
            IEnumerable<string> pathOfFile = from p in SongPaths
                                             where p == path
                                             select p;
            if (!pathOfFile.Any())
            {
                Songs.Add(CreateSong(path));
                SongPaths.Add(path);
            }
            return true;
        }

        public Song GetSongWithPath(string path)
        {
            IEnumerable<Song> enum_sg = from sg in Songs
                                        where sg.Path == path
                                        select sg;
            Song s = new Song();
            s = null;
            if (enum_sg.Any())
                foreach (Song sg in enum_sg)
                    s = sg;
            return s;
        }
    }
}
