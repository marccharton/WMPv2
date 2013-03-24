using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using wmp2;
using System.Windows;
using System.IO;
using Microsoft.Win32;


namespace SlideBarMVVM
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Library Lib;

        #region Binded Property

        #region Binded List Property
        
        public List<String> _genresLIST;
        public List<String> GenresLIST
        {
            get
            {
                return this._genresLIST;
            }
            set
            {
                if (_genresLIST != value)
                {
                    this._genresLIST = value;
                    NotifyPropertyChanged("GenresLIST");
                }
            }
        }
        
        public List<Artist> _artistsLIST;
        public List<Artist> ArtistsLIST
        {
            get
            {
                return this._artistsLIST;
            }
            set
            {
                if (_artistsLIST != value)
                {
                    this._artistsLIST = value;
                    NotifyPropertyChanged("ArtistsLIST");
                }
            }
        }
        
        private List<Album> _albumsLIST;
        public List<Album> AlbumsLIST
        {
            get
            {
                return this._albumsLIST;
            }
            set
            {
                if (_albumsLIST != value)
                {
                    this._albumsLIST = value;
                    NotifyPropertyChanged("AlbumsLIST");
                }
            }
        }

        private List<Song> _songsLIST;
        public List<Song> SongsLIST
        {
            get
            {
                return this._songsLIST;
            }
            set
            {
                if (_songsLIST != value)
                {
                    this._songsLIST = value;
                    NotifyPropertyChanged("SongsLIST");
                }
            }
        }


        private List<Song> _listViewProvider;
        public List<Song> ListViewProvider
        {
            get { return _listViewProvider; }
            set
            {
                if (this._listViewProvider != value)
                {
                    this._listViewProvider = value;
                    NotifyPropertyChanged("ListViewProvider");
                }
            }
        }

        #endregion

        #region Binded Selected Property
        
        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get
            {
                return _selectedArtist;
            }
            set
            {
                if (_selectedArtist != value)
                {
                    _selectedArtist = value;
                    NotifyPropertyChanged("SelectedArtist");
                }
            }
        }

        private Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get
            {
                return _selectedAlbum;
            }
            set
            {
                if (_selectedAlbum != value)
                {
                    _selectedAlbum = value;
                    NotifyPropertyChanged("SelectedAlbum");
                }
            }
        }

        private Song _selectedSong;
        public Song SelectedSong
        {
            get
            {
                return _selectedSong;
            }
            set
            {
                if (_selectedSong != value)
                {
                    _selectedSong = value;
                    NotifyPropertyChanged("SelectedSong");
                }
            }
        }

        #endregion

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        #endregion


        #region Binded Command

        public Command LoadLibraryCMD { get; set; }
        public Command LoadArtistCMD { get; set; }
        public Command LoadAlbumCMD { get; set; }
        public Command PlaySongCMD { get; set; }
        public Command ImportDirectory { get; set; }
        public Command ImportFile { get; set; }
        public Command TestBindingCMD { get; set; }
        #endregion


        public LibraryViewModel()
        {
            Lib = new Library(Tools.DefaultPathFileLibrary);

            try
            {
                string error = Lib.Init();
                if (error != null)
                    MessageBox.Show("-- Paths not Found --\n" + error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("At least one path couldn't be found");
            }

            ListViewProvider = Lib.Songs;

            TestBindingCMD = new Command(new Action(() =>
            {
                MessageBox.Show("YO MON GARS CA PETE !!");
            }));

            #region Load Library

            LoadLibraryCMD = new Command(new Action(() =>
            {
                MessageBox.Show("Chargement de la library");
                //MessageBox.Show("Yo mon gars GG !!");
            }));

            #endregion

            #region Load Artist

            LoadArtistCMD = new Command(new Action(() =>
            {
                //MessageBox.Show("Valeur de l'artist selectionné : " + _selectedArtist.Name);
                if (_selectedArtist != null)
                {
                    AlbumsLIST = _selectedArtist.Albums;
                    SongsLIST = null;
                }
            }));

            #endregion

            #region Load Album

            LoadAlbumCMD = new Command(new Action(() =>
            {
                //MessageBox.Show("Valeur de l'album selectionné : " + _selectedAlbum.Name);
                if (_selectedAlbum != null)
                {
                    SongsLIST = _selectedAlbum.Songs;
                    // MessageBox.Show("_selectedAlbum.Songs.Count = " + _selectedAlbum.Songs.Count.ToString());
                }
            }));

            #endregion

            #region Play Song

            PlaySongCMD = new Command(new Action(() =>
            {
                if (_selectedSong != null)
                {
                    MessageBox.Show("Valeur de la song selectionnée : " + _selectedSong.Title + Path.GetFullPath(_selectedSong.Path));
                    CurrentList.getInstance().addElement(Path.GetFullPath(SelectedSong.Path));
                }
            }));

            #endregion

            #region Import Directory

            ImportDirectory = new Command(new Action(() =>
            {
                //Lib.ImportDir(@"E:\Programs Files\Itunes\Music");

                //var dialog = new System.Windows.Forms.FolderBrowserDialog();
                //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }));

            #endregion

            #region Import File

            ImportFile = new Command(new Action(() =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = "All files (*.*)|*.*";
                try
                {
                    if (ofd.ShowDialog() == true)
                    {
                        foreach (string name in ofd.FileNames)
                            Lib.ImportFile(name);

                        MessageBox.Show("Lib.Artists.Count = " + Lib.Artists.Count.ToString());

                        string s = "";
                        foreach (Artist a in Lib.Artists)
                            s += a.Name + "\n";
                        MessageBox.Show("Artists : \n" + s);
                        RefreshLibrary();
                        MessageBox.Show("ArtistsLIST.Count = " + ArtistsLIST.Count.ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            #endregion

            GenresLIST = Lib.Genres;
            ArtistsLIST = Lib.Artists;
            AlbumsLIST = Lib.Albums;
        }

        private void RefreshLibrary()
        {
            ArtistsLIST = null;
            ArtistsLIST = Lib.Artists;
            AlbumsLIST = null;
            SongsLIST = null;
        }
    }

}
