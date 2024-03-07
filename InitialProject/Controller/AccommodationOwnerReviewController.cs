using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class AccommodationOwnerReviewController
    {
        private readonly AccommodationOwnerReviewService _accommodationOwnerReviewService;

        public AccommodationOwnerReviewController()
        {
            _accommodationOwnerReviewService = new AccommodationOwnerReviewService();
        }

        public List<AccommodationOwnerReview> GetAll()
        {
            return _accommodationOwnerReviewService.GetAll();
        }

        public AccommodationOwnerReview Get(int id)
        {
            return _accommodationOwnerReviewService.Get(id);
        }
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview)
        {
            return _accommodationOwnerReviewService.Save(accommodationOwnerReview);
        }
        public void Delete(AccommodationOwnerReview accommodationOwnerReview)
        {
            _accommodationOwnerReviewService.Delete(accommodationOwnerReview);
        }
        public void Update(AccommodationOwnerReview accommodationOwnerReview)
        {
            _accommodationOwnerReviewService.Update(accommodationOwnerReview);
        }

        public AccommodationOwnerReview SaveReviewImagesRecommendation(AccommodationOwnerReview accommodationOwnerReview)
        {
            return _accommodationOwnerReviewService.SaveReviewImagesRecommendation(accommodationOwnerReview);
        }
        public List<AccommodationOwnerReview> GetAllValidReviews(Accommodation accommodation)
        {
            return _accommodationOwnerReviewService.GetAllValidReviews(accommodation);
        }

        public bool IsSuperOwner(int ownerId)
        {
            return _accommodationOwnerReviewService.IsSuperOwner(ownerId);
        }


    }
}
