using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace wmp2
{
    public class Tools
    {
        public static string DefaultPathFolderPlaylist = @"Playlists/";

        public static string DefaultPathFileLibrary = @"library.xml";

        public enum Format { NONE, MUSIC, VIDEO, PICTURE };
        
        public static string Capitalize(string Str)
        {
            Char[] ca = Str.ToCharArray();

            foreach (Match m in Regex.Matches(Str, @"\b[a-z]"))
                ca[m.Index] = Char.ToUpper(ca[m.Index]);

            return new String(ca);
        }

        public static Format CheckFormat(string name)
        {
            string extension = Path.GetExtension(name);

            if (extension == ".mp3" || extension == ".wav" || extension == ".flac" || extension == ".ogg" || extension == ".wma" || extension == ".m4a")
                return Format.MUSIC;
            if (extension == ".wmv" || extension == ".avi" || extension == ".mpg" || extension == ".mov" || extension == ".mp4")
                return Format.VIDEO;
            if (extension == ".jpg" || extension == ".bmp" || extension == ".gif" || extension == ".png" || extension == ".tiff")
                return Format.PICTURE;
            return Format.NONE;
        }

    }
}
