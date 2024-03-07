using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class AccommodationByMonthStatisticDto
    {
        public int Month { get; set; }
        public int ReservationsNum { get; set; }
        public int CancelledReservationsNum { get; set; }
        public int RescheduledReservationsNum { get; set; }
        public int RecommendationForRenovationNum { get; set; }
        public AccommodationByMonthStatisticDto(int month, int reservationsNum, int cancelledReservationsNum, int rescheduledReservationsNum, int recommendationForRenovationNum)
        {
            Month = month;
            ReservationsNum = reservationsNum;
            CancelledReservationsNum = cancelledReservationsNum;
            RescheduledReservationsNum = rescheduledReservationsNum;
            RecommendationForRenovationNum = recommendationForRenovationNum;
        }


    }
}
