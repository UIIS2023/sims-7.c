using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class GuestReviewService
    {
        private GuestReviewRepository _guestReviewRepository;

        public GuestReviewService()
        {
            _guestReviewRepository = GuestReviewRepository.GetInstance();
        }

        public List<GuestReview> GetAll()
        {
            return _guestReviewRepository.GetAll();
        }

        public GuestReview Get(int id)
        {
            return _guestReviewRepository.Get(id);
        }
        public GuestReview Save(GuestReview guestReview)
        {
            return _guestReviewRepository.Save(guestReview);
        }
        public GuestReview Update(GuestReview guestReview)
        {
            return _guestReviewRepository.Update(guestReview);
        }
        public void Delete(GuestReview guestReview)
        {
            _guestReviewRepository.Delete(guestReview);
        }
        public List<GuestReview> GetByReservation(GuestReview guestReview)
        {
            return _guestReviewRepository.GetByReservation(guestReview.Reservation.Id);
        }

        public List<GuestReview> GetByUserId(int guest)
        {
            List<GuestReview> guestReviews = new List<GuestReview>();

            foreach (GuestReview guestReview in _guestReviewRepository.GetAll())
            {
                if (guestReview.Guest.Id == guest)
                {
                    if (guestReview.Reservation.AccommodationReview.Id != -1)
                    {
                        guestReviews.Add(guestReview);
                    }
                }
            }
            return guestReviews;
        }

        public string GetCleannessAverageByUserId(int guest)
        {
            double sum = 0;
            double numberOfGuestReviews = 0;
            double retVal;

            foreach (GuestReview guestReview in _guestReviewRepository.GetAll())
            {
                if (guestReview.Guest.Id == guest)
                {
                    numberOfGuestReviews++;
                    sum += guestReview.Cleanliness;
                }
            }

            retVal = sum / numberOfGuestReviews;

            return retVal.ToString();
        }

        public string GetBehaviorAverageByUserId(int guest)
        {
            double sum = 0;
            double numberOfGuestReviews = 0;
            double retVal;

            foreach (GuestReview guestReview in _guestReviewRepository.GetAll())
            {
                if (guestReview.Guest.Id == guest)
                {
                    numberOfGuestReviews++;
                    sum += guestReview.Behaviour;
                }
            }

            retVal = sum / numberOfGuestReviews;

            return retVal.ToString();
        }
    }
}
