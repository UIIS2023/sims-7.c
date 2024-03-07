using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationOwnerReviewRepository
    {
        public List<AccommodationOwnerReview> GetAll();
        public AccommodationOwnerReview Get(int id);
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview);
        public void Delete(AccommodationOwnerReview accommodationOwnerReview);
        public AccommodationOwnerReview Update(AccommodationOwnerReview accommodationOwnerReview);
        public int NextId();
        public List<AccommodationOwnerReview> GetByReservation(int reservationId);
        public AccommodationOwnerReview SaveReviewImagesRecommendation(AccommodationOwnerReview accommodationOwnerReview);
    }
}
