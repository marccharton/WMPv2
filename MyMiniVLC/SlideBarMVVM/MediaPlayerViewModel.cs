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
        private Boolean _isRepeat;
        public Boolean  IsRepeat
        {
            get
            {
                return (this._isRepeat);
            }
            set
            {
                if (this._isRepeat != value)
                {
                    this._isRepeat = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsRepeat"));
                }
            }
        }

        public ICommand PlayRequest { get; set; }
        public ICommand StopRequest { get; set; }
        public ICommand MediaOpenedCommand { get; set; }
        public ICommand MediaFailedCommand { get; set; }
        public ICommand MediaEndedCommand { get; set; }

        public MediaPlayerViewModel()
        {
            this._isOpened = false;
            this.PlayPauseButtonText = "Play";
            this.PlayState = PlayerState.Stop;

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

            this.MediaOpenedCommand = new Command(new Action(() =>
            {
                this._isOpened = true;
                this.PlayPauseButtonText = "Pause";
            }));

            this.MediaFailedCommand = new Command(new Action(() =>
            {
                this._isOpened = false;
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                MessageBox.Show("Error: Can't load file: Unknwon format");
            }));

            this.StopRequest = new Command(new Action(() =>
            {
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
            }));

            this.IsRepeat = false;
            this.MediaEndedCommand = new Command(new Action(() =>
            {
                this.PlayState = PlayerState.Stop;
                this.PlayPauseButtonText = "Play";
                if (this.IsRepeat) 
                {
                    this.PlayState = PlayerState.Play;
                    this.PlayPauseButtonText = "Pause";                
                }
            }));
        }
    }
}
