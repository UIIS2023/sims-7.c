using InitialProject.Controller;
﻿using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Service.Services
{
    public class TourEventService
    {

        private TourEventRepository _tourEventRepository;
        private TourReservationRepository _tourReservationRepository;

        private TourPointRepository _tourPointRepository;

        private TourReservationService _reservationService;


        public TourEventService()
        {
            _tourEventRepository = TourEventRepository.GetInstance();
            _tourReservationRepository = TourReservationRepository.GetInstance();

            _tourPointRepository = TourPointRepository.GetInstance();

            _reservationService = new TourReservationService();

        }

        public List<TourEvent> GetAll()
        {
            return _tourEventRepository.GetAll();
        }

        public TourEvent Get(int id)
        {
            return _tourEventRepository.Get(id);
        }

        public TourEvent Save(TourEvent tourEvent)
        {

            return _tourEventRepository.Save(tourEvent);
        }

        public void Delete(TourEvent tourEvent)
        {

            _tourEventRepository.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventRepository.Update(tourEvent);
        }


        public List<TourEvent> GetByTour(int id)
        {

            return _tourEventRepository.GetByTour(id);
        }

        public int CheckAvailability(TourEvent tourEvent)
        {
            int numOfPeople = 0;
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.TourEvent.Id == tourEvent.Id)
                {
                    numOfPeople += tourReservation.NumberOfPeople;
                }
            }
            return numOfPeople;
        }



        public List<TourEvent> GetAvailableTourEventsForLocation(Location location, int numberOfPeople)
        {
            List<TourEvent> tourEvents = new List<TourEvent>();


            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                int freePlaces = tourEvent.Tour.MaxGuests - CheckAvailability(tourEvent);
                if (tourEvent.StartTime > DateTime.Now && tourEvent.Tour.Location.City == location.City && tourEvent.Tour.Location.Country == location.Country && freePlaces > numberOfPeople)
                {
                    tourEvents.Add(tourEvent);
                }
            }
            return tourEvents;
        }



        public List<TourEvent> GetTourEventsForNow()
        {
            List<TourEvent> _tourEventsForNow = new List<TourEvent>();

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {

                if (tourEvent.StartTime.Date == DateTime.Today)
                {
                    _tourEventsForNow.Add(tourEvent);
                }
            }
            return _tourEventsForNow;
        }

        public void StartTourEvent(TourEvent tourEvent)
        {
            TourPoint tourPoint = tourEvent.Tour.TourPoints.ElementAt(0);
            tourPoint.Active = true;
            _tourPointRepository.Update(tourPoint);
            tourEvent.Status = Enumerations.TourEventStatus.Started;
            Update(tourEvent);
        }

        public List<TourEvent> GetTourEventsInFuture()
        {
            List<TourEvent> _tourEventsForNow = new List<TourEvent>();

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if (tourEvent.StartTime.Date > DateTime.Today.AddDays(2))
                {
                    _tourEventsForNow.Add(tourEvent);
                }
            }
            return _tourEventsForNow;
        }

        public void CancelTourEvent(TourEvent tourEvent)
        {
            _reservationService.CancelAllTourReservationsForTourEvent(tourEvent.Id);
            _tourEventRepository.Delete(tourEvent);

        }

        /*
       public List<TourReservation> GetAllTourReservationForTourEvent(TourEvent tourEvent)
       {
          List<TourReservation> tourReservationList = new List<TourReservation>();
          foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
          {
              if (tourReservation.TourEvent.Id == tourEvent.Id)
              {
                  tourReservationList.Add(tourReservation);
              }
          }
          return tourReservationList;
      }*/
        public List<TourEvent> GetTourEventsNotPassedForTour(Tour tour)
        {
            List<TourEvent> tourEventsNotPassed = new List<TourEvent>();

            foreach (TourEvent tourEvent in tour.TourEvents)
            {
                if (tourEvent.StartTime.Date > DateTime.Now.Date)
                {
                    tourEventsNotPassed.Add(tourEvent);
                }
            }

            return tourEventsNotPassed;
        }

        
        public TourEvent MostVisitedTourEvent(int year = -1)
        {
            TourEvent mostVisitedTourEvent = null;
            int maxPeopleCame = -1;

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if (tourEvent.Status != Enumerations.TourEventStatus.Finished)
                {
                    continue;  
                }
                if (year != -1)
                {
                    if (tourEvent.StartTime.Year != year)
                    {
                        continue;
                    }
                }
                int peopleCame = _reservationService.GetAllTourReservationsForTourEventWherePeopleShowed(tourEvent.Id).Count();
                if (peopleCame > maxPeopleCame)
                {
                    mostVisitedTourEvent = tourEvent;
                    maxPeopleCame = peopleCame;
                }
            }
            return mostVisitedTourEvent;
        }

        public List<int> YearsOfTourEvents(int guideId)
        {
            List<int> years = new List<int>();
            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if (tourEvent.Tour.Guide.Id == guideId)
                {
                    years.Add(tourEvent.StartTime.Year);
                }
            }
            return years.Distinct().ToList();
        }

        public List<TourEvent> GetAllTourEvents(int guideId)
        {
            List<TourEvent> _allTourEvents = new List<TourEvent>();

            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if (tourEvent.Tour.Guide.Id == guideId)
                {
                    _allTourEvents.Add(tourEvent);
                }
            }
            return _allTourEvents;
        }

    }
}
