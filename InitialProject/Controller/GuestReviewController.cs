using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class GuestReviewController
    {
        private readonly GuestReviewService _guestReviewService;

        public GuestReviewController()
        {
            _guestReviewService = new GuestReviewService();
        }

        public List<GuestReview> GetAll()
        {
            return _guestReviewService.GetAll();
        }

        public GuestReview Get(int id)
        {
            return _guestReviewService.Get(id);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            return _guestReviewService.Save(guestReview);
        }

        public void Delete(GuestReview guestReview)
        {
            _guestReviewService.Delete(guestReview);
        }

        public void Update(GuestReview guestReview)
        {
            _guestReviewService.Update(guestReview);
        }

        public List<GuestReview> GetByUserId(int guest)
        {
            return _guestReviewService.GetByUserId(guest);
        }

        public string GetCleannessAverageByUserId(int guest)
        {
            return _guestReviewService.GetCleannessAverageByUserId(guest);
        }

        public string GetBehaviorAverageByUserId(int guest)
        {
            return _guestReviewService.GetBehaviorAverageByUserId(guest);
        }
    }
}
