using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using InitialProject.Commands;

namespace InitialProject.WPF.ViewModels.GuideViewModels
{
    public class TourStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public TourEventController _tourEventController;
        public TourReservationController _tourReservationController;
        public TourController _tourController;
        public ObservableCollection<TourEvent> TourEvents { get; set; }
        public TourEvent SelectedTourEvent { get; set; }





        #region NotifyProperties
        private TourEvent _bestTourEvent;
        public TourEvent BestTourEvent
        {
            get => _bestTourEvent;
            set
            {
                if (value != _bestTourEvent)
                {
                    _bestTourEvent = value;
                    OnPropertyChanged("BestTourEvent");
                }
            }
        }

        private TourAgeGroupStatistic _tourAge;
        public TourAgeGroupStatistic TourAge
        {
            get => _tourAge;
            set
            {
                if (value != _tourAge)
                {
                    _tourAge = value;
                    OnPropertyChanged("TourAge");
                }
            }
        }

        private TourPercentageOfGuestsVouchers _tourPercentageOfGuestsVouchers;
        public TourPercentageOfGuestsVouchers TourPercentageOfGuestsVouchers
        {
            get => _tourPercentageOfGuestsVouchers;
            set
            {
                if (value != _tourPercentageOfGuestsVouchers)
                {
                    _tourPercentageOfGuestsVouchers = value;
                    OnPropertyChanged("TourPercentageOfGuestsVouchers");
                }
            }
        }


        #endregion

        

        public RelayCommand ViewMostVisitedTourInGeneralCommand { get; set; }

        public TourStatisticsViewModel()
        {
            
            _tourEventController = new TourEventController();
            _tourReservationController = new TourReservationController();
            Years = new ObservableCollection<int>(_tourEventController.YearsOfTourEvents(SignInForm.LoggedUser.Id));
            BestTourEvent = _tourEventController.MostVisitedTourEvent();
            SelectedYear = -1;
            _tourController = new TourController();
            TourEvents = new ObservableCollection<TourEvent>(_tourEventController.GetAllTourEvents(SignInForm.LoggedUser.Id));
            ViewMostVisitedTourInGeneralCommand = new RelayCommand(Executed_ViewMostVisitedTourInGeneralCommand, CanExecute_ViewMostVisitedTourInGeneralCommand);

        }

        public bool CanExecute_ViewMostVisitedTourInGeneralCommand(object obj)
        {
            return SelectedYear != null;
        }

        public void Executed_ViewMostVisitedTourInGeneralCommand(object obj)
        {
            /*if (SelectedYear == -1)
            {
                return;
            }*/
            BestTourEvent = _tourEventController.MostVisitedTourEvent(SelectedYear);
        }
        

        

        private void UpdateTourEventsList()
        {

            foreach (var tourEvent in _tourEventController.GetAllTourEvents(SignInForm.LoggedUser.Id))
            {
                TourEvents.Add(tourEvent);
            }
        }

        public void Update()
        {
            UpdateTourEventsList();
        }

        public bool CanExecute_ViewButtonCommand(object obj)
        {
            return SelectedTourEvent != null;
        }

        public void Executed_ViewButtonCommand(object obj)
        {
            /*if (SelectedTourEvent == null)
            {
                return;
            }*/
            TourAge = _tourReservationController.GetAgeStatisticsForTourEvent(SelectedTourEvent.Id);
            TourPercentageOfGuestsVouchers = _tourReservationController.GetPercentageOfGuestsWithVouchersForTourEvent(SelectedTourEvent.Id);
        }

        
    }
}
