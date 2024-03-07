using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using InitialProject.WPF.Views.Guest1Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.Views.Guest1Windows
{
    /// <summary>
    /// Interaction logic for WhereverWheneverWindow.xaml
    /// </summary>
    public partial class WhereverWheneverWindow : Window
    {
        private readonly AccommodationController _accommodationController;
        private readonly LocationController _locationController;
        private readonly AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public User guest { get; set; }

        public GuestReview guestReview = new GuestReview { Id = -1 };
        public AccommodationOwnerReview accommodationReview = new AccommodationOwnerReview { Id = -1 };

        DateTime start;
        private void StartDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            start = (DateTime)e.AddedItems[0];
        }

        DateTime end;
        private void EndDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            end = (DateTime)e.AddedItems[0];
        }

        public WhereverWheneverWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            guest = user;

            _accommodationController = new AccommodationController();
            _locationController = new LocationController();
            _accommodationReservationController = new AccommodationReservationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());

            for (int i = 0; i < Accommodations.Count; ++i)
            {
                Accommodations[i].Location = _locationController.GetAll().Find(a => a.Id == Accommodations[i].Location.Id);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfGuests = 0;
            int numberOfDaysForReservation = 0;

            try
            {
                numberOfGuests = Convert.ToInt32(NumberOfGuestsTextBox.Text);
            }
            catch { }

            try
            {
                numberOfDaysForReservation = Convert.ToInt32(NumerOfDaysForReservationTextBox.Text);
            }
            catch { }

            List<Accommodation> accommodations = _accommodationController.GetAll();

            for (int i = 0; i < Accommodations.Count; ++i)
            {
                Accommodations[i].Location = _locationController.GetAll().Find(a => a.Id == Accommodations[i].Location.Id);

            }

            List<Accommodation> filteredAccommodations = new List<Accommodation>();
            filteredAccommodations = GetFiltratedAccommodations(accommodations, numberOfGuests, numberOfDaysForReservation);
            AccommodationsDataGrid.ItemsSource = filteredAccommodations;
        }

        public List<Accommodation> GetFiltratedAccommodations(List<Accommodation> accommodations, int numberOfGuests, int numberOfDaysForReservation)
        {
            accommodations = accommodations.Where(a => a.MaxGuests >= numberOfGuests && (a.MinDaysForReservation >= numberOfDaysForReservation)).ToList();

            return accommodations;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfDaysForReservation = 0;
            try
            {
                numberOfDaysForReservation = Convert.ToInt32(NumerOfDaysForReservationTextBox.Text);
            }
            catch { }
            int numberOfGuests = 0;
            try
            {
                numberOfGuests = Convert.ToInt32(NumberOfGuestsTextBox.Text);
            }
            catch { }

            if (end < start)
            {
                MessageBox.Show("Krajnji datum ne moze biti pre pocetnog datuma!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(NumerOfDaysForReservationTextBox.Text) || numberOfDaysForReservation == 0)
            {
                MessageBox.Show("Niste uneli broj dana za rezervaciju!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(NumberOfGuestsTextBox.Text) || numberOfGuests == 0)
            {
                MessageBox.Show("Niste uneli broj gostiju koji dolaze!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedAccommodation == null)
            {
                MessageBox.Show("Morate izabrati smeštaj!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int id = 0;
                bool isCancelled = false;
                AccommodationReservation accommodationReservation = new AccommodationReservation(id, SelectedAccommodation, guest, start, start.AddDays(numberOfDaysForReservation), guestReview, accommodationReview, isCancelled);
                if (_accommodationReservationController.AvailableAccommodation(accommodationReservation, numberOfDaysForReservation))
                {
                    _accommodationReservationController.Save(accommodationReservation);
                    MessageBox.Show("Uspešno ste rezervisali smeštaj!", "Rezervisano!", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    _accommodationReservationController.GetFirstAvailableDate(accommodationReservation);
                }
            }
        }








        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _test3;
        public string Test3
        {
            get
            {
                return _test3;
            }
            set
            {
                if (value != _test3)
                {
                    _test3 = value;
                    OnPropertyChanged("Test3");
                }
            }
        }
    }
}
