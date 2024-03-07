using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repositories;
using InitialProject.Service.Services;
using InitialProject.Domain.Models;
using InitialProject.Domain.Dto;


namespace InitialProject.Controller
{
    public class TourReservationController
    {
        private readonly TourReservationService _tourReservationService;

        public TourReservationController()
        {
            _tourReservationService = new TourReservationService();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservationService.GetAll();
        }

        public TourReservation Get(int id)
        {
            return _tourReservationService.Get(id);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationService.Save(tourReservation);
        }


        public void Delete(TourReservation tourReservation)
        {
            _tourReservationService.Delete(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationService.Update(tourReservation);
        }

        public List<User> FindGuestsThatDidntComeYet(TourEvent tourEvent) {

            return _tourReservationService.FindGuestsThatDidntComeYet(tourEvent);
        }

        public TourReservation FindTourReservationForUserAndTourEvent(User user, TourEvent tourEvent)
        {
            return _tourReservationService.FindTourReservationForUserAndTourEvent(user, tourEvent);
        }


        public List<TourEvent> UsersTourEvents(int userId)
        {

            return _tourReservationService.UsersTourEvents(userId);
        }

        public void CancelTourReservation(TourReservation tourReservation)
        {
            _tourReservationService.CancelTourReservation(tourReservation);
        }


        public void CancelAllTourReservationsForTourEvent(int tourEventId)
        {
            _tourReservationService.CancelAllTourReservationsForTourEvent(tourEventId);

        }

        public TourReservation GetTourReservationForTourEventAndUser(int tourEventId, int userId)
        {
            return _tourReservationService.GetTourReservationForTourEventAndUser(tourEventId, userId);
        }

        public TourAgeGroupStatistic GetAgeStatisticsForTourEvent(int eventId) {
            return _tourReservationService.GetAgeStatisticsForTourEvent(eventId);
        }

        public TourPercentageOfGuestsVouchers GetPercentageOfGuestsWithVouchersForTourEvent(int eventId) {
            return _tourReservationService.GetPercentageOfGuestsWithVouchersForTourEvent(eventId);
        }

    }
}

