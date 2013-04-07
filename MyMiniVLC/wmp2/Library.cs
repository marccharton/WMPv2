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
        public List<Video> Videos { get; set; }
        public List<Picture> Pictures { get; set; }
        
        public List<string> MediaPaths;
        private List<string> validPaths;

        private string PathOfLibFile;

        public Library(string path)
        {
            PathOfLibFile = path;
            Name = "My Library";
            Songs = new List<Song>();
            Artists = new List<Artist>();
            Albums = new List<Album>();
            Genres = new List<String>();
            Playlists = new List<Playlist>();
            MediaPaths = new List<String>();
            validPaths = new List<String>();
            Videos = new List<Video>();
            Pictures = new List<Picture>();
        }

        ~Library()
        {
            Serialize();
        }

        // Load Existing Files in xml file in Library
        public string Init()
        {
            string error = null;
            
            if (Unserialize() == false)
                return "Problem with " + PathOfLibFile;
            foreach (string path in MediaPaths)
            {
                try
                {
                    if (Tools.CheckFormat(path) == Tools.Format.MUSIC)
                        this.Songs.Add(CreateSong(path));
                    else if (Tools.CheckFormat(path) == Tools.Format.VIDEO)
                        this.Videos.Add(new Video(path));
                    else if (Tools.CheckFormat(path) == Tools.Format.PICTURE)
                        this.Pictures.Add(new Picture (path));
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
            Serializer.Serialize(MediaPaths, PathOfLibFile, FileMode.OpenOrCreate, typeof(List<string>));
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
                        MediaPaths = xml.Deserialize(fs) as List<string>;
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
            
            if (songTag != null)
            {
                song.Track = (int)songTag.Track;
                if (String.IsNullOrEmpty(songTag.Title))
                    song.Title = Path.GetFileName(songPath);
                else
                    song.Title = Tools.Capitalize(songTag.Title);
                if (songTag.Genre != null) 
                    song.Genre = Tools.Capitalize(songTag.Genre);
                if (songTag.Duration != null) 
                    song.Duration = songTag.Duration;
            }
            
            if (songPath != null)
                song.Path = songPath;
            if (songPath != null)
                song.Name = Path.GetFileName(songPath);


            #region Match with Artist

            Artist curArt = new Artist();
            
            // Does this artist already exist in list ?
            IEnumerable<Artist> artists = from a in Artists
                                          where a.Name.ToUpper() == songTag.Artist.ToUpper()
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
                Artist art = new Artist() { Name = Tools.Capitalize(songTag.Artist) };
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
                                       where a.Name.ToUpper() == songTag.Album.ToUpper()
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
                Album alb = new Album() { Name = Tools.Capitalize(songTag.Album) };
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
                                         where g.ToUpper() == songTag.Genre.ToUpper()
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
                String gnr = Tools.Capitalize(songTag.Genre);
                Genres.Add(gnr);
                song.Genre = gnr;
                curGenre = gnr;
                //Console.WriteLine("L'artiste n'existait pas, j'ai donc du le creer : " + art.Name);
            }

            #endregion


            IEnumerable<Album> artAlbum = from a in curArt.Albums
                                           where a.Name.ToUpper() == curAlb.Name.ToUpper()
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
                scanRep(rep, level + 1);

            foreach (String file in files)
                if (Tools.CheckFormat(file) != Tools.Format.NONE)
                    validPaths.Add(file);
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
            IEnumerable<string> pathOfFile = from p in MediaPaths
                                             where p == path
                                             select p;
            if (!pathOfFile.Any())
            {
                if (Tools.CheckFormat(path) == Tools.Format.MUSIC)
                    this.Songs.Add(CreateSong(path));
                else if (Tools.CheckFormat(path) == Tools.Format.VIDEO)
                    this.Videos.Add(new Video(path));
                else if (Tools.CheckFormat(path) == Tools.Format.PICTURE)
                    this.Pictures.Add(new Picture(path));
                MediaPaths.Add(path);
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
                                      where song.Album.Name.ToUpper() == album.ToUpper()
                                      select song;
            
            foreach (Song s in songs)
                ret.Add(s);
            
            return ret;
        }

        public List<Song> GetSongsByArtist(string artist)
        {
            List<Song> ret = new List<Song>();
            IEnumerable<Song> songs = from song in Songs
                                      where song.Artist.Name.ToUpper() == artist.ToUpper()
                                      select song;

            foreach (Song s in songs)
                ret.Add(s);

            return ret;
        }

        public List<Album> GetAlbumsByArtist(string artist)
        {
            List<Album> ret = new List<Album>();
            IEnumerable<Album> albums = from album in Albums
                                        where album.Artist.Name.ToUpper() == artist.ToUpper()
                                        select album;

            foreach (Album alb in albums)
                ret.Add(alb);

            return ret;
        }

        public void OpenPlaylists()
        {
            string path = Tools.DefaultPathFolderPlaylist;
            //MessageBox.Show(Path.GetFullPath(path));
            string[] files;

            if (Directory.Exists(Path.GetFullPath(path)))
            {
                files = Directory.GetFiles(Path.GetFullPath(path));
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
        }

        public bool AddPlaylist(string name, string desc)
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
            else
                return false;
            return true;
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
