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

        private String _selectedGenre;
        public String SelectedGenre
        {
            get
            {
                return _selectedGenre;
            }
            set
            {
                if (_selectedGenre != value)
                {
                    _selectedGenre = value;
                    NotifyPropertyChanged("SelectedGenre");
                }
            }
        }

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

        

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        #endregion


        #region Binded Command

        public Command LoadGenreCMD { get; set; }
        public Command LoadArtistCMD { get; set; }
        public Command LoadAlbumCMD { get; set; }
        public Command ImportDirectoryCMD { get; set; }
        public Command ImportFileCMD { get; set; }
        public Command PlayItemCMD { get; set; }
        
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

            SongsLIST = Lib.Songs;
            GenresLIST = Lib.Genres;
            ArtistsLIST = Lib.Artists;
            AlbumsLIST = Lib.Albums;


            #region Play Item

            PlayItemCMD = new Command(new Action(() =>
            {
                //MessageBox.Show("YO MON GARS CA PETE !!");
                CurrentList curList = CurrentList.getInstance();
                curList.ResetList();
                curList.addElement(Path.GetFullPath(SelectedSong.Path));
                curList.DropEvent(this, null);
            }));

            #endregion


            
            
            #region Load Genre

            LoadGenreCMD = new Command(new Action(() =>
            {
                MessageBox.Show("Valeur du genre selectionné : " + _selectedGenre);
                if (_selectedGenre != null)
                {
                    IEnumerable<Artist> selectedArtists = from art in Lib.Artists
                                                          where art.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                          select art;
                    
                    if (selectedArtists.Any())
                    {
                        //ArtistsLIST.Clear();
                        MessageBox.Show("Valeur du genre selectionné : " + _selectedGenre);
                        foreach (Artist art in selectedArtists)
                        {
                            MessageBox.Show("Valeur du genre selectionné : " + _selectedGenre);
                            ArtistsLIST.Add(art);
                        }   
                    }
                    // SongsLIST = null;
                }
            }));

            #endregion


            #region Load Artist

            LoadArtistCMD = new Command(new Action(() =>
            {
                //MessageBox.Show("Valeur de l'artist selectionné : " + _selectedArtist.Name);
                if (_selectedArtist != null)
                {
                    AlbumsLIST = _selectedArtist.Albums;
                    // SongsLIST = null;
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





            #region Import Directory

            ImportDirectoryCMD = new Command(new Action(() =>
            {
                //Lib.ImportDir(@"E:\Programs Files\Itunes\Music");

                //var dialog = new System.Windows.Forms.FolderBrowserDialog();
                //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }));

            #endregion




            #region Import File

            ImportFileCMD = new Command(new Action(() =>
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
