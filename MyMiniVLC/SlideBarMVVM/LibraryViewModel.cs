using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using wmp2;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Controls;


namespace SlideBarMVVM
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        Library Lib;
        
        public bool IsPlaylistMode = false;
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        

        public string _playlistName;
        public string PlaylistName
        {
            get
            {
                return this._playlistName;
            }
            set
            {
                if (_playlistName != value)
                {
                    this._playlistName = value;
                    NotifyPropertyChanged("PlaylistName");
                }
            }
        }
        
        public List<Playlist> _playlistsLIST;
        public List<Playlist> PlaylistsLIST
        {
            get
            {
                return this._playlistsLIST;
            }
            set
            {
                if (_playlistsLIST != value)
                {
                    this._playlistsLIST = value;
                    NotifyPropertyChanged("PlaylistsLIST");
                }
            }
        }

        public List<Playlist> _playlistsMenuItemList;
        public List<Playlist> PlaylistsMenuItemList
        {
            get
            {
                return this._playlistsMenuItemList;
            }
            set
            {
                if (_playlistsMenuItemList != value)
                {
                    this._playlistsMenuItemList = value;
                    NotifyPropertyChanged("PlaylistsMenuItemList");
                }
            }
        }
        

        private String _allGenresText;
        public String AllGenresText
        {
            get
            {
                return _allGenresText;
            }
            set
            {
                if (_allGenresText != value)
                {
                    _allGenresText = value;
                    NotifyPropertyChanged("AllGenresText");
                }
            }
        }

        private String _allArtistsText;
        public String AllArtistsText
        {
            get
            {
                return _allArtistsText;
            }
            set
            {
                if (_allArtistsText != value)
                {
                    _allArtistsText = value;
                    NotifyPropertyChanged("AllArtistsText");
                }
            }
        }

        private String _allAlbumsText;
        public String AllAlbumsText
        {
            get
            {
                return _allAlbumsText;
            }
            set
            {
                if (_allAlbumsText != value)
                {
                    _allAlbumsText = value;
                    NotifyPropertyChanged("AllAlbumsText");
                }
            }
        }



        public Boolean _showPlaylistList;
        public Boolean ShowPlaylistList
        {
            get
            {
                return this._showPlaylistList;
            }
            set
            {
                if (_showPlaylistList != value)
                {
                    this._showPlaylistList = value;
                    NotifyPropertyChanged("ShowPlaylistList");
                }
            }
        }

        public Boolean _showFilters;
        public Boolean ShowFilters
        {
            get
            {
                return this._showFilters;
            }
            set
            {
                if (_showFilters != value)
                {
                    this._showFilters = value;
                    NotifyPropertyChanged("ShowFilters");
                }
            }
        }
        

        public List<Picture> _picturesList;
        public List<Picture> PicturesList
        {
            get
            {
                return this._picturesList;
            }
            set
            {
                if (_picturesList != value)
                {
                    this._picturesList = value;
                    NotifyPropertyChanged("PicturesList");
                }
            }
        }

        public List<Video> _videosList;
        public List<Video> VideosList
        {
            get
            {
                return this._videosList;
            }
            set
            {
                if (_videosList != value)
                {
                    this._videosList = value;
                    NotifyPropertyChanged("VideosList");
                }
            }
        }


        private Picture _selectedPicture;
        public Picture SelectedPicture
        {
            get
            {
                return this._selectedPicture;
            }
            set
            {
                if (_selectedPicture != value)
                {
                    this._selectedPicture = value;
                    NotifyPropertyChanged("SelectedPicture");
                }
            }
        }
        
        private Video _selectedVideo;
        public Video SelectedVideo
        {
            get
            {
                return this._selectedVideo;
            }
            set
            {
                if (_selectedVideo != value)
                {
                    this._selectedVideo = value;
                    NotifyPropertyChanged("SelectedVideo");
                }
            }
        }

        
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

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get
            {
                return _selectedPlaylist;
            }
            set
            {
                if (_selectedPlaylist != value)
                {
                    _selectedPlaylist = value;
                    NotifyPropertyChanged("SelectedPlaylist");
                }
            }
        }
        


        public ICommand LoadGenreCMD { get; set; }
        public ICommand LoadArtistCMD { get; set; }
        public ICommand LoadAlbumCMD { get; set; }
        public ICommand LoadPlaylistCMD { get; set; }
        public ICommand LoadLibraryCMD { get; set; }

        public ICommand ImportDirectoryCMD { get; set; }
        public ICommand ImportFileCMD { get; set; }

        public ICommand PlaySongItemCMD { get; set; }
        public ICommand PlayVideoItemCMD { get; set; }
        public ICommand PlayPictureItemCMD { get; set; }
        
        public ICommand AllGenresCMD { get; set; }
        public ICommand AllArtistsCMD { get; set; }
        public ICommand AllAlbumsCMD { get; set; }
        
        
        public ICommand DeleteFileCMD { get; set; }
        public ICommand AddToCurrentListCMD { get; set; }
        

        public ICommand ShowPlaylistsListCMD { get; set; }
        public ICommand AddToPlaylistCMD { get; set; }
        public ICommand RunAddPlaylistModeCMD { get; set; }
        public ICommand AddPlaylistCMD { get; set; }

        public ICommand DeletePlaylistCMD { get; set; }
        public ICommand RenamePlaylistCMD { get; set; }
        public ICommand PlayPlaylistCMD { get; set; }

        public ICommand OpenFileInExplorerCMD { get; set; }


        public LibraryViewModel()
        {
            LoadLibrary();
            LoadPlaylistModule();

            RefreshFirstDatas();

            ShowPlaylistList = false;
            ShowFilters = true;


            #region Load Library

            LoadLibraryCMD = new Command(new Action(() =>
            {
                IsPlaylistMode = false;
                RefreshFirstDatas();
                PlaylistName = "";
                ShowPlaylistList = false;
                ShowFilters = true;
            }));

            #endregion

            #region Load Playlist

            LoadPlaylistCMD = new CommandWithParameter(new Action<object>((o) =>
            {
                SelectedPlaylist = o as Playlist;
                if (SelectedPlaylist != null)
                {
                    SongsLIST = SelectedPlaylist.Songs;
                    PlaylistName = SelectedPlaylist.Name;
                }
                //else
                //{
                //    this.AddToPlaylistCMD.Execute("");
                //    ShowPlaylistList = false;
                //    IsAddPlaylistMode = false;
                //    if (SelectedPlaylist != null)
                //        MessageBox.Show("Song added to '" + SelectedPlaylist.Name + "'");
                //    PlaylistsLIST = Lib.Playlists;
                //}
            }));

            #endregion

            #region Load Genre

            LoadGenreCMD = new CommandWithParameter(new Action<object>((o) =>
            {
                SelectedGenre = o as string;
                if (_selectedGenre != null)
                {

                    ArtistsLIST = Lib.GetArtistsByGenre(_selectedGenre);

                    AlbumsLIST = Lib.GetAlbumsByGenre(_selectedGenre);

                    #region Load GenreSongs

                    IEnumerable<Song> selectedSongs = from son in Lib.Songs
                                                      where son.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                      select son;

                    List<Song> newListSon = new List<Song>();

                    if (selectedSongs.Any())
                    {
                        // MessageBox.Show("il y a des artists");

                        foreach (Song son in selectedSongs)
                        {
                            // MessageBox.Show("je parcours mes artiste du genre : " + art.Name);
                            newListSon.Add(son);
                        }
                    }

                    SongsLIST = newListSon;

                    #endregion

                    AllArtistsText = "All (" + ArtistsLIST.Count + ")";
                    AllAlbumsText = "All (" + AlbumsLIST.Count + ")";
                }
            }));

            #endregion

            #region Load Artist

            LoadArtistCMD = new CommandWithParameter(new Action<object>((o) =>
            {
                SelectedArtist = o as Artist;
                if (_selectedArtist != null)
                {
                    if (SelectedGenre != null)
                    {
                        IEnumerable<Album> linqAlbums = from alb in Lib.GetAlbumsByGenre(SelectedGenre)
                                                        where alb.Artist.Name.ToUpper() == SelectedArtist.Name.ToUpper()
                                                        select alb;
                        List<Album> newAlbumList = new List<Album>();
                        if (linqAlbums.Any())
                            foreach (Album alb in linqAlbums)
                                newAlbumList.Add(alb);
                        AlbumsLIST = newAlbumList;
                    }
                    else
                    {
                        AlbumsLIST = _selectedArtist.Albums;
                    }

                    #region Load ArtistSongs

                    IEnumerable<Song> selectedSongs = null;

                    if (SelectedGenre != null)
                    {
                        selectedSongs = from son in Lib.Songs
                                        where (son.Artist.Name.ToUpper() == _selectedArtist.Name.ToUpper()) &&
                                              (son.Genre.ToUpper() == SelectedGenre.ToUpper())
                                        select son;
                    }
                    else
                    {
                        selectedSongs = from son in Lib.Songs
                                        where son.Artist.Name.ToUpper() == _selectedArtist.Name.ToUpper()
                                        select son;
                    }

                    List<Song> newListSon = new List<Song>();

                    if (selectedSongs.Any())
                    {
                        // MessageBox.Show("il y a des artists");

                        foreach (Song son in selectedSongs)
                        {
                            // MessageBox.Show("je parcours mes artiste du genre : " + art.Name);
                            newListSon.Add(son);
                        }
                    }

                    SongsLIST = newListSon;

                    #endregion

                    AllAlbumsText = "All (" + AlbumsLIST.Count + ")";
                }
            }));

            #endregion

            #region Load Album

            LoadAlbumCMD = new CommandWithParameter(new Action<object>((o) =>
            {
                SelectedAlbum = o as Album;
                if (_selectedAlbum != null)
                {
                    List<Song> newList = new List<Song>();

                    if (_selectedGenre != null)
                    {
                        IEnumerable<Song> linqSongs = from sg in _selectedAlbum.Songs
                                                      where sg.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                      select sg;
                        if (linqSongs.Any())
                            foreach (Song sg in linqSongs)
                                newList.Add(sg);
                    }
                    else
                    {
                        SongsLIST = _selectedAlbum.Songs;
                    }
                }
            }));

            #endregion


            #region Show Playlists List

            ShowPlaylistsListCMD = new Command(new Action(() =>
            {
                IsPlaylistMode = true;
                SongsLIST = null;
                PlaylistName = "Select your playlist";
                ShowPlaylistList = true;
                ShowFilters = false;
                GenresLIST = null;
                ArtistsLIST = null;
                AlbumsLIST = null;
                //AllGenresText = "Ouech";
                //AllArtistsText = "TonTon";
                //AllAlbumsText = " ! ! !";
            }));

            #endregion


            #region Play Song Item

            PlaySongItemCMD = new Command(new Action(() =>
            {
                if (SelectedSong != null)
                {
                    //MessageBox.Show("YO MON GARS CA PETE !!");
                    CurrentList curList = CurrentList.getInstance();
                    curList.ResetList();
                    curList.addElement(Path.GetFullPath(SelectedSong.Path));
                    curList.DropEvent(this, null);
                }
            }));

            #endregion

            #region Play Video Item

            PlayVideoItemCMD = new Command(new Action(() =>
            {
                if (SelectedVideo != null)
                {
                    CurrentList curList = CurrentList.getInstance();
                    curList.ResetList();
                    curList.addElement(Path.GetFullPath(SelectedVideo.PathOfFile));
                    curList.DropEvent(this, null);
                }
            }));

            #endregion

            #region Play Picture Item

            PlayPictureItemCMD = new Command(new Action(() =>
            {
                if (SelectedPicture != null)
                {
                    CurrentList curList = CurrentList.getInstance();
                    curList.ResetList();
                    curList.addElement(Path.GetFullPath(SelectedPicture.PathOfFile));
                    curList.DropEvent(this, null);
                }
            }));

            #endregion


            #region Filters All

            AllGenresCMD = new Command(new Action(() =>
            {
                RefreshFirstDatas();
            }));

            AllArtistsCMD = new Command(new Action(() =>
            {
                this.LoadGenreCMD.Execute(null);
            }));

            AllAlbumsCMD = new Command(new Action(() =>
            {
                this.LoadArtistCMD.Execute(null);
            }));
            #endregion


            #region Import Directory

            ImportDirectoryCMD = new Command(new Action(() =>
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != null && dialog.SelectedPath != "")
                {
                    MessageBox.Show(dialog.SelectedPath);
                    Lib.ImportDir(dialog.SelectedPath);
                    RefreshFirstDatas();
                }
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
                        RefreshFirstDatas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            #endregion

            #region Delete File

            DeleteFileCMD = new Command(new Action(() =>
            {
                if (SelectedSong != null)
                {
                    MessageBoxResult yo = MessageBox.Show("This will be deleted from you library\nDo you want to delete the file from your computer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (yo == MessageBoxResult.Yes)
                    {
                        File.Delete(Path.GetFullPath(SelectedSong.Path));
                    }
                    if (IsPlaylistMode == false)
                    {
                        Lib.Songs.Remove(SelectedSong);
                        RefreshFirstDatas();
                    }
                    else
                    {
                        SelectedPlaylist.Songs.Remove(SelectedSong);
                        SongsLIST = null;
                        SongsLIST = SelectedPlaylist.Songs;
                    }
                }
            }));

            #endregion

            #region Add to current List
            AddToCurrentListCMD = new Command(new Action(() =>
            {
                CurrentList.getInstance().addElement(Path.GetFullPath(SelectedSong.Path));
            }));
            #endregion

            #region Open Explorer
            OpenFileInExplorerCMD = new Command(new Action(() =>
            {
                Process.Start(Path.GetDirectoryName(SelectedSong.Path));
            }));
            #endregion

            
            #region Add to Playlist
            AddToPlaylistCMD = new CommandWithParameter(new Action<object>((o) =>
                {
                    if (o != null)
                        SelectedPlaylist = o as Playlist;
                    if (SelectedSong != null && SelectedPlaylist != null)
                    {
                        SelectedPlaylist.AddSong(SelectedSong);
                        MessageBox.Show(SelectedSong.Title + " added to '" + SelectedPlaylist.Name + "'");
                    }
                    else
                        MessageBox.Show("SelectedSong : " + SelectedSong + "\nSelectedPlaylist" + SelectedPlaylist);
                }));
            #endregion


            #region Add Playlist

            AddPlaylistCMD = new Command(new Action(() => 
                {
                    //MessageBox.Show("Type the name of your new Playlist :");
                    string playlistName = Interaction.InputBox("Type the name of your new Playlist :", "New Playlist", "My playlist");
                    if (playlistName != "" && playlistName != null)
                    {
                        if (Lib.AddPlaylist(playlistName, "") == false)
                        {
                            MessageBox.Show("This playlist name already exists");
                        }
                        PlaylistsLIST = null;
                        PlaylistsLIST = Lib.Playlists;
                        PlaylistsMenuItemList = null;
                        PlaylistsMenuItemList = Lib.Playlists;
                    }
                }));
            #endregion

            #region Delete Playlist
            DeletePlaylistCMD = new Command(new Action(() => 
                {
                    if (SelectedPlaylist != null)
                    {
                        File.Delete(Tools.DefaultPathFolderPlaylist + SelectedPlaylist.Name + ".xml");
                        SelectedPlaylist.IsDeleted = true;
                        Lib.Playlists.Remove(SelectedPlaylist);
                        PlaylistsLIST = null;
                        PlaylistsLIST = Lib.Playlists;
                        PlaylistsMenuItemList = null;
                        PlaylistsMenuItemList = Lib.Playlists;
                    }
                }));
            #endregion

            #region Rename Playlist
            RenamePlaylistCMD = new Command(new Action(() =>
                {
                    string playlistName = Interaction.InputBox("Type the new name of your Playlist :", "New Name", SelectedPlaylist.Name);
                    if (playlistName != "" && playlistName != null)
                    {
                        File.Delete(Tools.DefaultPathFolderPlaylist + SelectedPlaylist.Name + ".xml");
                        SelectedPlaylist.Name = playlistName;
                        PlaylistsLIST = null;
                        PlaylistsLIST = Lib.Playlists;
                        PlaylistsMenuItemList = null;
                        PlaylistsMenuItemList = Lib.Playlists;
                    }
                }));
            #endregion

            #region Play Playlist

            PlayPlaylistCMD = new Command(new Action(() =>
                {
                    if (SelectedPlaylist != null)
                    {
                        if (SelectedPlaylist.Songs.Count > 0)
                        {
                            CurrentList curList = CurrentList.getInstance();
                            curList.ResetList();
                            foreach (Song sg in SelectedPlaylist.Songs)
                            {
                                curList.addElement(Path.GetFullPath(sg.Path));
                            }
                            curList.DropEvent(this, null);
                        }
                        else
                        {
                            MessageBox.Show("Your Playlist is empty !\nAdd some songs to play it");
                        }
                    }
                }));

            #endregion

        }


        private void LoadGenre(Object o)
        {
            //SelectedGenre = o as string;
            MessageBox.Show("Valeur du genre selectionné : " + _selectedGenre);
            if (_selectedGenre != null)
            {

                ArtistsLIST = Lib.GetArtistsByGenre(_selectedGenre);

                AlbumsLIST = Lib.GetAlbumsByGenre(_selectedGenre);

                #region Load GenreSongs

                IEnumerable<Song> selectedSongs = from son in Lib.Songs
                                                    where son.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                    select son;

                List<Song> newListSon = new List<Song>();

                if (selectedSongs.Any())
                {
                    // MessageBox.Show("il y a des artists");

                    foreach (Song son in selectedSongs)
                    {
                        // MessageBox.Show("je parcours mes artiste du genre : " + art.Name);
                        newListSon.Add(son);
                    }
                }

                SongsLIST = newListSon;

                #endregion

                AllArtistsText = "All (" + ArtistsLIST.Count + ")";
                AllAlbumsText = "All (" + AlbumsLIST.Count + ")";
            }
        }
        
        private void LoadPlaylistModule()
        {
            if (Lib != null)
            {
                Lib.OpenPlaylists();
                PlaylistsLIST = Lib.Playlists;
                PlaylistsMenuItemList = Lib.Playlists;
            }
        }

        private void LoadLibrary()
        {
            Lib = new Library(Path.GetFullPath(Tools.DefaultPathFileLibrary));

            try
            {
                if (Lib != null)
                {
                    string error = Lib.Init();
                    if (error != null)
                        MessageBox.Show("-- Paths not Found --\n" + error);
                }
            }
            catch
            {
                MessageBox.Show("Problem with library loading");
            }

        }   

        public void RefreshFirstDatas()
        {
            if (Lib != null)
            {
                SongsLIST = null;
                SongsLIST = Lib.Songs;

                GenresLIST = null;
                GenresLIST = Lib.Genres;

                ArtistsLIST = null;
                ArtistsLIST = Lib.Artists;

                AlbumsLIST = null;
                AlbumsLIST = Lib.Albums;

                AllGenresText = "All (" + Lib.Genres.Count + ")";
                AllArtistsText = "All (" + Lib.Artists.Count + ")";
                AllAlbumsText = "All (" + Lib.Albums.Count + ")";

                VideosList = null;
                VideosList = Lib.Videos;

                PicturesList = null;
                PicturesList = Lib.Pictures;
            }
        }

    }

}
