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

        public string Init()
        {
            string error = null;
            int i = 0;
            
            if (Unserialize() == false)
                return "Problem with the path of library : " + PathOfLibFile;
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
                    if (i < 20)
                    {
                        error += path + "\n";
                        i++;
                    }
                    else
                    {
                        error += "...";
                    }
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
                if (songTag.Duration != null) 
                    song.Duration = songTag.Duration;
            }
            
            if (songPath != null)
                song.Path = songPath;
            if (songPath != null)
                song.Name = Path.GetFileName(songPath);

            #region Match with Artist

            Artist curArt = null;

            if (songTag.Artist != null && songTag.Artist != "")
            {
                IEnumerable<Artist> artists = from a in Artists
                                              where a.Name.ToUpper() == songTag.Artist.ToUpper()
                                              select a;
                if (artists.Any())
                {
                    Artist art = artists.First();
                    song.Artist = art;
                    art.Songs.Add(song);
                    curArt = art;
                }
                else
                {
                    Artist art = new Artist() { Name = Tools.Capitalize(songTag.Artist) };
                    Artists.Add(art);
                    song.Artist = art;
                    art.Songs.Add(song);
                    curArt = art;
                }
            }
            else
            {
                IEnumerable<Artist> artists = from a in Artists
                                              where a.Name.ToUpper() == Tools.UnknownArtistName.ToUpper()
                                              select a;
                if (artists.Any())
                {
                    Artist art = artists.First();
                    song.Artist = art;
                    art.Songs.Add(song);
                    curArt = art;
                }
                else
                {
                    Artist art = new Artist() { Name = Tools.UnknownArtistName };
                    Artists.Add(art);
                    song.Artist = art;
                    art.Songs.Add(song);
                    curArt = art;
                }
            }

            #endregion

            #region Match with Album

            Album curAlb = null;

            if (songTag.Album != null && songTag.Album != "")
            {
                IEnumerable<Album> album = from a in Albums
                                           where a.Name.ToUpper() == songTag.Album.ToUpper()
                                           select a;
                if (album != null)
                {
                    if (album.Any())
                    {
                        curAlb = album.First();
                        song.Album = album.First();
                        curAlb.Songs.Add(song);
                    }
                    else
                    {
                        Album alb = new Album() { Name = Tools.Capitalize(songTag.Album) };
                        Albums.Add(alb);
                        song.Album = alb;
                        alb.Songs.Add(song);
                        curAlb = alb;
                    }
                }

            }
            else
            {
                IEnumerable<Album> album = from a in Albums
                                           where a.Name.ToUpper() == Tools.UnknownAlbumName.ToUpper()
                                           select a;
                if (album.Any())
                {
                    curAlb = album.First();
                    song.Album = album.First();
                    curAlb.Songs.Add(song);
                }
                else
                {
                    Album alb = new Album() { Name = Tools.UnknownAlbumName };
                    Albums.Add(alb);
                    song.Album = alb;
                    alb.Songs.Add(song);
                    curAlb = alb;
                }
            }

            #endregion

            #region Match with Genre

            String curGenre = null;

            if (songTag.Genre != null && songTag.Genre != "")
            {
                IEnumerable<String> genres = from g in Genres
                                             where g.ToUpper() == songTag.Genre.ToUpper()
                                             select g;
                if (genres.Any())
                {
                    String gnr = genres.First();
                    song.Genre = gnr;
                    curGenre = gnr;
                }
                else
                {
                    String gnr = Tools.Capitalize(songTag.Genre);
                    Genres.Add(gnr);
                    song.Genre = gnr;
                    curGenre = gnr;
                }
            }
            else
            {
                IEnumerable<String> genres = from g in Genres
                                             where g.ToUpper() == Tools.UnknownGenreName.ToUpper()
                                             select g;
                if (genres.Any())
                {
                    String gnr = genres.First();
                    song.Genre = gnr;
                    curGenre = gnr;
                }
                else
                {
                    String gnr = Tools.Capitalize(Tools.UnknownGenreName);
                    Genres.Add(gnr);
                    song.Genre = gnr;
                    curGenre = gnr;
                }
            }
            #endregion


            if (curArt != null && curAlb != null)
            {
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
            }

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
                {
                    this.Songs.Add(CreateSong(path));
                    MediaPaths.Add(path);
                }
                else if (Tools.CheckFormat(path) == Tools.Format.VIDEO)
                {
                    this.Videos.Add(new Video(path));
                    MediaPaths.Add(path);
                }
                else if (Tools.CheckFormat(path) == Tools.Format.PICTURE)
                {
                    this.Pictures.Add(new Picture(path));
                    MediaPaths.Add(path);
                }
                
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
            {
                try
                {
                    s = CreateSong(path);
                }
                catch
                {
                    return s;
                }
            }
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

        public List<Album> GetAlbumsByGenre(string genre)
        {
            List<Album> ret = new List<Album>();

            foreach (Album alb in Albums)
            {
                IEnumerable<Song> linqSongs = from sg in alb.Songs
                                              where sg.Genre.ToUpper() == genre.ToUpper()
                                              select sg;

                if (linqSongs.Any())
                    ret.Add(alb);
            }

            return ret;
        }

        public List<Artist> GetArtistsByGenre(string genre)
        {
            List<Artist> ret = new List<Artist>();
            bool genreFound = false;

            foreach (Artist art in Artists)
            {
                foreach (Album alb in art.Albums)
                {
                    IEnumerable<Song> linqSongs = from sg in alb.Songs
                                                  where sg.Genre.ToUpper() == genre.ToUpper()
                                                  select sg;

                    if (linqSongs.Any())
                        genreFound = true;
                }
                if (genreFound == true)
                {
                    ret.Add(art);
                    genreFound = false;
                }
            }
            

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
                        List<Song> newList = new List<Song>(); 
                        Console.WriteLine(Path.GetFileName(file));
                        Playlist tmp = new Playlist();
                        tmp.Unserialize(Path.GetFileName(file));
                        foreach (Song sg in tmp.Songs)
                        {
                            Song sgToAdd = GetSongWithPath(sg.Path);
                            IEnumerable<Song> songInList = from s in Songs
                                                     where s.Path == sgToAdd.Path
                                                     select s;
                            if (sgToAdd != null)
                            {
                                if (!songInList.Any())
                                {
                                    Songs.Add(sgToAdd);
                                    MediaPaths.Add(sgToAdd.Path);
                                }
                                newList.Add(sgToAdd);
                            }
                        }
                        tmp.Songs = newList;
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
