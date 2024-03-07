using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service.Services
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservationRepository;
        private ReservationRescheduleRequestService _reservationRescheduleService;
        private AccommodationOwnerReviewService _accommodationOwnerService;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();
            _reservationRescheduleService = new ReservationRescheduleRequestService();
            _accommodationOwnerService = new AccommodationOwnerReviewService();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public AccommodationReservation Get(int id)
        {
            return _accommodationReservationRepository.Get(id);
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {

            return _accommodationReservationRepository.Save(accommodationReservation);
        }

        public void Delete(AccommodationReservation accommodationReservation)
        {

            _accommodationReservationRepository.Delete(accommodationReservation);

        }

        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationRepository.Update(accommodationReservation);
        }

        public int NextId()
        {

            return _accommodationReservationRepository.NextId();

        }

        private bool IsGuestWithoutReview(AccommodationReservation accommodationReservation)
        {
            bool retVal = accommodationReservation.GuestReview.Id == -1 && DateTime.Now >= accommodationReservation.End
                           && DateTime.Now <= accommodationReservation.End.AddDays(5);

            return retVal;
        }

        public List<AccommodationReservation> GetAllReservationsWithoutGuestReview()
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (IsGuestWithoutReview(accommodationReservation) && accommodationReservation.Accommodation.Owner.Id == SignInForm.LoggedUser.Id)
                {
                    accommodationReservations.Add(accommodationReservation);
                }
            }

            return accommodationReservations;
        }

        public int FindNumberOfGuestsWithoutReview()
        {
            int number;
            List<AccommodationReservation> accommodationReservations = GetAllReservationsWithoutGuestReview();
            return number = accommodationReservations.Count;

        }

        public List<AccommodationReservation> GetByUserId(int guest)
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.Guest.Id == guest)
                {
                    accommodationReservations.Add(accommodationReservation);
                }
            }

            return accommodationReservations;
        }

        public List<AccommodationReservation> GetAllReservationsWithoutAccommodationOwnerReview(int guest)
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.Guest.Id == guest)
                {
                    if (IsAccommodationWithoutReview(accommodationReservation))
                    {
                        accommodationReservations.Add(accommodationReservation);
                    }
                }
            }
            return accommodationReservations;
        }

        private bool IsAccommodationWithoutReview(AccommodationReservation accommodationReservation)
        {
            bool retVal = accommodationReservation.AccommodationReview.Id == -1 /*&& DateTime.Now >= accommodationReservation.End
                           && DateTime.Now <= accommodationReservation.End.AddDays(5)*/;

            return retVal;
        }

        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            List<AccommodationReservation> reservations = GetAll();
            return reservations.Where(reservation => reservation.Accommodation.Id == id).ToList();
        }

        public Boolean AvailableAccommodation(AccommodationReservation accommodationReservation, int numberOfDaysForReservation)
        {
            List<AccommodationReservation> bookedReservations = GetByAccommodationId(accommodationReservation.Accommodation.Id);
             foreach (AccommodationReservation bookedReservation in bookedReservations)
            {
                if (bookedReservation.IsCancelled == false)
                {
                    if (accommodationReservation.Start >= bookedReservation.Start && accommodationReservation.Start < bookedReservation.End)
                    {
                        DateTime startSuggestion = bookedReservation.End;
                        DateTime endSuggestion = bookedReservation.End.AddDays(numberOfDaysForReservation);
                        MessageBox.Show("Ovaj smeštaj je zauzet u izabranom periodu!\n\nPreporuka za početni datum: " + startSuggestion.ToString("dd.MM.yyyy.") + "\nPreporuka za krajnji datum: " + endSuggestion.ToString("dd.MM.yyyy."), "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

            }
            return true;
        }


        public DateTime GetFirstAvailableDate(AccommodationReservation accommodationReservation)
        {
            List<AccommodationReservation> bookedReservations = GetByAccommodationId(accommodationReservation.Accommodation.Id);

            bookedReservations.Sort((r1, r2) => r1.Start.CompareTo(r2.Start));

            DateTime availableDate = accommodationReservation.Start;

            foreach (AccommodationReservation bookedReservation in bookedReservations)
            {
                if (availableDate < bookedReservation.Start)
                {
                    return availableDate;
                }

                availableDate = bookedReservation.End;
            }
            return availableDate;
        }
        public bool IsDatesIntertwine(DateTime StartFirst, DateTime EndFirst, DateTime StartSecond, DateTime EndSecond)
        {
            return (StartSecond.Date <= EndFirst.Date && EndSecond.Date >= StartFirst.Date);
        }

        public bool IsReschedulePossible(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            List<AccommodationReservation> reservations = _accommodationReservationRepository.GetByAccommodationId(reservationRescheduleRequest.Reservation.Accommodation.Id);
            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.Id == reservationRescheduleRequest.Reservation.Id)
                {
                    reservations.Remove(reservation);
                    break;
                }
            }
            foreach (AccommodationReservation reservation in reservations)
            {
                if (IsDatesIntertwine(reservation.Start, reservation.End, reservationRescheduleRequest.NewStart, reservationRescheduleRequest.NewEnd))
                {
                    return false;
                }
            }

            return true;
        }





        public List<AccommodationReservation> GetCancelledByUserId(int guest)
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.Guest.Id == guest && accommodationReservation.IsCancelled == false)
                {
                    accommodationReservations.Add(accommodationReservation);
                }
            }

            return accommodationReservations;
        }

        //Year statisic
        public AccommodationByYearStatisticDto CheckIfReservationIsCancelledOrRescheduledForYear(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            if (reservation.IsCancelled)
            {
                byYear.CancelledReservationsNum++;
            }
            if (_reservationRescheduleService.IsReservationRescheduled(reservation))
            {
                byYear.RescheduledReservationsNum++;
            }
            if (_accommodationOwnerService.IsReservationWithRenovationRecommendations(reservation))
            {
                byYear.RecommendationForRenovationNum++;
            }

            return byYear;
        }

        private AccommodationByYearStatisticDto AddReservationYearWhichNotExists(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            byYear.Year = reservation.Start.Year;
            byYear.CancelledReservationsNum = 0;
            byYear.ReservationsNum = 1;
            byYear.RescheduledReservationsNum = 0;
            byYear = CheckIfReservationIsCancelledOrRescheduledForYear(byYear, reservation);
            return byYear;
        }

        private AccommodationByYearStatisticDto AddReservationYearWhichExists(AccommodationByYearStatisticDto byYear, AccommodationReservation reservation)
        {
            byYear.ReservationsNum++;
            byYear = CheckIfReservationIsCancelledOrRescheduledForYear(byYear, reservation);

            return byYear;
        }

        private void AddReservationYearToStatistics(List<AccommodationByYearStatisticDto> statistics, AccommodationReservation reservation)
        {
            AccommodationByYearStatisticDto byYear = null;
            foreach (AccommodationByYearStatisticDto accommodationByYearStatisticDto in statistics)
            {
                if (accommodationByYearStatisticDto.Year == reservation.Start.Year)
                {
                    byYear = accommodationByYearStatisticDto;
                    break;
                }
            }
            if (byYear == null)
            {
                byYear = new AccommodationByYearStatisticDto(0, 0, 0, 0, 0);
                statistics.Add(AddReservationYearWhichNotExists(byYear, reservation));
            }
            else
            {
                AddReservationYearWhichExists(byYear, reservation);
            }
        }

        public List<AccommodationByYearStatisticDto> GetYearStatisticForAccommodation(int accommodationId)
        {
            List<AccommodationByYearStatisticDto> statistics = new List<AccommodationByYearStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationYearToStatistics(statistics, reservation);
            }

            return statistics;
        }

        //Month statistic
        public AccommodationByMonthStatisticDto CheckIfReservationIsCancelledOrRescheduledForMonth(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            if (reservation.IsCancelled)
            {
                byMonth.CancelledReservationsNum++;
            }
            if (_reservationRescheduleService.IsReservationRescheduled(reservation))
            {
                byMonth.RescheduledReservationsNum++;
            }
            if (_accommodationOwnerService.IsReservationWithRenovationRecommendations(reservation))
            {
                byMonth.RecommendationForRenovationNum++;
            }
            return byMonth;
        }
        private AccommodationByMonthStatisticDto AddReservationMonthWhichNotExists(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            byMonth.Month = reservation.Start.Month;
            byMonth.CancelledReservationsNum = 0;
            byMonth.ReservationsNum = 1;
            byMonth.RescheduledReservationsNum = 0;
            byMonth = CheckIfReservationIsCancelledOrRescheduledForMonth(byMonth, reservation);
            return byMonth;
        }

        private AccommodationByMonthStatisticDto AddReservationMonthWhichExists(AccommodationByMonthStatisticDto byMonth, AccommodationReservation reservation)
        {
            byMonth.ReservationsNum++;
            byMonth = CheckIfReservationIsCancelledOrRescheduledForMonth(byMonth, reservation);
            return byMonth;
        }

        private void AddReservationMonthToStatistics(List<AccommodationByMonthStatisticDto> statistics, AccommodationReservation reservation, int year)
        {
            AccommodationByMonthStatisticDto byMonth = null;
            foreach(AccommodationByMonthStatisticDto accommodationByMonthStatisticDto in statistics)
            {
                if(accommodationByMonthStatisticDto.Month == reservation.Start.Month && year == reservation.Start.Year)
                {
                    byMonth = accommodationByMonthStatisticDto;
                    break;
                }
            }
            if (byMonth == null)
            {
                byMonth = new AccommodationByMonthStatisticDto(0, 0, 0, 0, 0);
                statistics.Add(AddReservationMonthWhichNotExists(byMonth, reservation));
            }
            else
            {
                AddReservationMonthWhichExists(byMonth, reservation);
            }
        }

        public List<AccommodationByMonthStatisticDto> GetMonthStatisticForAccommodation(int year, int accommodationId)
        {
            List<AccommodationByMonthStatisticDto> statisticsByMonth = new List<AccommodationByMonthStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationMonthToStatistics(statisticsByMonth, reservation, year);
            }

            return statisticsByMonth;
        }

        //Best year
        private void AddReservationYearToBestStatistics(List<BestStatisticDto> statistics, AccommodationReservation reservation)
        {
            BestStatisticDto byYear = null;
            foreach (BestStatisticDto bestStatisticDto in statistics)
            {
                if (bestStatisticDto.Year == reservation.Start.Year)
                {
                    byYear = bestStatisticDto;
                    break;
                }
            }
            if (byYear == null)
            {
                byYear = new BestStatisticDto() { Year = reservation.Start.Year, DaysReserved = (int)(reservation.End - reservation.Start).TotalDays };
                statistics.Add(byYear);
            }
            else
            {
                byYear.DaysReserved += (int)(reservation.End - reservation.Start).TotalDays;
            }
        }

        public int GetBestYearForAccommodation(int accommodationId)
        {
            List<BestStatisticDto> statistics = new List<BestStatisticDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationYearToBestStatistics(statistics, reservation);
            }
            int max = 0;
            if (statistics.Any())
            {
                max = statistics.Max(i => i.DaysReserved);
            }
            else
            {
                return max;
            }
            BestStatisticDto bestStatistic = statistics.First(x => x.DaysReserved == max);
            return bestStatistic.Year;
        }

        //Best month
        private void AddReservationMonthToBestStatistics(int year, List<BestStatisticMonthDto> statistics, AccommodationReservation reservation)
        {
            BestStatisticMonthDto byMonth = null;
            foreach (BestStatisticMonthDto bestStatisticDto in statistics)
            {
                if (bestStatisticDto.Month == reservation.Start.Month && reservation.Start.Year == year)
                {
                    byMonth = bestStatisticDto;
                    break;
                }
            }
            if (byMonth == null)
            {
                byMonth = new BestStatisticMonthDto() { Month = reservation.Start.Month, DaysReserved = (int)(reservation.End - reservation.Start).TotalDays };
                statistics.Add(byMonth);
            }
            else
            {
                byMonth.DaysReserved += (int)(reservation.End - reservation.Start).TotalDays;
            }
        }

        public int GetBestMonthForAccommodation(int year, int accommodationId)
        {
            List<BestStatisticMonthDto> statistics = new List<BestStatisticMonthDto>();
            List<AccommodationReservation> reservations = GetByAccommodationId(accommodationId);

            foreach (AccommodationReservation reservation in reservations)
            {
                AddReservationMonthToBestStatistics(year, statistics, reservation);
            }

            int max = statistics.Max(i => i.DaysReserved);
            BestStatisticMonthDto bestStatistic = statistics.First(x => x.DaysReserved == max);

            return bestStatistic.Month;
        }


    }
}
