using System;
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
using Microsoft.Win32;

namespace SlideBarMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }

        //public void LoadLibrary(object sender, RoutedEventArgs e)
        //{

        //    //Tools.DefaultPathFileLibrary = @"C:\Users\Marc\Documents\Depots\wmpv2\wmp2\wmp2\bin\Debug\library.xml";
        //    _lib = new Library(Tools.DefaultPathFileLibrary);
        //    _lib.Init();

        //    //Tools.DefaultPathFileLibrary = @"C:\Users\Marc\Documents\Depots\wmpv2\wmp2\wmp2\bin\Debug\library.xml";
        //    //Library lib = new Library(Tools.DefaultPathFileLibrary);
        //    //lib.Init();

        //    this.lstArtists.ItemsSource = _lib.Artists;
        //}

        //private void poil()
        //{
        //    if (lstArtists.SelectedItem != null)
        //    {
        //        Artist at = lstArtists.SelectedItem as Artist;
        //        //MessageBox.Show(at.Name);
        //        lstAlbums.ItemsSource = at.Albums;
        //        lstSongs.ItemsSource = null;
        //    }
        //}
        
        //private void lstArtists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    poil();
        //}

        //private void lstAlbums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (lstAlbums.SelectedItem != null)
        //    {
        //        Album al = lstAlbums.SelectedItem as Album;
        //        //MessageBox.Show(al.Name);
        //        lstSongs.ItemsSource = al.Songs;
        //    }
        //}

        //private void lstSongs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    CurrentList curList = CurrentList.getInstance();

        //    if (lstSongs.SelectedItem != null)
        //    {
        //        Song sg = lstSongs.SelectedItem as Song;
        //        MessageBox.Show(sg.ToString());
        //        curList.addElement(sg.Path);
        //    }

        //}

        //private void btnImport_Click(object sender, RoutedEventArgs e)
        //{
        //    CurrentList curList = CurrentList.getInstance();
        //    OpenFileDialog fdlg = new OpenFileDialog();
            
        //    fdlg.Title = "Import File";
        //    fdlg.InitialDirectory = @"E:\";
        //    fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
        //    fdlg.FilterIndex = 2;
        //    fdlg.RestoreDirectory = true;
        //    fdlg.Multiselect = true;
        //    if (fdlg.ShowDialog() == true)
        //    {
        //        curList.ResetList();
        //        foreach (string name in fdlg.FileNames)
        //            curList.addElement(name);
        //        _lib.ImportFile(curList.getCurrentElement());
        //        // textBox1.Text = fdlg.FileName;
        //        poil();
        //    }
        //}

        //private void btnDir_Click(object sender, RoutedEventArgs e)
        //{
        //    _lib.ImportDir(@"E:\Programs Files\Itunes\Music");
        //}

    }
}
