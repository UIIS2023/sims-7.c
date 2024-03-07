using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.WPF.Views.GuideWindows
{
    /// <summary>
    /// Interaction logic for TodaysToursOverviewWindow.xaml
    /// </summary>
    public partial class TodaysToursOverviewWindow : Window
    {
        public ObservableCollection<TourEvent> TodaysEvents { get; set; }
        private TourEventController _tourEventController;

        public TourEvent SelectedTourEvent { get; set; }
        public TodaysToursOverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourEventController = new TourEventController();

            TodaysEvents = new ObservableCollection<TourEvent>(_tourEventController.GetTourEventsForNow());
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourEvent != null && SelectedTourEvent.Status == TourEventStatus.NotStarted && SelectedTourEvent.Tour.TourPoints.Count()>=2)
            {
                TourPointsWindow tourPointsWindow = new TourPointsWindow(SelectedTourEvent);
                tourPointsWindow.Show();
                SelectedTourEvent.Status = TourEventStatus.Started;
                _tourEventController.Update(SelectedTourEvent);
            }
        }
    }
}
