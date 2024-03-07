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
    public class AccommodationStatisticsByMonthViewModel : ViewModelBase, IClose
    {
        public Action Close { get; set; }

        public AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationByMonthStatisticDto> AccommodationStatisticsByMonth { get; set; }
        public RelayCommand CloseStatisticsByMonthCommand { get; set; }

        public Accommodation Accommodation { get; set; }
        public AccommodationByYearStatisticDto SelectedStatisticByYear { get; set; }
        public int bestMonth { get; set; }
        public string BestMonth { get; set; }
        public AccommodationStatisticsByMonthViewModel(AccommodationByYearStatisticDto statisticByYear, Accommodation accommodation)
        {
            _accommodationReservationController = new AccommodationReservationController();
            CloseStatisticsByMonthCommand = new RelayCommand(Execute_CloseStatisticsByMonthCommand);
            SelectedStatisticByYear = statisticByYear;
            Accommodation = accommodation;
            AccommodationStatisticsByMonth = new ObservableCollection<AccommodationByMonthStatisticDto>(_accommodationReservationController.GetMonthStatisticForAccommodation(statisticByYear.Year, Accommodation.Id));

            bestMonth = _accommodationReservationController.GetBestMonthForAccommodation(SelectedStatisticByYear.Year, Accommodation.Id);

            if(bestMonth == 1)
            {
                BestMonth = "januara";
            }
            else if(bestMonth == 2)
            {
                BestMonth = "februara";
            }else if(bestMonth == 3)
            {
                BestMonth = "marta";
            }else if(bestMonth == 4)
            {
                BestMonth = "aprila";
            }else if(bestMonth == 5)
            {
                BestMonth = "maja";
            }else if(bestMonth == 6)
            {
                BestMonth = "juna";
            }else if(bestMonth == 7)
            {
                BestMonth = "jula";
            }
            else if (bestMonth == 8)
            {
                BestMonth = "avgusta";
            }
            else if (bestMonth == 9)
            {
                BestMonth = "septembra";
            }
            else if (bestMonth == 10)
            {
                BestMonth = "oktobra";
            }
            else if (bestMonth == 11)
            {
                BestMonth = "decembra";
            }
            else if (bestMonth == 12)
            {
                BestMonth = "decembra";
            }

        }

        public void Execute_CloseStatisticsByMonthCommand(object param)
        {
            Close();
        }



    }
}
