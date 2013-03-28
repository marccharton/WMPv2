using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wmp2
{
    public class Video
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string PathOfFile { get; set; }

        public Video(string aPathOfFile)
        {
            PathOfFile = aPathOfFile;
            Name = Path.GetFileNameWithoutExtension(PathOfFile);
            Extension = Path.GetExtension(PathOfFile);
        }

    }
}
