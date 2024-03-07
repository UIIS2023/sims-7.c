using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TourPointsWindow.xaml
    /// </summary>
    public partial class TourPointsWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        
        public TourPoint SelectedTourPoint { get; set; }

        public TourPointController _tourPointController { get; set; }
        public TourEventController _tourEventController { get; set; }

        public TourReservationController _tourReservationController { get; set; }   
        public TourEvent SelectedTourEvent { get; set; }
        public TourPointsWindow(TourEvent selectedTourEvent)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourPointController = new TourPointController();
            _tourEventController = new TourEventController();

            SelectedTourEvent = selectedTourEvent;

            _tourEventController.StartTourEvent(selectedTourEvent);

            TourPoints = new ObservableCollection<TourPoint>(SelectedTourEvent.Tour.TourPoints);
            SelectedTourPoint = TourPoints.ElementAt(0);

        }


        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private void RefreshWindow()
        {
            TourPoints.Clear();
            foreach(TourPoint point in SelectedTourEvent.Tour.TourPoints) { 
                TourPoints.Add(point);
            }
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourPoint == null)
            {
                return;
            }
            SelectedTourPoint.Active = true;
            _tourPointController.Update(SelectedTourPoint);

            if(TourPoints.ElementAt(TourPoints.Count - 1).Active)
            {
                SelectedTourEvent.Status = TourEventStatus.Finished;
                _tourEventController.Update(SelectedTourEvent);
                Close();
            }

            RefreshWindow();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTourEvent.Status = TourEventStatus.Finished;
            _tourEventController.Update(SelectedTourEvent);
            Close();
        }

        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourEvent == null || SelectedTourPoint == null)
            {
                return;
            }
            GuestsForTour guestsForTour = new GuestsForTour(SelectedTourEvent, SelectedTourPoint);
            guestsForTour.Show();

        }
    }
}
