using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace SlideBarMVVM
{
    public enum PlayerState
    {
        Play = 1,
        Pause = 2,
        Stop = 4
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

        public ICommand OpenDialogCommand { get; set; }
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

        private String  _repeatButtonText;
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

        public ICommand PlayRequest { get; set; }
        public ICommand StopRequest { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand MediaFailedCommand { get; set; }
        public ICommand MediaEndedCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PrevCommand { get; set; }

        private void Init() 
        {
            if (CurrentList.getSize() > 0)
            {
                this.CurrentSourceMedia = new Uri(CurrentList.getCurrentElement());
                this.PlayState = PlayerState.Play;
            }
        }

        private void Test() 
        {
            CurrentList.addElement(@"C:\Users\S@suke\Google Drive\KramAyrtoogle\dotNet\BDD\Video\4 Univers ou multivers.mp4");
            CurrentList.addElement(@"C:\Users\S@suke\Google Drive\KramAyrtoogle\dotNet\BDD\Music\01 Normal.mp3");

        }


        public MediaPlayerViewModel()
        {


            MessageBox.Show("View Model construction");

            this._isOpened = false;
            this.PlayPauseButtonText = "Play";
            this.PlayState = PlayerState.Stop;

            #region FileDialogCommand
            this.OpenDialogCommand = new Command(new Action(() =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                // Video files (*.avi, *.mp4, *.wmv)|*.avi; *.mp4; *.wmv
                ofd.Filter = "All files (*.*)|*.*";
                try
                {
                    if (ofd.ShowDialog() == true)
                    {
                        this.CurrentSourceMedia = new Uri(ofd.FileName);
                        this.PlayState = PlayerState.Stop;
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
                if (this.PlayState == PlayerState.Pause || this.PlayState == PlayerState.Stop)
                    this.PlayState = PlayerState.Play;
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
                this._isOpened = true;
                this.PlayPauseButtonText = "Pause";
            }));
            #endregion

            #region MediaFailedCommand
            this.MediaFailedCommand = new Command(new Action(() =>
            {
                this._isOpened = false;
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                MessageBox.Show("Error: Can't load file: Unknwon format");
            }));
            #endregion

            #region StopRequestCommand
            this.StopRequest = new Command(new Action(() =>
            {
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
            }));
            #endregion

            this._repeatState = RepeatState.NoRepeat;
            this.RepeatButtonText = "No repeat";

            #region RepeatCommand
            this.RepeatCommand = new Command(new Action(() =>
            {
                if (this._repeatState == RepeatState.NoRepeat) 
                {
                    this._repeatState = RepeatState.Repeat;
                    this.RepeatButtonText = "Repeat";
                }
                else if (this._repeatState == RepeatState.Repeat)
                {
                    this._repeatState = RepeatState.RepeatAll;
                    this.RepeatButtonText = "Repeat All";
                }
                else 
                {
                    this._repeatState = RepeatState.NoRepeat;
                    this.RepeatButtonText = "No Repeat";
                }
            }));
            #endregion

            #region MediaEndedCommand
            this.MediaEndedCommand = new Command(new Action(() =>
            {
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                if (this._repeatState == RepeatState.NoRepeat && CurrentList.HasNextElement())
                {
                    this.CurrentSourceMedia = new Uri(CurrentList.moveToNextElement());
                    this.PlayState = PlayerState.Play;
                    this.PlayPauseButtonText = "Pause";
                }
                else if (this._repeatState == RepeatState.Repeat)
                {
                    this.PlayState = PlayerState.Play;
                    this.PlayPauseButtonText = "Pause";
                }
                else if (this._repeatState == RepeatState.RepeatAll)
                {
                    if (CurrentList.HasNextElement())
                        this.CurrentSourceMedia = new Uri(CurrentList.moveToNextElement());
                    else
                    {
                        CurrentList.ResetIdx();
                        this.CurrentSourceMedia = new Uri(CurrentList.getCurrentElement());
                    }
                    this.PlayState = PlayerState.Play;
                    this.PlayPauseButtonText = "Pause";
                }
                else
                    CurrentList.ResetIdx();
            }));
            #endregion

            #region NextCommand
            this.NextCommand = new Command(new Action(() =>
            {
                MessageBox.Show("Next");
            }));
            #endregion

            #region PrevCommand
            this.PrevCommand = new Command(new Action(() =>
            {
                MessageBox.Show("Previous");
            }));
            #endregion

            this.Test();
            this.Init();
        }
    }
}
