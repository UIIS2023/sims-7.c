using InitialProject.Controller;
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
using InitialProject.Enumerations;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2Windows;
using System.ComponentModel;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Resources;
using InitialProject.Properties;

namespace InitialProject.WPF.Views.Guest2Window
{
    /// <summary>
    /// Interaction logic for ToursOverviewWindow.xaml
    /// </summary>
    public partial class ToursOverviewWindow : UserControl, IDataErrorInfo  
    {
        public ObservableCollection<Tour> Tours { get; set; }

        public ObservableCollection<Tour> Images { get; set; }

        public TourController _tourController;

        public ImageController _imageController;

        public string Country { get; set; }

        public string City { get; set; }

        public string Languages { get; set; }

        public string Duration { get; set; }
        
        public string NumberOfPeople { get; set; }

        public Tour SelectedTour { get; set; }


        public ToursOverviewWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            _tourController = new TourController();

            Tours = new ObservableCollection<Tour>(_tourController.GetAll());


            Country = "";
            City = "";
            Languages = "";
            Duration = "";
            NumberOfPeople = "";
            
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                TourReservationWindow tourReservation = new TourReservationWindow(SelectedTour);
                tourReservation.Show();
            }
            else
            {
                //MessageBox.Show(Properties.Resources.SelectTour, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(Localization.TranslationSource.Instance["Please select a tour!"], Localization.TranslationSource.Instance["Error"], MessageBoxButton.OK);
            }
        }

        private void RefreshTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach(Tour tour in tours)
            {
                Tours.Add(tour);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(IsValid)
            {
                List<Tour> searchedTours = _tourController.SearchTours(Country, City, Languages, NumberOfPeople, Duration);
                RefreshTours(searchedTours);
            }
            else
            {
                MessageBox.Show("Neispravno popunjeni podaci", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        private void RefreshButtons()
        {
            foreach (Button tb in FindVisualChildren<Button>(this))
            {
                tb.Background = Brushes.LightGray;
            }
        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tourId = Convert.ToInt32(button.Tag);
            SelectedTour = _tourController.Get(tourId);
            RefreshButtons();
            button.Background = Brushes.RosyBrown;
        }

        //Validacija

        private Regex _CountryRegex = new Regex("[A-Z][a-z]*");

        private Regex _LanguageRegex = new Regex("[a-z]*");
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Country")
                {
                        
                    Match match = _CountryRegex.Match(Country);
                    if (!match.Success)
                        return "Samo slova!";
                }
                if (columnName == "City")
                {

                    Match match = _CountryRegex.Match(Country);
                    if (!match.Success)
                        return "Samo slova!";
                }
                if (columnName == "Language")
                {

                    Match match = _LanguageRegex.Match(Country);
                    if (!match.Success)
                        return "Samo slova!";
                }
                if (columnName == "Duration")
                {
                    int duration;
                    if (!int.TryParse(Duration, out duration) || duration <= 0)
                    {
                        return "Pozitivan broj!";
                    }
                }

                if (columnName == "NumberOfPeople")
                {
                    int maxGuests;
                    if (!int.TryParse(NumberOfPeople, out maxGuests) || maxGuests <= 0)
                    {
                        return "Pozitivan broj!";
                    }
                }

                return null;

            }

        }
        private readonly string[] _validatedProperties = { "Country", "City", "Language","Duration", "NumberOfPeople" };

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
