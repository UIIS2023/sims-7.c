using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = InitialProject.Domain.Models.Image;


namespace InitialProject.WPF.Views.GuideWindows
{
    /// <summary>
    /// Interaction logic for CreateTourWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : Page, IDataErrorInfo
    {
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }



        LocationController _locationController;

        TourController _tourController;
        TourEventController _tourEventController;
        public List<Image> AllImages { get; set; }
        public List<TourPoint> AllTourPoints { get; set; }

        public CreateTourWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Tour Tour = new Tour();
            nameTextBox.Focus();

            _locationController = new LocationController();
            _tourController = new TourController();
            _tourEventController = new TourEventController();


            Countries = new ObservableCollection<string>(_locationController.GetAllCountries());
            Cities = new ObservableCollection<string>();

            AllImages = new List<Image>();
            AllTourPoints = new List<TourPoint>();

            SelectedDate = DateTime.Now;
        }

        #region NotifyProperties
        private string _name;
        public string Namee
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Namee");
                }
            }
        }
        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _languages;
        public string Languages
        {
            get => _languages;
            set
            {
                if (value != _languages)
                {
                    _languages = value;
                    OnPropertyChanged("Languages");
                }
            }
        }

        private int _maxGuests;
        public int MaxGuestss
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuestss");
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (value != _selectedDate)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }


        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
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

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion



        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                Location location = _locationController.FindLocationByCountryCity(SelectedCountry, SelectedCity);
                User user = SignInForm.LoggedUser;

                Tour tour = new Tour()
                {
                    Name = Namee,
                    Location = location,
                    Description = Description,
                    Languages = Languages,
                    MaxGuests = MaxGuestss,
                    Duration = Duration,
                    Guide = user,
                    Images = AllImages,
                    TourPoints = AllTourPoints
                };

                _tourController.SaveImagesTourPoints(tour);

                TourEvent tourEvent = new TourEvent(-1, tour, SelectedDate, TourEventStatus.NotStarted);
                _tourEventController.Save(tourEvent);
                MessageBox.Show("Kreirali ste turu!");

            }
            else
            {
                MessageBox.Show("Niste dobro uneli podatke");
            }







        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CountryComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

            List<string> cities = _locationController.GetCitiesByCountry(SelectedCountry);
            Cities.Clear();
            foreach (string city in cities)
            {
                Cities.Add(city);
            }

        }

       

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddImages_Click(object sender, RoutedEventArgs e)
        {
            AddTourImages TourImage = new AddTourImages(ImageResource.Tour, AllImages);
            TourImage.Show();

        }

        private void LanguageComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == 0)
            {
                Languages = "srpski";
            }
            else if (LanguageComboBox.SelectedIndex == 1)
            {

                Languages = "engleski";

            }
            else if (LanguageComboBox.SelectedIndex == 2)
            {

                Languages = "italijanski";

            }
            else if (LanguageComboBox.SelectedIndex == 3)
            {
                Languages = "korejski";
            }
            else if (LanguageComboBox.SelectedIndex == 4)
            {
                Languages = "japanski";
            }


        }



        private void AddTourPoint_Click(object sender, RoutedEventArgs e)
        {
            AddTourPoints TourPoint = new AddTourPoints(AllTourPoints);
            TourPoint.Show();

        }



        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MaxGuestsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Validacija
        private Regex _NameRegex = new Regex("[A-Z]{1}[a-z]*");
        private Regex _DurationRegex = new Regex("[0-9]*");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Namee")
                {
                    if (string.IsNullOrEmpty(Namee))
                        return "Ime je obavezno";

                    Match match = _NameRegex.Match(Namee);
                    if (!match.Success)
                        return "Ime moze imati samo mala i velika slova";
                }
                else if (columnName == "SelectedCountry")
                {
                    if (string.IsNullOrEmpty(SelectedCountry))
                        return "Drzava je obavezna";
                }
                else if (columnName == "SelectedCity")
                {
                    if (string.IsNullOrEmpty(SelectedCity))
                        return "Grad je obavezan";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Opis je obavezan";
                }
                else if (columnName == "Languages")
                {
                    if (string.IsNullOrEmpty(Languages))
                        return "Jezik je obavezan";
                }
                else if (columnName == "MaxGuestss")
                {
                    if (string.IsNullOrEmpty(MaxGuestss.ToString()))
                        return "Max broj gostiju je obavezan";
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Trajanje je obavezano";
                    Match match = _DurationRegex.Match(Duration.ToString());
                    if (!match.Success)
                        return "Trajanje moze biti samo broj";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Namee", "SelectedCountry", "SelectedCity", "Description", "Languages", "MaxGuestss", "Duration" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
     
     
    }
}
