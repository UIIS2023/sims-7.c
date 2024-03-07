using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class GuideReviewController
    {
        private readonly GuideReviewService _guideReviewService;

        public GuideReviewController()
        {
            _guideReviewService = new GuideReviewService();
        }

        public List<GuideReview> GetAll()
        {
            return _guideReviewService.GetAll();
        }

        public GuideReview Get(int id)
        {
            return _guideReviewService.Get(id);
        }

        public GuideReview Save(GuideReview guideReview)
        {
            return _guideReviewService.Save(guideReview);
        }

        public void Delete(GuideReview guideReview)
        {
            _guideReviewService.Delete(guideReview);
        }

        public void Update(GuideReview guideReview)
        {
            _guideReviewService.Update(guideReview);
        }
        
        public List<GuideReview> GetAllGuideReviews(int guideId, int guestId)
        {
            return _guideReviewService.GetAllGuideReviews(guideId,guestId);
        }


        public List<GuideReview> GetReviewsForUser(int tourReservation, int userId)
        {
            return _guideReviewService.GetReviewsForUser(tourReservation, userId);
        }
    }
}
