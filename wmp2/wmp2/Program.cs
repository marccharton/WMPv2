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

            Library lib = new Library("library.xml");
            lib.Init();

            lib.ImportDir(@"E:\Programs Files\Itunes\Music");
            lib.ImportFile(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a");

            Console.WriteLine("Voici la liste des artistes :");
            foreach (Artist art in lib.Artists)
                Console.WriteLine(art.Name);

            Console.WriteLine("J'ai trouve " + lib.Artists.Count + " Artist(s)");
            Console.WriteLine("J'ai trouve " + lib.Albums.Count + " Album(s)");
            Console.WriteLine("J'ai trouve " + lib.Songs.Count + " Song(s)");

            #endregion


            #region playlist
            
            Playlist p = new Playlist() { Name = "Ma super playlist" , Description = "Alors c'est une playlist qui envoie grave du lourd parcque ben tout simplement parceque elle est trop bien"};
            p.AddSong(lib.GetSongWithPath(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a"));
            p.AddSong(lib.GetSongWithPath(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a"));
            p.AddSong(lib.GetSongWithPath(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a"));
            p.AddSong(lib.GetSongWithPath(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a"));
            p.AddSong(lib.GetSongWithPath(@"E:\Programs Files\Itunes\Music\Bumblefoot\Normal\04 Rockstar For a Day.m4a"));
            
            p.Serialize();
            lib.Playlists.Add(p);
            
            Console.WriteLine(p.toString());
            
            #endregion

            // Affichage des arguments
            foreach (string str in args)
                Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
