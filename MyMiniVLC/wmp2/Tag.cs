using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wmp2
{
    public class Tag
    {
        private TagLib.File File;

        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public uint Disc { get; set; }
        public uint Track { get; set; }
        public string Composer { get; set; }
        public string Lyrics { get; set; }
        public TimeSpan Duration { get; set; }
        
        public Tag(string songPath)
        {
            File = TagLib.File.Create(songPath);

            Title = File.Tag.Title;
            Artist = File.Tag.FirstPerformer;
            AlbumArtist = File.Tag.FirstAlbumArtist;
            Album = File.Tag.Album;
            Composer = File.Tag.FirstComposer;
            Disc = File.Tag.Disc;
            Track = File.Tag.Track;
            Genre = File.Tag.FirstGenre;
            Lyrics = File.Tag.Lyrics;
            Duration = File.Properties.Duration;
        }
    }
}
