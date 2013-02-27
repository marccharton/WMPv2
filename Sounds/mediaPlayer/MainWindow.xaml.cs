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

namespace mediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Media.SoundPlayer _s;
        
        public MainWindow()
        {
            InitializeComponent();
            _s = new System.Media.SoundPlayer();
            _s.SoundLocation = @"C:\Program Files (x86)\Total Video Converter\Converted\01 Chilly In F Minor.wav";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlaySound();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopSound();
        }

        public void PlaySound()
        {
            _s.Play();
        }

        public void StopSound()
        {
            _s.Stop();
        }
    }
}
