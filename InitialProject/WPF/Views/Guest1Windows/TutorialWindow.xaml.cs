using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        public TutorialWindow()
        {
            InitializeComponent();

            Tutorial.Source = new Uri("../../../Resources/Videos/tutorial.mp4", UriKind.RelativeOrAbsolute);

            Tutorial.LoadedBehavior = MediaState.Manual;
        }


        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Tutorial.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Tutorial.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Tutorial.Stop();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
