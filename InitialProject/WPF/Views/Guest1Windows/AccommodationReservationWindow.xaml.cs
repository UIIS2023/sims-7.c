using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AccommodationReservationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationWindow : Window
    {
        //private readonly AccommodationReservationRepository accommodationReservationRepository;
        private readonly AccommodationReservationController _accommodationReservationController;
        public Accommodation accommodation;
        public int us;
        public User guest;
        public GuestReview guestReview = new GuestReview { Id = -1 };
        public AccommodationOwnerReview accommodationReview = new AccommodationOwnerReview { Id = -1 };

        bool isAvailable = true;

        public AccommodationReservationWindow(Accommodation a, User user)
        {
            InitializeComponent();
            accommodation = a;
            guest = user;
            _accommodationReservationController = new AccommodationReservationController();
            //accommodationReservationRepository = new AccommodationReservationRepository();
            //accommodationReservationRepository = AccommodationReservationRepository.GetInstance();
        }

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

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            int numberOfDaysForReservation = 0;
            try
            {
                numberOfDaysForReservation = Convert.ToInt32(NumberOfDaysTextBox.Text);
            }
            catch { }
            int numberOfGuests = 0;
            try
            {
                numberOfGuests = Convert.ToInt32(NumberOfGuestsTextBox.Text);
            }
            catch { }

            if (StartDateDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Niste uneli pocetni datum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EndDateDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Niste uneli krajnji datum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (end < start)
            {
                MessageBox.Show("Krajnji datum ne moze biti pre pocetnog datuma!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(NumberOfDaysTextBox.Text) || numberOfDaysForReservation == 0)
            {
                MessageBox.Show("Niste uneli broj dana za rezervaciju!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(NumberOfGuestsTextBox.Text) || numberOfGuests == 0)
            {
                MessageBox.Show("Niste uneli broj gostiju koji dolaze!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (numberOfDaysForReservation < accommodation.MinDaysForReservation)
            {
                MessageBox.Show("Uneli ste manje dana nego što je moguće za ovaj smeštaj!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (numberOfGuests > accommodation.MaxGuests)
            {
                MessageBox.Show("Uneli ste više gostiju nego što je moguće za ovaj smeštaj!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (end < start.AddDays(numberOfDaysForReservation))
            {
                MessageBox.Show("Broj dana prevazilazi opseg datuma!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int id = 0;
                bool isCancelled = false;
                AccommodationReservation accommodationReservation = new AccommodationReservation(id, accommodation, guest, start, start.AddDays(numberOfDaysForReservation), guestReview, accommodationReview, isCancelled);
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

        /*
        public static List<AccommodationReservation> ReadCSV(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split('|');
                int id = Convert.ToInt32(line[0]);
                Accommodation accommodation = new Accommodation() { Id = Convert.ToInt32(line[1]) };
                User guest = new User() { Id = Convert.ToInt32(line[2]) };
                DateTime start = DateTime.Parse(line[3]);
                DateTime end = DateTime.Parse(line[4]);
                GuestReview guestReview = new GuestReview() { Id = Convert.ToInt32(line[5]) };
                AccommodationOwnerReview accommodationReview = new AccommodationOwnerReview() { Id = Convert.ToInt32(line[6]) };
                bool isCancelled = Convert.ToBoolean(line[7]);
                AccommodationReservation reservation = new AccommodationReservation(id, accommodation, guest, start, end, guestReview, accommodationReview, isCancelled);
                reservations.Add(reservation);
            }
            reader.Close();
            return reservations;
        }

        List<AccommodationReservation> reservations = ReadCSV("../../../Resources/Data/accommodationReservations.csv");
        */

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
