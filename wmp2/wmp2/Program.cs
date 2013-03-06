using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wmp2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region library

            Library lib = new Library(Tools.DefaultPathFileLibrary);
            lib.Init();

            lib.ImportDir(@"E:\Programs Files\Itunes\Music");
            lib.ImportFile(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a");

            Console.WriteLine("Voici la liste des artistes :");
            foreach (Artist art in lib.Artists)
                Console.WriteLine(art.Name);

            Console.WriteLine("J'ai trouvé " + lib.Artists.Count + " Artist(s)");
            Console.WriteLine("J'ai trouvé " + lib.Albums.Count + " Album(s)");
            Console.WriteLine("J'ai trouvé " + lib.Songs.Count + " Song(s)");

            Console.WriteLine("La liste des songs de 'BUMBLEFOOT' : ");
            foreach (Song s in lib.GetSongsByArtist("BUMBLEFOOT"))
            {
                Console.WriteLine(s.Title + ", " + s.Artist.Name);
            }

            Console.WriteLine("La liste des songs de 'BUMBLEFOOT' : ");
            foreach (Album alb in lib.GetAlbumsByArtist("BUMBLEFOOT"))
            {
                Console.WriteLine(alb.Name);
            }

            #endregion


            #region playlist
            
            Console.WriteLine("--------");
            lib.OpenPlaylists();
            Console.WriteLine("--------");
            foreach (Playlist pl in lib.Playlists)
            {
                Console.WriteLine(pl.ToString());
            }

            Playlist p = lib.GetPLaylistWithName("Ma super playlist");

            Console.ReadKey();

            for (int i = 0; i < 4; ++i)
                p.AddSong(lib.GetSongWithPath(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a"));
            p.AddSong(lib.GetSongWithPath(@"E:\Programs Files\Itunes\Music\Bumblefoot\Normal\04 Rockstar For a Day.m4a"));

            p.Serialize();
            lib.Playlists.Add(p);

            Console.WriteLine(p.ToString());

            p.AddSong(lib.GetSongWithPath(@"E:\Programs Files\Itunes\Music\Bumblefoot\Normal\04 Rockstar For a Day.m4a"));
            p.Serialize();

            Console.WriteLine(p.ToString());

            #endregion

            // Affichage des arguments
            foreach (string str in args)
                Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
