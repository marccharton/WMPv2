﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using wmp2;

namespace SlideBarMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Library _lib;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void LoadLibrary(object sender, RoutedEventArgs e)
        {
<<<<<<< .mine
            //Tools.DefaultPathFileLibrary = @"C:\Users\Marc\Documents\Depots\wmpv2\wmp2\wmp2\bin\Debug\library.xml";
            _lib = new Library(Tools.DefaultPathFileLibrary);
            _lib.Init();
=======
            //Tools.DefaultPathFileLibrary = @"C:\Users\Marc\Documents\Depots\wmpv2\wmp2\wmp2\bin\Debug\library.xml";
            //Library lib = new Library(Tools.DefaultPathFileLibrary);
            //lib.Init();
>>>>>>> .theirs

<<<<<<< .mine
            this.lstArtists.ItemsSource = _lib.Artists;



=======
            //lib.ImportDir(@"E:\Programs Files\Itunes\Music");
            //lib.ImportFile(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a");

            //this.lstArtists.ItemsSource = lib.Artists;
>>>>>>> .theirs
        }

        private void lstArtists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstArtists.SelectedItem != null)
            {
                Artist at = lstArtists.SelectedItem as Artist;
                //MessageBox.Show(at.Name);
                lstAlbums.ItemsSource = at.Albums;
                lstSongs.ItemsSource = null;
            }
        }

        private void lstAlbums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstAlbums.SelectedItem != null)
            {
                Album al = lstAlbums.SelectedItem as Album;
                //MessageBox.Show(al.Name);
                lstSongs.ItemsSource = al.Songs;
            }
        }

        private void lstSongs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstSongs.SelectedItem != null)
            {
                Song sg = lstSongs.SelectedItem as Song;
                MessageBox.Show(sg.ToString());
                CurrentList.addElement(sg.Path);
            }

        }

<<<<<<< .mine
        private void importDir_Click(object sender, EventHandler e)
        {
           _lib.ImportDir(@"E:\Programs Files\Itunes\Music");
        }

        private void importDir_Click(object sender, EventHandler e)
        {
            _lib.ImportFile(@"C:\Users\Marc\Google Drive\[Partages]\KramAyrtoogle\dotNet\BDD\Music\01 Normal.m4a");
        }


=======











>>>>>>> .theirs
    }
}
