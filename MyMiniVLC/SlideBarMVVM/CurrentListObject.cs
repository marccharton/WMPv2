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

        public CurrentListObject() { }

        public override string ToString()
        {
            if (this.Content == null)
                return ("");
            return (Path.GetFileName(this.Content));
        }

    }
}
