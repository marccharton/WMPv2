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
        public List<String> Genres { get; set; }
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
            Genres = new List<string>();
            Playlists = new List<Playlist>();
            SongPaths = new List<string>();
            validPaths = new List<string>();
        }

        ~Library()
        {
            Serialize();

        }

        // Load Existing Files in xml file in Library
        public string Init()
        {
            Unserialize();
            string error = null;

            foreach (string path in SongPaths)
            {
                try
                {
                    Songs.Add(CreateSong(path));
                }
                catch (DirectoryNotFoundException)
                {
                    error += path + "\n";
                }
            }
            
            return error;
        }

        public bool Serialize()
        {
            Serializer.Serialize(SongPaths, PathOfLibFile, FileMode.OpenOrCreate, typeof(List<string>));
            return true;
        }

        public bool Unserialize()
        {
            Console.WriteLine("[deserialize] file : " + PathOfLibFile);
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
            Song song = new Song();
            if (songTag.Title != null) song.Title = songTag.Title.ToUpper();
                                       song.Track = (int)songTag.Track;
            if (songTag.Genre != null) song.Genre = songTag.Genre.ToUpper();
            if (songPath != null)      song.Path = songPath;
            if (songPath != null)      song.Name = Path.GetFileName(songPath);

            #region Match with Artist

            Artist curArt = new Artist();
            
            // Does this artist already exist in list ?
            IEnumerable<Artist> artists = from a in Artists
                                          where a.Name == songTag.Artist.ToUpper()
                                          select a;
            

            // yes -> Catch it and match with my current song
            if (artists.Any())
            {
                Artist art = artists.First();
                song.Artist = art;
                art.Songs.Add(song);
                curArt = art;
                //Console.WriteLine("J'ai trouve l'artist il s'appelle bien : " + art.Name);
            }
            // no -> Create a new artist et push it in my artists List
            else
            {
                Artist art = new Artist() { Name = songTag.Artist.ToUpper() };
                Artists.Add(art);
                song.Artist = art;
                art.Songs.Add(song);
                curArt = art;
                //Console.WriteLine("L'artiste n'existait pas, j'ai donc du le creer : " + art.Name);
            }

            #endregion

            #region Match with Album

            Album curAlb = new Album();

            Console.WriteLine("Est ce que Mon album existe ?");
            // Does this album already exist in list ?
            IEnumerable<Album> album = from a in Albums
                                       where a.Name == songTag.Album.ToUpper()
                                       select a;

            // yes -> Catch it and match with my current song
            if (album.Any())
            {
                Console.WriteLine("Mon album existe");
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
                Console.WriteLine("Mon album n'existe pas");
                Album alb = new Album() { Name = songTag.Album.ToUpper() };
                Albums.Add(alb);
                song.Album = alb;
                alb.Songs.Add(song);
                curAlb = alb;
                //Console.WriteLine("L'album n'existait pas, j'ai donc du le creer : " + alb.Name);
            }
            #endregion

            #region Match with Genre

            String curGenre;

            // Does this artist already exist in list ?
            IEnumerable<String> genres = from g in Genres
                                         where g == songTag.Genre.ToUpper()
                                         select g;


            // yes -> Catch it and match with my current song
            if (genres.Any())
            {
                String gnr = genres.First();
                song.Genre = gnr;
                curGenre = gnr;
                // Console.WriteLine("J'ai trouve l'artist il s'appelle bien : " + art.Name);
            }
            // no -> Create a new artist et push it in my artists List
            else
            {
                String gnr = songTag.Genre.ToUpper();
                Genres.Add(gnr);
                song.Genre = gnr;
                curGenre = gnr;
                //Console.WriteLine("L'artiste n'existait pas, j'ai donc du le creer : " + art.Name);
            }

            #endregion


            IEnumerable<Album> artAlbum = from a in curArt.Albums
                                           where a.Name == curAlb.Name.ToUpper()
                                           select a;
            if (!artAlbum.Any())
            {
                curArt.Albums.Add(curAlb);
                curArt.Genre = curGenre;

                curAlb.Artist = curArt;
                curAlb.Genre = curGenre;
            }

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
                s = enum_sg.First();
            else
                s = CreateSong(path);
            return s;
        }

        public List<Song> GetSongsByAlbum(string album)
        {
            List<Song> ret          = new List<Song>();
            IEnumerable<Song> songs = from song in Songs
                                      where song.Album.Name == album.ToUpper()
                                      select song;
            
            foreach (Song s in songs)
                ret.Add(s);
            
            return ret;
        }

        public List<Song> GetSongsByArtist(string artist)
        {
            List<Song> ret = new List<Song>();
            IEnumerable<Song> songs = from song in Songs
                                      where song.Artist.Name == artist.ToUpper()
                                      select song;

            foreach (Song s in songs)
                ret.Add(s);

            return ret;
        }

        public List<Album> GetAlbumsByArtist(string artist)
        {
            List<Album> ret = new List<Album>();
            IEnumerable<Album> albums = from album in Albums
                                        where album.Artist.Name == artist.ToUpper()
                                        select album;

            foreach (Album alb in albums)
                ret.Add(alb);

            return ret;
        }

        public void OpenPlaylists()
        {
            string path = Tools.DefaultPathFolderPlaylist;
            string[] files = Directory.GetFiles(path);

            foreach (String file in files)
            {
                if (Path.GetExtension(file) == ".xml")
                {
                    Console.WriteLine(Path.GetFileName(file));
                    Playlist tmp = new Playlist();
                    tmp.Unserialize(Path.GetFileName(file));
                    Playlists.Add(tmp);
                }
            }
        }

        public void AddPlaylist(string name, string desc)
        {
            IEnumerable<Playlist> pls = from pl in Playlists
                                        where pl.Name == name
                                        select pl;

            if (!pls.Any())
            {
                Playlists.Add(new Playlist()
                {
                    Name = name,
                    Description = desc
                });
            }
        }

        public Playlist GetPLaylistWithName(string name)
        {
            IEnumerable<Playlist> pls = from pl in Playlists
                                        where pl.Name == name
                                        select pl;

            if (!pls.Any())
                return null;
            return pls.First();
        }
    }
}
