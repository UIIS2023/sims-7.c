using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Enumerations;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class TourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        private INewTourNotificationRepository _newTourNotificationRepository;
        public TourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
            _newTourNotificationRepository = Injector.Injector.CreateInstance<INewTourNotificationRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest Get(int id)
        {
            return _tourRequestRepository.Get(id);
        }

        public TourRequest Save(TourRequest request)
        {
            return _tourRequestRepository.Save(request);
        }

        public TourRequest Update(TourRequest request)
        {
            return _tourRequestRepository.Update(request);
        }

        public void Delete(TourRequest request)
        {
            _tourRequestRepository.Delete(request);
        }

        public int NextId()
        {
            return _tourRequestRepository.NextId();
        }

        public List<TourRequest> GetAllTourRequestsForUser(int userId)
        {
            List<TourRequest> myRequests = new List<TourRequest>();
            var allRequests = _tourRequestRepository.GetAll();

            for (int i = 0; i < allRequests.Count(); i++)
            {
                var request = allRequests.ElementAt(i);
                if (request.Guest.Id == userId)
                {
                    if ((request.Start - DateTime.Today).TotalDays <= 2 && request.Status == RequestStatusType.Standby)
                    {
                        request.Status = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), "Declined");
                        _tourRequestRepository.Update(request);
                    }
                    myRequests.Add(request);
                }
            }

            return myRequests;
        }


        public List<int> YearsOfTourRequests(int guestId)
        {
            List<int> years = new List<int>();
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.Guest.Id == guestId)
                {
                    years.Add(tourRequest.Start.Year);
                }
            }
            return years.Distinct().ToList();
        }

        public TourRequestPercentageDto GetPercentageOfTourRequest(int userId)
        {
            int acceptedRequests = 0;
            int rejectedRequests = 0;
            int numberOfPeopleInAcceptedRequests = 0;

            TourRequestPercentageDto tourRequestPercentage = new TourRequestPercentageDto(0, 0, 0);

            foreach (TourRequest tourRequest in GetAllTourRequestsForUser(userId))
            {
                if (tourRequest.Status == RequestStatusType.Approved)
                {
                    acceptedRequests += 1;
                    numberOfPeopleInAcceptedRequests += tourRequest.MaxGuests;
                }
                else if(tourRequest.Status == RequestStatusType.Declined)
                {
                    rejectedRequests += 1;
                }
            }

            CalculateRequestPercentage(tourRequestPercentage, acceptedRequests, rejectedRequests, numberOfPeopleInAcceptedRequests);

            return tourRequestPercentage;
        }

        public TourRequestPercentageDto GetPercentageOfTourRequestForYear(int userId,int year)
        {
            int acceptedRequests = 0;
            int rejectedRequests = 0;
            int numberOfPeopleInAcceptedRequests = 0;

            TourRequestPercentageDto tourRequestPercentage = new TourRequestPercentageDto(0, 0, 0);

            foreach (TourRequest tourRequest in GetAllTourRequestsForUser(userId))
            {
                if (tourRequest.Start.Year == year)
                {
                    if (tourRequest.Status == RequestStatusType.Approved)
                    {
                        acceptedRequests += 1;
                        numberOfPeopleInAcceptedRequests += tourRequest.MaxGuests;
                    }
                    else if (tourRequest.Status == RequestStatusType.Declined)
                    {
                        rejectedRequests += 1;
                    }
                }
                
            }

            CalculateRequestPercentage(tourRequestPercentage, acceptedRequests, rejectedRequests, numberOfPeopleInAcceptedRequests);

            return tourRequestPercentage;
        }

        private void CalculateRequestPercentage(TourRequestPercentageDto tourRequestPercentage, int acceptedRequests, int rejectedRequests,int numberOfPeopleInAcceptedRequests)
        {
            double totalRequests = acceptedRequests + rejectedRequests;

            if (totalRequests > 0)
            {
                double acceptedRequestsPercentage = (acceptedRequests * 100.0) / totalRequests;   //decimal bolje
                double rejectedRequestsPercentage = (rejectedRequests * 100.0) / totalRequests;
                tourRequestPercentage.PercentageOfAcceptedRequests = (int)Math.Round(acceptedRequestsPercentage);
                tourRequestPercentage.PercentageOfRejectedRequests = (int)Math.Round(rejectedRequestsPercentage);

                if(acceptedRequests > 0)
                {
                    tourRequestPercentage.AverageNumberOfPeopleInAcceptedRequests = numberOfPeopleInAcceptedRequests / acceptedRequests;
                }               
            }
        }


     

        private void AddUnfulfilledRequest(List<TourRequest> unfullfilledRequests, TourRequest requestToAdd)
        {
            foreach (TourRequest tourRequest in unfullfilledRequests)
            {
                if (tourRequest.Guest.Id == requestToAdd.Guest.Id)
                {
                    return;
                }
            }

            foreach (TourRequest request in GetAllTourRequestsForUser(requestToAdd.Guest.Id))
            {
                if (request.Status == RequestStatusType.Approved && request.Language == requestToAdd.Language && request.Location.City == requestToAdd.Location.City)
                {
                    return;
                }
            }
            unfullfilledRequests.Add(requestToAdd);
        }

        public void GenerateNewTourNotifications(Tour tour)  
        {
            List<TourRequest> unfullfilledRequests = new List<TourRequest>();
            foreach (TourRequest request in _tourRequestRepository.GetAll())
            {
                if (IsUnfulfilledTourRequest(request, tour))
                {
                    AddUnfulfilledRequest(unfullfilledRequests, request);
                }
            }


            foreach (TourRequest tourRequest in unfullfilledRequests)
            {
                NewTourNotification newTourNotification = new NewTourNotification() { Tour = tour, Guest = tourRequest.Guest, IsDelivered = false };
                _newTourNotificationRepository.Save(newTourNotification);
            }
        }

        private bool IsUnfulfilledTourRequest(TourRequest request, Tour tour)
        {
            return request.Status != RequestStatusType.Approved &&
                   (request.Location.City == tour.Location.City || request.Language == tour.Languages);
        }
    }
}
