using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class ComplexTourRequestService
    {
        private IComplexTourRequestRepository _tourRequestRepository;
        private ITourRequestRepository _simpleTourRequestRepository;
        public ComplexTourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<IComplexTourRequestRepository>();
            _simpleTourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public ComplexTourRequest Get(int id)
        {
            return _tourRequestRepository.Get(id);
        }

        public ComplexTourRequest Save(ComplexTourRequest request)
        {
            return _tourRequestRepository.Save(request);
        }

        public ComplexTourRequest Update(ComplexTourRequest request)
        {
            return _tourRequestRepository.Update(request);
        }

        public void Delete(ComplexTourRequest request)
        {
            _tourRequestRepository.Delete(request);
        }

        public int NextId()
        {
            return _tourRequestRepository.NextId();
        }

        public List<ComplexTourRequest> GetAllComplexTourRequestsForUser(int userId)
        {
            List<ComplexTourRequest> myComplexRequests = new List<ComplexTourRequest>();
            var allComplexRequests = _tourRequestRepository.GetAll();

            for (int i = 0; i < allComplexRequests.Count(); i++)
            {
                var request = allComplexRequests.ElementAt(i);
                if (request.Guest.Id == userId)
                {
                    List<TourRequest> simpleRequests = GetSimpleTourRequestsForComplexRequest(request.Id);
                    request.SimpleTourRequests = simpleRequests;

                    bool allAccepted = simpleRequests.All(simpleRequest => simpleRequest.Status == RequestStatusType.Approved);

                    bool allDeclined = simpleRequests.All(simpleRequest => simpleRequest.Status == RequestStatusType.Declined);

                    bool within48Hours = (simpleRequests[0].Start - DateTime.Today).TotalHours <= 48;

                    if (allAccepted)
                    {
                        request.Status = RequestStatusType.Approved;
                    }
                    else if (within48Hours && allDeclined && request.Status == RequestStatusType.Standby)
                    {
                        request.Status = RequestStatusType.Declined;
                    }

                    _tourRequestRepository.Update(request);
                    
                    myComplexRequests.Add(request);
                }
            }

            return myComplexRequests;
        }

        public List<TourRequest> GetSimpleTourRequestsForComplexRequest(int complexRequestId)
        {
            List<TourRequest> tourRequests = new List<TourRequest>();
            foreach (TourRequest request in _simpleTourRequestRepository.GetAll()) 
            {
                if(request.ComplexTourRequestId == complexRequestId)
                {
                    tourRequests.Add(request);
                }

            }

            return tourRequests;
        }
    }
}
