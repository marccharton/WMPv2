using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace wmp2
{
    class Serializer
    {
        public static bool Serialize(Object obj, string path, FileMode fmode, Type type)
        {
            using (var fs = new FileStream(path, fmode))
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(type);
                    xml.Serialize(fs, obj);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static void Deserialize(Object obj, string path, FileMode fmode, Type type)
        {
            
        }
    }
}
