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


namespace SlideBarMVVM
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        Library Lib;
        
        public bool IsPlaylistMode = false;
        public bool IsAddPlaylistMode = false;
        
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

        public List<MenuItemCustom> _playlistsMenuItemList;
        public List<MenuItemCustom> PlaylistsMenuItemList
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
        


        public Command LoadGenreCMD { get; set; }
        public Command LoadArtistCMD { get; set; }
        public Command LoadAlbumCMD { get; set; }
        public Command LoadPlaylistCMD { get; set; }
        public Command LoadLibraryCMD { get; set; }

        public Command ImportDirectoryCMD { get; set; }
        public Command ImportFileCMD { get; set; }

        public Command PlaySongItemCMD { get; set; }
        public Command PlayVideoItemCMD { get; set; }
        public Command PlayPictureItemCMD { get; set; }
        
        public Command AllGenresCMD { get; set; }
        public Command AllArtistsCMD { get; set; }
        public Command AllAlbumsCMD { get; set; }
        
        
        public Command DeleteFileCMD { get; set; }
        public Command AddToCurrentListCMD { get; set; }
        

        public Command ShowPlaylistsListCMD { get; set; }
        public Command AddToPlaylistCMD { get; set; }
        public Command RunAddPlaylistModeCMD { get; set; }
        public Command AddPlaylistCMD { get; set; }

        public Command DeletePlaylistCMD { get; set; }
        public Command RenamePlaylistCMD { get; set; }
        public Command PlayPlaylistCMD { get; set; }
        
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

            LoadPlaylistCMD = new Command(new Action(() =>
            {
                if (IsAddPlaylistMode == false)
                {
                    if (SelectedPlaylist != null)
                    {
                        SongsLIST = SelectedPlaylist.Songs;
                        PlaylistName = SelectedPlaylist.Name;
                    }
                }
                else
                {
                    this.AddToPlaylistCMD.Execute(null);
                    ShowPlaylistList = false;
                    IsAddPlaylistMode = false;
                    if (SelectedPlaylist != null)
                        MessageBox.Show("Song added to '" + SelectedPlaylist.Name + "'");
                    PlaylistsLIST = Lib.Playlists;
                }
            }));

            #endregion

            #region Load Genre

            LoadGenreCMD = new Command(new Action(() =>
            {
                // MessageBox.Show("Valeur du genre selectionné : " + _selectedGenre);
                if (_selectedGenre != null)
                {
                    #region Load GenreArtists

                    IEnumerable<Artist> selectedArtists = from art in Lib.Artists
                                                          where art.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                          select art;

                    List<Artist> newList = new List<Artist>();

                    if (selectedArtists.Any())
                    {
                        // MessageBox.Show("il y a des artists");

                        foreach (Artist art in selectedArtists)
                        {
                            // MessageBox.Show("je parcours mes artiste du genre : " + art.Name);
                            newList.Add(art);
                        }
                    }

                    ArtistsLIST = newList;

                    #endregion

                    #region Load GenreAlbums

                    IEnumerable<Album> selectedAlbums = from alb in Lib.Albums
                                                        where alb.Genre.ToUpper() == _selectedGenre.ToUpper()
                                                        select alb;

                    List<Album> newListAlb = new List<Album>();

                    if (selectedArtists.Any())
                    {
                        // MessageBox.Show("il y a des artists");

                        foreach (Album alb in selectedAlbums)
                        {
                            // MessageBox.Show("je parcours mes artiste du genre : " + art.Name);
                            newListAlb.Add(alb);
                        }
                    }

                    AlbumsLIST = newListAlb;

                    #endregion

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

            LoadArtistCMD = new Command(new Action(() =>
            {
                //MessageBox.Show("Valeur de l'artist selectionné : " + _selectedArtist.Name);
                if (_selectedArtist != null)
                {
                    AlbumsLIST = _selectedArtist.Albums;
                    // SongsLIST = null;

                    #region Load ArtistSongs

                    IEnumerable<Song> selectedSongs = from son in Lib.Songs
                                                      where son.Artist.Name.ToUpper() == _selectedArtist.Name.ToUpper()
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

                    AllAlbumsText = "All (" + AlbumsLIST.Count + ")";
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

                        //MessageBox.Show("Lib.Artists.Count = " + Lib.Artists.Count.ToString());

                        string s = "";
                        foreach (Artist a in Lib.Artists)
                            s += a.Name + "\n";
                        //MessageBox.Show("Artists : \n" + s);
                        RefreshFirstDatas();
                        //MessageBox.Show("ArtistsLIST.Count = " + ArtistsLIST.Count.ToString());
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
                if (IsPlaylistMode == false && SelectedSong != null)
                {
                    MessageBoxResult yo = MessageBox.Show("This will be deleted from you library\nDo you want to delete the file from your computer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (yo == MessageBoxResult.Yes)
                    {
                        File.Delete(Path.GetFullPath(SelectedSong.Path));
                    }
                    Lib.Songs.Remove(SelectedSong);
                    RefreshFirstDatas();
                }
            }));

            #endregion

            #region Add to current List
            AddToCurrentListCMD = new Command(new Action(() =>
            {
                CurrentList.getInstance().addElement(Path.GetFullPath(SelectedSong.Path));
            }));
            #endregion

            
            #region Add to Playlist
            AddToPlaylistCMD = new Command(new Action(() =>
                {
                    if (SelectedSong != null && SelectedPlaylist != null)
                    {
                        SelectedPlaylist.AddSong(SelectedSong);
                    }
                    else
                    {
                        MessageBox.Show("SelectedSong : " + SelectedSong + "\nSelectedPlaylist" + SelectedPlaylist);
                    }
                }));
            #endregion

            #region Run Add Playlist Mode
            RunAddPlaylistModeCMD = new Command(new Action(() =>
                {
                    IsAddPlaylistMode = true;
                    ShowPlaylistList = true;
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
                    }
                }));
            #endregion

            #region Rename Playlist
            RenamePlaylistCMD = new Command(new Action(() =>
                {
                    string playlistName = Interaction.InputBox("Type the new name of your Playlist :", "New Name", "My playlist");
                    if (playlistName != "" && playlistName != null)
                    {
                        SelectedPlaylist.Name = playlistName;
                        PlaylistsLIST = null;
                        PlaylistsLIST = Lib.Playlists;
                    }
                }));
            #endregion

            #region Play Playlist

            PlayPlaylistCMD = new Command(new Action(() =>
                {
                    CurrentList curList = CurrentList.getInstance();
                    curList.ResetList();
                    foreach (Song sg in SelectedPlaylist.Songs)
                    {
                        curList.addElement(Path.GetFullPath(sg.Path));
                    }
                    curList.DropEvent(this, null);
                }));

            #endregion


        }

        private void LoadPlaylistModule()
        {
            if (Lib != null)
            {
                Lib.OpenPlaylists();
                PlaylistsLIST = Lib.Playlists;
            }
        }

        private void LoadLibrary()
        {
            //MessageBox.Show(Path.GetFullPath(Tools.DefaultPathFileLibrary));
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
