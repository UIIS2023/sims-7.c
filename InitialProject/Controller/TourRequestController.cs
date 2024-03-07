using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourRequestController
    {
        private readonly TourRequestService _tourRequestService;

        public TourRequestController()
        {
            _tourRequestService = new TourRequestService();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }

        public TourRequest Get(int id)
        {
            return _tourRequestService.Get(id);
        }

        public TourRequest Save(TourRequest tourRequest)
        {

            return _tourRequestService.Save(tourRequest);
        }

        public void Delete(TourRequest tourRequest)
        {

            _tourRequestService.Delete(tourRequest);

        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestService.Update(tourRequest);
        }

        public int NextId()
        {
            return _tourRequestService.NextId();
        }
        public List<TourRequest> GetAllTourRequestsForUser(int userId)
        {
            return _tourRequestService.GetAllTourRequestsForUser(userId);
        }

        public List<int> YearsOfTourRequests(int guestId)
        {
            return _tourRequestService.YearsOfTourRequests(guestId);
        }

        public TourRequestPercentageDto GetPercentageOfTourRequest(int userId)
        {
            return _tourRequestService.GetPercentageOfTourRequest(userId);
        }

        public TourRequestPercentageDto GetPercentageOfTourRequestForYear(int userId, int year)
        {
            return _tourRequestService.GetPercentageOfTourRequestForYear(userId, year);
        }
    }
}
