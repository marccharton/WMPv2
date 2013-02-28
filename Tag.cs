   class Tag
    {
        private TagLib.File File;
        
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                File.Tag.Title = value;
                File.Save();
            }
        }

        private string artist { get; set; }
        public string Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                if (File.Tag.Performers != null)
                {
                    if (File.Tag.Performers.Length == 0)
                        File.Tag.Performers = new string[] { "", "" };
                    File.Tag.Performers[0] = value;
                }
                File.Save();
            }
        }

        private string albumArtist { get; set; }
        public string AlbumArtist
        {
            get { return albumArtist; }
            set
            {
                albumArtist = value;
                if (File.Tag.AlbumArtists != null)
                {
                    if (File.Tag.AlbumArtists.Length == 0)
                        File.Tag.AlbumArtists = new string[] { "", "" };
                    File.Tag.AlbumArtists[0] = value;
                }
                File.Save();
            }
        }

        private string album { get; set; }
        public string Album
        {
            get { return album; }
            set
            {
                album = value;
                File.Tag.Album = value;
                File.Save();
            }
        }

        private string genre { get; set; }
        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                if (File.Tag.Genres != null)
                {
                    if (File.Tag.Genres.Length == 0)
                        File.Tag.Genres = new string[] { "", "" };
                    File.Tag.Genres[0] = value;
                }
                File.Save();
            }
        }

        private uint disc { get; set; }
        public uint Disc
        {
            get { return disc; }
            set
            {
                disc = value;
                File.Tag.Disc = value;
                File.Save();
            }
        }


        private string composer { get; set; }
        public string Composer
        {
            get { return composer; }
            set
            {
                composer = value;
                if (File.Tag.Composers != null)
                {
                    if (File.Tag.Composers.Length == 0)
                        File.Tag.Composers = new string[] { "", "" };
                    File.Tag.Composers[0] = value;
                }
                File.Save();
            }
        }

        private string lyrics { get; set; }
        public string Lyrics
        {
            get { return lyrics; }
            set
            {
                lyrics = value;
                File.Tag.Lyrics = value;
                File.Save();
            }
        }

        public Tag(string songPath)
        {
            File = TagLib.File.Create(songPath);
            
            Title = File.Tag.Title;
            Artist = File.Tag.Performers[0];
            albumArtist = File.Tag.FirstAlbumArtist;
            Album = File.Tag.Album;
            Composer = File.Tag.FirstComposer;
            Disc = File.Tag.Disc;
            Genre = File.Tag.FirstGenre;
            Lyrics = File.Tag.Lyrics;
        }
    }