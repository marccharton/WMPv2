using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wmp2
{
    class Tools
    {
        public static bool checkFormat(string name)
        {
            string extension = Path.GetExtension(name);

            if (extension == ".mp3" || extension == ".wav" || extension == ".flac" || extension == ".ogg" || extension == ".wav" || extension == ".m4a")
                return true;
            if (extension == ".wmv" || extension == ".avi" || extension == ".mpg" || extension == ".mov" || extension == ".mp4")
                return true;
            if (extension == ".jpg" || extension == ".bmp" || extension == ".gif" || extension == ".png" || extension == ".tiff")
                return true;
            return false;
        }
    }
}
