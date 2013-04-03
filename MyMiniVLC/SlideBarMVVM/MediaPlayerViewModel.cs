using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using wmp2;
using System.IO;
using System.Timers;
using System.Reflection;
using System.Collections.ObjectModel;

namespace SlideBarMVVM
{
    public enum PlayerState
    {
        Play = 1,
        Pause = 2,
        Stop = 4,
        Restart = 8
    }

    public enum RepeatState
    {
        NoRepeat = 1,
        Repeat = 2,
        RepeatAll = 4
    }

    public class MediaPlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command OpenDialogCommand { get; set; }
        private Uri _currentSourceMedia;
        public Uri CurrentSourceMedia
        {
            get
            {
                return (this._currentSourceMedia);
            }
            set
            {
                if (this._currentSourceMedia != value)
                {
                    this._currentSourceMedia = value;
                    this._isOpened = false;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CurrentSourceMedia"));
                }
            }
        }

        private String _currentSourceName;
        public String CurrentSourceName
        {
            get
            {
                return (_currentSourceName);
            }
            set
            {
                if (this._currentSourceName != value)
                {
                    this._currentSourceName = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CurrentSourceName"));
                }
            }
        }

        private String _playButtonText;
        public String PlayPauseButtonText
        {
            get
            {
                return (this._playButtonText);
            }
            set
            {
                if (this._playButtonText != value)
                {
                    this._playButtonText = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PlayPauseButtonText"));
                }
            }
        }

        private String _playButtonImage;
        public String PlayPauseButtonImage
        {
            get
            {
                return (this._playButtonImage);
            }
            set
            {
                if (this._playButtonImage != value)
                {
                    this._playButtonImage = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PlayPauseButtonImage"));
                }
            }
        }

        private PlayerState _playState;
        public PlayerState PlayState
        {
            get
            {
                return (this._playState);
            }
            set
            {
                if (this._playState != value)
                {
                    this._playState = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PlayState"));
                }
            }
        }

        private Boolean _isOpened;

        private String _repeatButtonText;
        public String RepeatButtonText
        {
            get
            {
                return (this._repeatButtonText);
            }
            set
            {
                if (this._repeatButtonText != value)
                {
                    this._repeatButtonText = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("RepeatButtonText"));
                }
            }
        }
        private String _repeatButtonImage;
        public String RepeatButtonImage
        {
            get
            {
                return (this._repeatButtonImage);
            }
            set
            {
                if (this._repeatButtonImage != value)
                {
                    this._repeatButtonImage = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("RepeatButtonImage"));
                }
            }
        }

        private String _shuffleButtonImage;
        public String ShuffleButtonImage
        {
            get
            {
                return (this._shuffleButtonImage);
            }
            set
            {
                if (this._shuffleButtonImage != value)
                {
                    this._shuffleButtonImage = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ShuffleButtonImage"));
                }
            }
        }

        private Boolean _mute;
        public Boolean Mute
        {
            get { return (_mute); }
            set
            {
                if (this._mute != value)
                {
                    this._mute = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Mute"));
                }
            }
        }
        private String _muteButtonImage;
        public String MuteButtonImage
        { 
            get 
            {
                return (this._muteButtonImage);
            }
            set 
            {
                if (this._muteButtonImage != value) 
                {
                    this._muteButtonImage = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MuteButtonImage"));
                }
            }
        }

        private Boolean _changedInPause;
        private Boolean _fullScreen;
        

        public Command PlayRequest { get; set; }
        public Command StopRequest { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand MediaFailedCommand { get; set; }
        public ICommand MediaEndedCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand ShuffleCommand { get; set; }
        public ICommand MuteCommand { get; set; }
        public ICommand FullScreenCommand { get; set; }
        public ICommand CloseWindow { get; set; }

        public ObservableCollection<CurrentListObject> Collect { get; set; }

        private void Init()
        {
            CurrentList curList = CurrentList.getInstance();

            if (Environment.GetCommandLineArgs().Count() > 1)
            {
                bool first = true;
                foreach (string s in Environment.GetCommandLineArgs())
                {
                    if (!first)
                        curList.addElement(s);
                    else
                        first = false;
                }
            }
            else 
            {
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\01 - Skrillex - First Of The Year (Equinox).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\02 - Skrillex - Ruffneck (Flex).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\02 - Skrillex - Ruffneck (Flex).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\02 - Skrillex - Ruffneck (Flex).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\02 - Skrillex - Ruffneck (Flex).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\02 - Skrillex - Ruffneck (Flex).mp3");
                curList.addElement(@"G:\Musique\Skrillex\Albums and EPs\2011 - More Monsters And Sprites [EP]\01 - Skrillex - First Of The Year (Equinox).mp3");
            }
            if (curList.getSize() > 0)
               this.PlayRequest.Execute(this);
        }

        public MediaPlayerViewModel()
        {
            this.PlayPauseButtonImage = "/Assets/play.png";
            CurrentList.getInstance().ModifiedEvent += new EventHandler(modifiedEvent);

            this.Collect = new ObservableCollection<CurrentListObject>();

            this._isOpened = false;
            this.PlayPauseButtonText = "Play";
            this.PlayState = PlayerState.Stop;
            this._changedInPause = false;

            this.CloseWindow = new Command(new Action(() =>
                {
                    Application.Current.MainWindow.Close();
                }));
            
            #region FileDialogCommand
            this.OpenDialogCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                OpenFileDialog ofd = new OpenFileDialog();
                // Video files (*.avi, *.mp4, *.wmv)|*.avi; *.mp4; *.wmv
                ofd.Multiselect = true;
                ofd.Filter = "All files (*.*)|*.*";
                try
                {
                    if (ofd.ShowDialog() == true)
                    {
                        curList.ResetList();
                        foreach (string name in ofd.FileNames)
                            curList.addElement(name);
                        this.PlayState = PlayerState.Stop;
                        this.CurrentSourceMedia = new Uri(curList.getCurrentElement());
                        this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                        this.PlayState = PlayerState.Play;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));
            #endregion

            #region PlayRequestCommand
            this.PlayRequest = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (curList.getSize() <= 0)
                    return;
                else if (this.PlayState == PlayerState.Pause)
                    this.PlayState = PlayerState.Play;
                else if (this.PlayState == PlayerState.Stop)
                {
                    this.CurrentSourceMedia = new Uri(curList.getCurrentElement());
                    this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                    this.PlayState = PlayerState.Play;
                }
                else
                    this.PlayState = PlayerState.Pause;
                if (this._isOpened)
                {
                    if (this.PlayState == PlayerState.Play)
                    {
                        this.PlayPauseButtonText = "Pause";
                        this.PlayPauseButtonImage = "/Assets/pause.png";
                    }
                    else
                    {
                        this.PlayPauseButtonText = "Play";
                        this.PlayPauseButtonImage = "/Assets/play.png";
                    }
                }
            }));
            #endregion

            #region MediaOpenedCommand
            this.MediaOpenedCommand = new Command(new Action(() =>
            {
                this._isOpened = true;
                CurrentList.getInstance().setIsPlaying(true, CurrentList.getInstance().getCurrentElementIdx());
                if (!this._changedInPause)
                {
                    this.PlayPauseButtonText = "Pause";
                    this.PlayPauseButtonImage = "/Assets/pause.png";
                }
                this.StopRequest.CanExec = true;
                this._changedInPause = false;
            }));
            #endregion

            #region MediaFailedCommand
            this.MediaFailedCommand = new Command(new Action(() =>
            {
                CurrentList.getInstance().setIsPlaying(false, CurrentList.getInstance().getCurrentElementIdx());
                this._isOpened = false;
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                this.StopRequest.CanExec = false;
            }));
            #endregion

            #region StopRequestCommand
            this.StopRequest = new Command(new Action(() =>
            {
                CurrentList.getInstance().setIsPlaying(false, CurrentList.getInstance().getCurrentElementIdx());
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                this.PlayPauseButtonImage = "/Assets/play.png";
                this.StopRequest.CanExec = false;
            }), false);
            #endregion

            this.RepeatButtonText = "No repeat";
            this.RepeatButtonImage = "/Assets/repeat.png";

            #region RepeatCommand
            this.RepeatCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (curList.Repeat == RepeatState.NoRepeat)
                {
                    curList.Repeat = RepeatState.Repeat;
                    this.RepeatButtonText = "Repeat";
                    this.RepeatButtonImage = "/Assets/repeatone.png";
                }
                else if (curList.Repeat == RepeatState.Repeat)
                {
                    curList.Repeat = RepeatState.RepeatAll;
                    this.RepeatButtonText = "Repeat All";
                    this.RepeatButtonImage = "/Assets/repeatall.png";
                }
                else
                {
                    curList.Repeat = RepeatState.NoRepeat;
                    this.RepeatButtonText = "No Repeat";
                    this.RepeatButtonImage = "/Assets/repeat.png";
                }
            }));
            #endregion

            #region MediaEndedCommand
            this.MediaEndedCommand = new Command(new Action(() =>
            {
                CurrentList.getInstance().setIsPlaying(false, CurrentList.getInstance().getCurrentElementIdx());
                CurrentList curList = CurrentList.getInstance();
                if (curList.Repeat == RepeatState.NoRepeat)
                {
                    this.PlayState = PlayerState.Stop;
                    this.PlayPauseButtonText = "Play";
                    this.PlayPauseButtonImage = "/Assets/play.png";
                    if (curList.HasNextElement())
                    {
                        this.CurrentSourceMedia = new Uri(curList.moveToNextElement());
                        this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                        this.PlayState = PlayerState.Play;
                        this.PlayPauseButtonText = "Pause";
                        this.PlayPauseButtonImage = "/Assets/pause.png";
                    }
                    else
                        curList.ResetIdx();
                }
                else if (curList.Repeat == RepeatState.RepeatAll && curList.getSize() >= 1)
                {
                    this.PlayState = PlayerState.Stop;
                    this.PlayPauseButtonText = "Play";
                    this.PlayPauseButtonImage = "/Assets/play.png";
                    if (curList.HasNextElement())
                    {
                        this.CurrentSourceMedia = new Uri(curList.moveToNextElement());
                        this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                    }
                    else
                    {
                        curList.ResetIdx();
                        this.CurrentSourceMedia = new Uri(curList.getCurrentElement());
                        this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                    }
                    this.PlayState = PlayerState.Play;
                    this.PlayPauseButtonText = "Pause";
                    this.PlayPauseButtonImage = "/Assets/pause.png";
                }
                else
                    CurrentList.getInstance().setIsPlaying(true, CurrentList.getInstance().getCurrentElementIdx());
            }));
            #endregion

            this.ShuffleButtonImage = "/Assets/shuffleoff.png";
            #region ShuffleCommand
            this.ShuffleCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (this.ShuffleButtonImage == "/Assets/shuffleoff.png")
                {
                    if (curList.getSize() > 0)
                    {
                        curList.Random();
                        curList.Shuffle = true;
                        this.ShuffleButtonImage = "/Assets/shuffleon.png";
                    }
                }
                else
                {
                    curList.ResetRandom();
                    curList.Shuffle = false;
                    this.ShuffleButtonImage = "/Assets/shuffleoff.png";
                }
            }));
            #endregion

            this.Mute = false;
            this.MuteButtonImage = "/Assets/NoMute.png";
            #region MuteCommand
            this.MuteCommand = new Command(new Action(() =>
            {
                if (this.Mute)
                {
                    this.Mute = false;
                    this.MuteButtonImage = "/Assets/NoMute.png";
                }
                else
                {
                    this.Mute = true;
                    this.MuteButtonImage = "/Assets/Mute.png";
                }                  
            }));
            #endregion

            this._fullScreen = false;
            #region FullScreenCommand
            this.FullScreenCommand = new Command(new Action(() => 
            {
                Window w = App.Current.MainWindow;
                if (!_fullScreen)
                {
                    w.WindowStyle = WindowStyle.None;
                    w.WindowState = WindowState.Maximized;
                    _fullScreen = true;
                }
                else
                {
                    w.WindowStyle = WindowStyle.SingleBorderWindow;
                    w.WindowState = WindowState.Normal;
                    _fullScreen = false;
                }
            }));
            #endregion
            
            CurrentList.getInstance().DropEvent += new EventHandler(dropEvent);
            CurrentList.getInstance().ChangedEvent += new EventHandler(changedEvent);
            this.Init();

        }

        void dropEvent(object sender, EventArgs e)
        {
            this.StopRequest.Execute(this);
            this.PlayRequest.Execute(this);
        }

        void changedEvent(object sender, EventArgs e)
        {
            PlayerState tmp = this.PlayState;

            this.StopRequest.Execute(this);
            if (tmp != PlayerState.Stop)
            {
                if (tmp == PlayerState.Pause)
                    this._changedInPause = true;
                this.PlayRequest.Execute(this);
                if (tmp == PlayerState.Pause)
                    this.PlayRequest.Execute(this);
            }
        }

        void modifiedEvent(object sender, EventArgs e) 
        {
            this.Collect.Clear();
            foreach (CurrentListObject s in CurrentList.getInstance().getAllElement())
                this.Collect.Add(s);
        }

    }
}
