using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.GuideWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace InitialProject.WPF.ViewModels.GuideViewModels
{
    public class ReviewsViewModel : ViewModelBase
    {
        public ObservableCollection<GuideReview> Guests { get; set; }
        public GuideReviewController _guideReviewController;

        public NavigationService navigationService { get; set; } //?


        #region NotifyProperties
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private GuideReview _selectedGuest;

        public GuideReview SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                if (_selectedGuest != value)
                {
                    _selectedGuest = value;
                    OnPropertyChanged(nameof(SelectedGuest));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private bool _validity;
        public bool Validity
        {
            get => _validity;
            set
            {
                if (value != _validity)
                {
                    _validity = value;
                    OnPropertyChanged("Validity");
                }
            }
        }

        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                if (value != _header)
                {
                    _header = value;
                    OnPropertyChanged("Header");
                }
            }
        }

        #endregion

       

        public RelayCommand ShowButtonCommand { get; set; }

        public ReviewsViewModel(NavigationService service)
        {
            this.navigationService = service;
            _guideReviewController = new GuideReviewController();
            Guests = new ObservableCollection<GuideReview>(_guideReviewController.GetAll());
            ShowButtonCommand = new RelayCommand(Executed_ShowButtonCommand,CanExecute_ShowButtonCommand);


        }

        public bool CanExecute_ShowButtonCommand(object obj)
        {
            return SelectedGuest != null;
        }

        public void Executed_ShowButtonCommand(object obj)
        {
            ReviewsInformationWindow reviews = new ReviewsInformationWindow(SelectedGuest);
            this.navigationService.Navigate(reviews);
        }


       


    }
}
