using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class CreateTourRequestViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public LocationController _locationController;

        public TourRequestController _tourRequestController;

        public TourController _tourController;  //


        public RelayCommand CreateRequestCommand { get; set; }

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

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                if (value != _selectedStartDate)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                if (value != _selectedEndDate)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
        public void Executed_CreateRequestCommand(object obj)
        {
            Location location = _locationController.FindLocationByCountryCity(SelectedCountry, SelectedCity);
            User user = SignInForm.LoggedUser;

            // MaxGuests = 0;

            TourRequest tourRequest = new TourRequest()
            {
                Location = location,
                Language = Language,
                MaxGuests = MaxGuests,
                Description = Description,
                Start = SelectedStartDate,
                End = SelectedEndDate,
                Guest = user,
                Status = Enumerations.RequestStatusType.Standby,
                ComplexTourRequestId = -1
            };


            _tourRequestController.Save(tourRequest);
            Close();
            //NavigationService.Navigate(new Uri("WPF/Views/Guest2Windows/MyTourRequests.xaml", UriKind.Relative));
        }

        public bool CanExecute_CreateRequestCommand(object obj)
        {
            return true;
        }

        public CreateTourRequestViewModel(NavigationService navigationService)
        {
            this.CreateRequestCommand = new RelayCommand(Executed_CreateRequestCommand, CanExecute_CreateRequestCommand);
            this.NavigationService = navigationService;

            _locationController = new LocationController();
            _tourRequestController = new TourRequestController();

            Countries = new ObservableCollection<string>(_locationController.GetAllCountries());
            Cities = new ObservableCollection<string>();

            TourRequest tourRequest = new TourRequest();

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
        }
       
       

            
        
    }
}
