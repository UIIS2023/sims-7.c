using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using InitialProject.WPF.Views.Guest2Window;
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
    /// Interaction logic for AccommodationsOverview.xaml
    /// </summary>
    public partial class AccommodationsOverview : Window
    {
        private readonly AccommodationController _accommodationController;
        private readonly LocationController _locationController;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public User guest { get; set; }

        public int userId;

        public AccommodationsOverview(User user)
        {
            InitializeComponent();
            DataContext = this;

            //userId = id;
            guest = user;

            _accommodationController = new AccommodationController();
            //_accommodationController.Subscribe(this);
            _locationController = new LocationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());

            for (int i = 0; i < Accommodations.Count; ++i)
            {
                Accommodations[i].Location = _locationController.GetAll().Find(a => a.Id == Accommodations[i].Location.Id);

            }
        }

        private void UpdateAccommodationsList()
        {
            Accommodations.Clear();
            foreach (var accommodation in _accommodationController.GetAll())
            {
                //List<AccommodationRenovation> renovations =  _accommodationRenovationContreller.GetAll();
                //foreach(AccommodationRenovation renovation in renovations){
                //  if(renovation.Accommodation.id == accommodation.id){
                //      isAccommodationRecentlyRenovated(renovation);
                //  }
                Accommodations.Add(accommodation);
            }
        }

        public void Update()
        {
            UpdateAccommodationsList();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AccommodationSearchByNameTextBox.Text;
            string country = AccommodationSearchByCountryTextBox.Text;
            string city = AccommodationSearchByCityTextBox.Text;
            string type = AccommodationSearchByTypeTextBox.Text;

            int numberOfGuests = 0;
            int numberOfDaysForReservation = 0;

            try
            {
                numberOfGuests = Convert.ToInt32(AccommodationSearchByNumberOfGuestsTextBox.Text);
            }
            catch { }

            try
            {
                numberOfDaysForReservation = Convert.ToInt32(AccommodationSearchByNumerOfDaysForReservationTextBox.Text);
            }
            catch { }

            List<Accommodation> accommodations = _accommodationController.GetAll();

            for (int i = 0; i < Accommodations.Count; ++i)
            {
                Accommodations[i].Location = _locationController.GetAll().Find(a => a.Id == Accommodations[i].Location.Id);

            }

            List<Accommodation> filteredAccommodations = new List<Accommodation>();
            filteredAccommodations = GetFiltratedAccommodations(accommodations, numberOfGuests, numberOfDaysForReservation, name, city, country, type);
            AccommodationsDataGrid.ItemsSource = filteredAccommodations;
        }

        public List<Accommodation> GetFiltratedAccommodations(List<Accommodation> accommodations, int numberOfGuests, int numberOfDaysForReservation, string name, string city, string country, string type)
        {
            accommodations = accommodations.Where(a => a.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase)
                                                                                && (a.MaxGuests >= numberOfGuests)
                                                                                && (a.MinDaysForReservation >= numberOfDaysForReservation)
                                                                                && a.Location.City.StartsWith(city, StringComparison.OrdinalIgnoreCase)
                                                                                && a.Location.Country.StartsWith(country, StringComparison.OrdinalIgnoreCase)
                                                                                && a.Type.ToString().StartsWith(type, StringComparison.OrdinalIgnoreCase)).ToList();

            return accommodations;
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccommodationReservationWindow accommodationReservation = new AccommodationReservationWindow(SelectedAccommodation, guest);
                accommodationReservation.Show();
            }
            else
            {
                MessageBox.Show("Prvo morate odabrati smeštaj!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
