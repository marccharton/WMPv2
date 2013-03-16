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

        #endregion

        #region Binded Current Property

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

        #endregion


        public LibraryViewModel()
        {
            Lib = new Library(Tools.DefaultPathFileLibrary);

            try
            {
                string error = Lib.Init();
                if (error != null)
                    MessageBox.Show("Paths not Found :\n" + error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("At least one path couldn't be found");
            }

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
                    MessageBox.Show(_selectedAlbum.Songs.Count.ToString());
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
                Lib.ImportDir(@"E:\Programs Files\Itunes\Music");
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
                        MessageBox.Show(Lib.Artists.Count.ToString());
                        ArtistsLIST = Lib.Artists;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            #endregion            

            ArtistsLIST = Lib.Artists;
        }
    }

}
