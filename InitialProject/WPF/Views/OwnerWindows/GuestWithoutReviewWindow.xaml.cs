using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for GuestWithoutReviewWindow.xaml
    /// </summary>
    public partial class GuestWithoutReviewWindow : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public AccommodationReservationController _accommodationReservationController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        public RelayCommand GuestReviewCommand { get; set; }
     
        public GuestWithoutReviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationReservationController = new AccommodationReservationController();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservationsWithoutGuestReview());


            GuestReviewCommand = new RelayCommand(goToGuestReview_Click);
         
            
        }

        public void Refresh()
        {
            AccommodationReservations.Clear();
            foreach (AccommodationReservation reservation in _accommodationReservationController.GetAllReservationsWithoutGuestReview())
            {
                AccommodationReservations.Add(reservation);
            }
        }

        private void goToGuestReview_Click(object sender)
        {
            GuestReviewWindow GuestReview = new GuestReviewWindow(SelectedAccommodationReservation);
            GuestReview.ShowDialog();

            Refresh();


        }
    }
}
