using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationStatisticByYearViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public ObservableCollection<AccommodationByYearStatisticDto> AccommodationStatistics { get; set; }
        public AccommodationReservationController _accommodationReservationController;

        public Accommodation Accommodation { get; set; }

        private AccommodationByYearStatisticDto _selectedStatistic;
        public AccommodationByYearStatisticDto SelectedStatistic
        {
            get { return _selectedStatistic; }

            set
            {
                _selectedStatistic = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand StatisticsByMonthCommand { get; set; }

        public int BestYear { get; set; }
        public AccommodationStatisticByYearViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
            _accommodationReservationController = new AccommodationReservationController();
            AccommodationStatistics = new ObservableCollection<AccommodationByYearStatisticDto>(_accommodationReservationController.GetYearStatisticForAccommodation(Accommodation.Id));

            
            StatisticsByMonthCommand = new RelayCommand(Execute_StatisticsByMonthCommand, CanExecute_StatisticsByMonthCommand);

            BestYear = _accommodationReservationController.GetBestYearForAccommodation(Accommodation.Id);
        }
        public bool CanExecute_StatisticsByMonthCommand(object param)
        {
            return SelectedStatistic != null;
        }

        public void Execute_StatisticsByMonthCommand(object param)
        {
            AccommodationStatisticsByMonth statisticsByMonth = new AccommodationStatisticsByMonth(SelectedStatistic, Accommodation);
            statisticsByMonth.Show();
        }
    }
}
