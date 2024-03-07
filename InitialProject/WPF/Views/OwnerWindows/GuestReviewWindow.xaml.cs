using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace InitialProject.WPF.Views.OwnerWindows
{
    /// <summary>
    /// Interaction logic for GuestReviewWindow.xaml
    /// </summary>
    public partial class GuestReviewWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<int> Grades { get; set; }
        public GuestReviewController _guestReviewController;
        public AccommodationReservationController _accommodationReservationController;

        #region NotifyProperties
        private string _guest;
        public string Guest
        {
            get => _guest;
            set
            {
                if (value != _guest)
                {
                    _guest = value;
                    OnPropertyChanged("Guest");
                }
            }
        }

        private string _accommodation;
        public string Accommodation
        {
            get => _accommodation;
            set
            {
                if (value != _accommodation)
                {
                    _accommodation = value;
                    OnPropertyChanged("Accommodation");
                }
            }
        }
        private int _selectedCleanliness;
        public int SelectedCleanliness
        {
            get => _selectedCleanliness;
            set
            {
                if (value != _selectedCleanliness)
                {
                    _selectedCleanliness = value;
                    OnPropertyChanged("SelectedCleanliness");
                }
            }
        }

        private int _selectedBehaviour;
        public int SelectedBehaviour
        {
            get => _selectedBehaviour;
            set
            {
                if (value != _selectedBehaviour)
                {
                    _selectedBehaviour = value;
                    OnPropertyChanged("SelectedBehaviour");
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
        public AccommodationReservation reservation { get; set; }

        public RelayCommand FinishCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public User guest { get; set; }

        public GuestReviewWindow(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            this.DataContext = this;

            _guestReviewController = new GuestReviewController();
            _accommodationReservationController = new AccommodationReservationController();
            reservation = accommodationReservation;
            guest = reservation.Guest;
            Grades = new ObservableCollection<int>();
            Grades.Add(1);
            Grades.Add(2);
            Grades.Add(3);
            Grades.Add(4);
            Grades.Add(5);

            Guest = reservation.Guest.FirstName + " " + reservation.Guest.LastName;

            FinishCommand = new RelayCommand(AddReviewButton_Click, CanAccept);

            CancelCommand = new RelayCommand(CancelButton_Click);
       
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool CanAccept(object param)
        {
            return true;
        }
        private void AddReviewButton_Click(object sender)
        {
            GuestReview guestReview = new GuestReview() { Reservation = reservation, Guest = guest, Cleanliness = SelectedCleanliness, Behaviour = SelectedBehaviour, Comment = Comment };
            _guestReviewController.Save(guestReview);
            reservation.GuestReview = guestReview;
            _accommodationReservationController.Update(reservation);

             Close();
        }

        private void CancelButton_Click(object sender)
        {
            Close();
        }
    }
}
