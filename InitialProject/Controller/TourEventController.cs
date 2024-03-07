using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourEventController
    {

        private readonly TourEventService _tourEventService;

        public TourEventController()
        {
            _tourEventService = new TourEventService();
        }

        public List<TourEvent> GetAll()
        {
            return _tourEventService.GetAll();
        }

        public TourEvent Get(int id)
        {
            return _tourEventService.Get(id);
        }

        public TourEvent Save(TourEvent tourEvent)
        {

            return _tourEventService.Save(tourEvent);
        }

        public void Delete(TourEvent tourEvent)
        {

            _tourEventService.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventService.Update(tourEvent);
        }

        
        public List<TourEvent> GetByGuide(int id)
        {

            return _tourEventService.GetByTour(id);
        }

        public int CheckAvailability(TourEvent tourEvent)
        {
            return _tourEventService.CheckAvailability(tourEvent);
        }

       /* public List<TourReservation> GetAllTourReservationForTourEvent(TourEvent tourEvent)
        {
            return _tourEventService.GetAllTourReservationForTourEvent(tourEvent);
        }*/

        public List<TourEvent> GetAvailableTourEventsForLocation(Location location, int numberOfPeople)
        {
            return _tourEventService.GetAvailableTourEventsForLocation(location, numberOfPeople);
        }

        
        public List<TourEvent> GetTourEventsForNow()
        {
            return _tourEventService.GetTourEventsForNow(); 
        }

        public void StartTourEvent(TourEvent tourEvent)
        {
            _tourEventService.StartTourEvent(tourEvent);
        }
        public List<TourEvent> GetTourEventsInFuture()
        {
            return _tourEventService.GetTourEventsInFuture();

        }

        public List<TourEvent> GetTourEventsNotPassedForTour(Tour tour)
        {
            return _tourEventService.GetTourEventsNotPassedForTour(tour);
        }

        public List<int> YearsOfTourEvents(int guideId)
        {
            return _tourEventService.YearsOfTourEvents(guideId);
        }
        
        public TourEvent MostVisitedTourEvent(int year = -1) {
            return _tourEventService.MostVisitedTourEvent(year);
        }

        public List<TourEvent> GetAllTourEvents(int guideId)
        {
            return _tourEventService.GetAllTourEvents(guideId);
        }

    }
}
