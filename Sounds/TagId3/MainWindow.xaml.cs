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

namespace TagId3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string _songPath = @"E:\Programs Files\Itunes\Music\Eric Legnini\Eric Legnini - The Vox\" + @"02._Éric_Legnini_-_Joy.mp3";

        public MainWindow()
        {
            InitializeComponent();
            loadInformations();
            this.btnSubmit.IsEnabled = false;
        }
        
        private void boxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnSubmit.IsEnabled = true;
        }


        private void loadInformations()
        {
            TagLib.File file2;
            
            // Mp3Lib.Mp3File file;

            try
            {
                // file = new Mp3Lib.Mp3File(_songPath);
                file2 = TagLib.File.Create(_songPath);
                titleBox.Text = file2.Tag.Title;
                artistBox.Text = file2.Tag.AlbumArtists[0];
                albumBox.Text = file2.Tag.Album;
                composerBox.Text = file2.Tag.Composers[0];
                discBox.Text = file2.Tag.Disc + "";
                genreBox.Text = file2.Tag.Genres[0];
                imageBox.Text = file2.Tag.Pictures[0].MimeType;
                //if (file.TagHandler.Length.HasValue)
                //    lenghtBox.Text = string.Format("{0:mm-ss-tt}", );
                lyricsBox.Text = file2.Tag.Lyrics;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }

        private void applyModifications(object sender, RoutedEventArgs e)
        {
            TagLib.File file2;

            try
            {
                
                file2 = TagLib.File.Create(_songPath);
                //Mp3Lib.Mp3File file = new Mp3Lib.Mp3File(_songPath);
                file2.Tag.Title = titleBox.Text;
                file2.Tag.AlbumArtists[0] = artistBox.Text;
                file2.Tag.Album = albumBox.Text;
                file2.Tag.Composers[0] = composerBox.Text;
                file2.Tag.Disc= Convert.ToUInt32(discBox.Text);
                file2.Tag.Genres[0] = genreBox.Text;
                //file2.Tag.Pictures[0].MimeType = imageBox.Text = ;
                //if (file.TagHandler.Length.HasValue)
                //    lenghtBox.Text = string.Format("{0:mm-ss-tt}", );
                file2.Tag.Lyrics = lyricsBox.Text;
                
                file2.Save();
                this.btnSubmit.IsEnabled = false;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
            
        }

        private void validModifications(object sender, RoutedEventArgs e)
        {
            applyModifications(sender, e);
            quit();
        }
        
        private void cancel(object sender, RoutedEventArgs e)
        {
            quit();
        }

        private void quit()
        {
            this.errorLog.Text = "Bye bye !";
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void onClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMaximizeOrRestore(object sender, RoutedEventArgs e)
        {
            if (!(ResizeMode == System.Windows.ResizeMode.CanResize))
                return;
            if (WindowState == System.Windows.WindowState.Maximized)
                WindowState = System.Windows.WindowState.Normal;
            else if (WindowState == System.Windows.WindowState.Normal)
                WindowState = System.Windows.WindowState.Maximized;
        }

        private void OnMinimize(object sender, RoutedEventArgs e)
        {
            if (!(ResizeMode == System.Windows.ResizeMode.CanResize || ResizeMode == System.Windows.ResizeMode.CanMinimize))
                return;
            WindowState = System.Windows.WindowState.Minimized;
        }

    }
}
