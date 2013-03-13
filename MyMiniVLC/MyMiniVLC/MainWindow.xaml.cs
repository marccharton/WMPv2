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
using Microsoft.Win32;

namespace MyMiniVLC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand fileDialogRoutedCommand = new RoutedCommand();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            sliderTime.IsEnabled = false;
        }


        public void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Video files (*.avi, *.mp4, *.wmv)|*.avi; *.mp4; *.wmv
            ofd.Filter = "All files (*.*)|*.*";
            try
            {
                if (ofd.ShowDialog() == true)
                {
                    MessageBox.Show(ofd.FileName);
                    videoPlayer.Source = new Uri(ofd.FileName);        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {

            videoPlayer.Pause();
            //MessageBox.Show(videoPlayer.NaturalDuration.ToString());
            //videoPlayer.Position = TimeSpan.FromSeconds(3600);
            //sliderTime.Maximum = videoPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
            //sliderTime.Value = videoPlayer.Position.TotalMilliseconds;
        }
        //http://msdn.microsoft.com/en-us/library/ms748248.aspx
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
       //     sliderTime.Maximum = videoPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
            try
            {
                videoPlayer.Play();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            sliderTime.IsEnabled = true;
            //if (videoPlayer.time
        }

        private void videoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ended");
            videoPlayer.Position = TimeSpan.FromMilliseconds(1);
            videoPlayer.Play();
        }

    }
}
