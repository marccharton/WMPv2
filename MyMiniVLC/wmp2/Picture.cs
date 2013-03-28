using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wmp2
{
    public class Picture
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string PathOfFile { get; set; }

        public Picture(string aPathOfFile)
        {
            PathOfFile = aPathOfFile;
            Name = Path.GetFileName(PathOfFile);
            Extension = Path.GetExtension(PathOfFile);
        }
    }
}
