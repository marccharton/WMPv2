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
        public SlideBarViewModel SbViewModel { get; set; }

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

        //private Tag _tag;
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
        private RepeatState _repeatState;

        public Command PlayRequest { get; set; }
        public Command StopRequest { get; set; }
        public Command NextCommand { get; set; }
        public Command PrevCommand { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand MediaFailedCommand { get; set; }
        public ICommand MediaEndedCommand { get; set; }
        public ICommand RepeatCommand { get; set; }

        //private Timer _timer;

        private void Init()
        {
            //if (CurrentList.getSize() > 0)
            //{
            //    this.CurrentSourceMedia = new Uri(CurrentList.getCurrentElement());
            //    this.PlayState = PlayerState.Play;
            //}
        }

        private void Test()
        {
            CurrentList tmp = CurrentList.getInstance();

            tmp.addElement(@"C:\Users\S@suke\Pictures\1184-shakaponk.bmp");
            tmp.addElement(@"C:\Users\S@suke\Desktop\3194648_Bangarang_feat__Sirah_Original_Mix.mp3");
            tmp.addElement(@"C:\Users\S@suke\Google Drive\KramAyrtoogle\dotNet\BDD\Music\01 Normal.mp3");
        }


        public MediaPlayerViewModel()
        {

            this._isOpened = false;
            this.PlayPauseButtonText = "Play";
            this.PlayState = PlayerState.Stop;
            //this._timer = new Timer();
            //this._timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            //this._timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;

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
                        //this._tag = new Tag(CurrentList.getCurrentElement());
                        //this._timer.Start();
                        this.PlayState = PlayerState.Play;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));
            //this.PropertyChanged(this, new PropertyChangedEventArgs("OpenDialogCommand"));
            #endregion

            #region PlayRequestCommand
            this.PlayRequest = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (this.PlayState == PlayerState.Pause)
                    this.PlayState = PlayerState.Play;
                else if (this.PlayState == PlayerState.Stop && curList.getSize() > 0)
                {
                    this.CurrentSourceMedia = new Uri(curList.getCurrentElement());
                    this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                    //this._tag = new Tag(CurrentList.getCurrentElement());
                    this.PlayState = PlayerState.Play;
                }
                else
                    this.PlayState = PlayerState.Pause;
                if (this._isOpened)
                {
                    if (this.PlayState == PlayerState.Play)
                        this.PlayPauseButtonText = "Pause";
                    else
                        this.PlayPauseButtonText = "Play";
                }
            }));
            #endregion

            #region MediaOpenedCommand
            this.MediaOpenedCommand = new Command(new Action(() =>
            {
               // MessageBox.Show("Opened");
                this._isOpened = true;
                this.PlayPauseButtonText = "Pause";
                this.StopRequest.CanExec = true;
                this.NextCommand.CanExec = true;
                this.PrevCommand.CanExec = true;
            }));
            #endregion

            #region MediaFailedCommand
            this.MediaFailedCommand = new Command(new Action(() =>
            {
                this._isOpened = false;
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                this.StopRequest.CanExec = false;
                this.NextCommand.CanExec = false;
                this.PrevCommand.CanExec = false;
                MessageBox.Show("Error: Can't load file: Unknwon format");
            }));
            #endregion

            #region StopRequestCommand
            this.StopRequest = new Command(new Action(() =>
            {
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                this.StopRequest.CanExec = false;
            }), false);
            #endregion

            this._repeatState = RepeatState.NoRepeat;
            this.RepeatButtonText = "No repeat";

            #region RepeatCommand
            this.RepeatCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (curList.Repeat == RepeatState.NoRepeat)
                {
                    curList.Repeat = RepeatState.Repeat;
                    this.RepeatButtonText = "Repeat";
                }
                else if (curList.Repeat == RepeatState.Repeat)
                {
                    curList.Repeat = RepeatState.RepeatAll;
                    this.RepeatButtonText = "Repeat All";
                }
                else
                {
                    curList.Repeat = RepeatState.NoRepeat;
                    this.RepeatButtonText = "No Repeat";
                }
            }));
            #endregion

            #region MediaEndedCommand
            this.MediaEndedCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                if (curList.Repeat == RepeatState.NoRepeat)
                {
                    this.PlayState = PlayerState.Stop;
                    this.PlayPauseButtonText = "Play";
                    if (curList.HasNextElement())
                    {
                        this.CurrentSourceMedia = new Uri(curList.moveToNextElement());
                        this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                        //this._tag = new Tag(CurrentList.getCurrentElement());
                        this.PlayState = PlayerState.Play;
                        this.PlayPauseButtonText = "Pause";
                    }
                    else
                        curList.ResetIdx();
                }
                else if (curList.Repeat == RepeatState.RepeatAll && curList.getSize() > 1)
                {
                    this.PlayState = PlayerState.Stop;
                    this.PlayPauseButtonText = "Play";
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
                }
                //else
                //    CurrentList.ResetIdx();
            }));
            #endregion

            #region NextCommand
            this.NextCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                PlayerState tmp = this.PlayState;

                if (tmp != PlayerState.Stop) 
                    this.PlayState = PlayerState.Stop;
                this.CurrentSourceMedia = new Uri(curList.moveToNextElement());
                this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                //this._tag = new Tag(CurrentList.getCurrentElement());
                if (tmp == PlayerState.Play)
                    this.PlayState = PlayerState.Play;
                else if (tmp == PlayerState.Pause)
                {
                    this.PlayState = PlayerState.Pause;
                    this.PlayPauseButtonText = "Play";
                    //MessageBox.Show("la");
                }
            }), false);
            #endregion

            #region PrevCommand
            this.PrevCommand = new Command(new Action(() =>
            {
                CurrentList curList = CurrentList.getInstance();
                PlayerState tmp = this.PlayState;

                if (tmp != PlayerState.Stop)
                    this.PlayState = PlayerState.Stop;
                this.CurrentSourceMedia = new Uri(curList.moveToPrevElement());
                this.CurrentSourceName = Path.GetFileName(curList.getCurrentElement());
                //this._tag = new Tag(CurrentList.getCurrentElement());
                if (tmp == PlayerState.Play)
                    this.PlayState = PlayerState.Play;
                else if (tmp == PlayerState.Pause)
                {
                    this.PlayState = PlayerState.Pause;
                    this.PlayPauseButtonText = "Play";
                }
            }), false);
            #endregion

            this.Test();
            this.Init();

          //  CurrentList.DropEvent += new EventHandler(_timer_Elapsed);
        }

        void _timer_Elapsed(object sender, EventArgs e)
        {
            MessageBox.Show("Event");
            //App.Current.Dispatcher.Invoke(new Action(() =>
            //{
            //    MessageBox.Show(this._tag.Album + ";" + this._tag.Title + ";" + this._tag.Composer);
            //}));
        }

    }
}
