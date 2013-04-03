using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SlideBarMVVM
{
    public class CurrentListObject
    {
        public int Index { get; set; }
        public String Content { get; set; }
        public bool IsPlaying { get; set; }

        public CurrentListObject() 
        {
            this.IsPlaying = false;
        }

        public override string ToString()
        {
            if (this.Content == null)
                return ("");
            return (Path.GetFileName(this.Content));
        }

    }
}
