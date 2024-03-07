using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Windows;
using InitialProject.WPF.Views.OwnerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<int> Years { get; set; }
        public int SelectedYear { get; set; }

        public TourRequestController _tourRequestController { get; set; }

        public RelayCommand InGeneralCommand { get; set; }

        public RelayCommand ViewCommand { get; set; }

        private TourRequestPercentageDto _tourRequestPercentage;
        public TourRequestPercentageDto TourRequestPercentageDto
        {
            get => _tourRequestPercentage;
            set
            {
                if (value != _tourRequestPercentage)
                {
                    _tourRequestPercentage = value;
                    OnPropertyChanged("TourRequestPercentageDto");
                }
            }
        }

        private Dictionary<string, int> _languageRequests;
        public Dictionary<string, int> LanguageRequests
        {
            get => _languageRequests;
            set
            {
                if (_languageRequests != value)
                {
                    _languageRequests = value;
                    OnPropertyChanged("LanguageRequests");
                }
            }
        }

        private Dictionary<string, int> CountRequestsByLanguage()
        {
            var languagesCount = new Dictionary<string, int>();
            var allGuestRequests = _tourRequestController.GetAllTourRequestsForUser(SignInForm.LoggedUser.Id);
            foreach (var request in allGuestRequests)
            {
                var languages = request.Language.Split('|');
                foreach (var language in languages)
                {
                    if (languagesCount.ContainsKey(language))
                    {
                        languagesCount[language]++;
                    }
                    else
                    {
                        languagesCount[language] = 1;
                    }
                }

            }
            return languagesCount;
        }

        
        private Dictionary<string, int> _locationRequests;
        public Dictionary<string, int> LocationRequests
        {
            get => _locationRequests;
            set
            {
                if (_locationRequests != value)
                {
                    _locationRequests = value;
                    OnPropertyChanged("LocationRequests");
                }
            }
        }

        private Dictionary<string, int> CountRequestsByLocation()
        {
            var locationsCount = new Dictionary<string, int>();
            var allGuestRequests = _tourRequestController.GetAllTourRequestsForUser(SignInForm.LoggedUser.Id);
            foreach (var request in allGuestRequests)
            {
                var location = request.Location.City;
                if (locationsCount.ContainsKey(location))
                {
                    locationsCount[location]++;
                }
                else
                {
                    locationsCount[location] = 1;
                }

            }
            return locationsCount;
        }
        

        public TourRequestStatisticsViewModel()
        {

            this.ViewCommand = new RelayCommand(Executed_ViewCommand, CanExecute_ViewCommand);
            this.InGeneralCommand = new RelayCommand(Executed_InGeneralCommand, CanExecute_InGeneralCommand);

            _tourRequestController = new TourRequestController();


            Years = new ObservableCollection<int>(_tourRequestController.YearsOfTourRequests(SignInForm.LoggedUser.Id));
            SelectedYear = -1;

            LanguageRequests = CountRequestsByLanguage();
            LocationRequests = CountRequestsByLocation();
        }

        public void Executed_ViewCommand(object obj)
        {
            TourRequestPercentageDto = _tourRequestController.GetPercentageOfTourRequestForYear(SignInForm.LoggedUser.Id, SelectedYear);
        }

        public bool CanExecute_ViewCommand(object obj)
        {
            return true;
        }

        public void Executed_InGeneralCommand(object obj)
        {
            TourRequestPercentageDto = _tourRequestController.GetPercentageOfTourRequest(SignInForm.LoggedUser.Id);
           


        }

        public bool CanExecute_InGeneralCommand(object obj)
        {
            return true;
        }

    }
}
