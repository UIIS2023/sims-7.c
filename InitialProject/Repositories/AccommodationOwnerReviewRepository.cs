using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class AccommodationOwnerReviewRepository : IAccommodationOwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationOwnerReviews.csv";

        //private static AccommodationOwnerReviewRepository instance = null;

        private readonly Serializer<AccommodationOwnerReview> _serializer;

        private ImageRepository _imageRepository;

        private RenovationRecommendationRepository _renovationRecommendationRepository;

        private List<AccommodationOwnerReview> _accommodationOwnerReviews;

        public AccommodationOwnerReviewRepository()
        {
            _serializer = new Serializer<AccommodationOwnerReview>();
            _imageRepository = ImageRepository.GetInstance();
            _renovationRecommendationRepository = new RenovationRecommendationRepository();
            _accommodationOwnerReviews = _serializer.FromCSV(FilePath);
        }

        /*
        public static AccommodationOwnerReviewRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new AccommodationOwnerReviewRepository();
            }
            return instance;
        }
        */

        public void BindAccommodationOwnerReviewWithAccommodationReservation()
        {
            foreach (AccommodationOwnerReview accommodationOwnerReview in _accommodationOwnerReviews)
            {
                int accommodationReservationId = accommodationOwnerReview.Reservation.Id;
                AccommodationReservation reservation = AccommodationReservationRepository.GetInstance().Get(accommodationReservationId);
                if (reservation != null)
                {
                    accommodationOwnerReview.Reservation = reservation;
                    reservation.AccommodationReview = accommodationOwnerReview;
                }
                else
                {
                    Console.WriteLine("Error in accommodationOwnerReviewAccommodationReservation binding");
                }
            }
        }

        public List<AccommodationOwnerReview> GetAll()
        {
            return _accommodationOwnerReviews;
        }
        public AccommodationOwnerReview Get(int id)
        {
            return _accommodationOwnerReviews.Find(aor => aor.Id == id);
        }
        public AccommodationOwnerReview Save(AccommodationOwnerReview accommodationOwnerReview)
        {
            accommodationOwnerReview.Id = NextId();
            _accommodationOwnerReviews.Add(accommodationOwnerReview);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }
        public int NextId()
        {
            if (_accommodationOwnerReviews.Count < 1)
            {
                return 1;
            }
            return _accommodationOwnerReviews.Max(aor => aor.Id) + 1;
        }
        public void Delete(AccommodationOwnerReview accommodationOwnerReview)
        {
            AccommodationOwnerReview founded = _accommodationOwnerReviews.Find(aor => aor.Id == accommodationOwnerReview.Id);
            _accommodationOwnerReviews.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
        }

        public AccommodationOwnerReview Update(AccommodationOwnerReview accommodationOwnerReview)
        {
            AccommodationOwnerReview current = _accommodationOwnerReviews.Find(aor => aor.Id == accommodationOwnerReview.Id);
            int index = _accommodationOwnerReviews.IndexOf(current);
            _accommodationOwnerReviews.Remove(current);
            _accommodationOwnerReviews.Insert(index, accommodationOwnerReview);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }

        public List<AccommodationOwnerReview> GetByReservation(int reservationId) //nema bas nekog smisla
        {
            return _accommodationOwnerReviews.FindAll(aor => aor.Reservation.Id == reservationId);
        }

        public AccommodationOwnerReview SaveReviewImagesRecommendation(AccommodationOwnerReview accommodationOwnerReview)
        {
            accommodationOwnerReview.Id = NextId();

            foreach (Image image in accommodationOwnerReview.Images)
            {
                image.ResourceId = accommodationOwnerReview.Id;
                _imageRepository.Save(image);
            }

            foreach (RenovationRecommendation renovationRecommendation in accommodationOwnerReview.RenovationRecommendations)
            {
                renovationRecommendation.ResourceId = accommodationOwnerReview.Id;
                _renovationRecommendationRepository.Save(renovationRecommendation);
            }

            _accommodationOwnerReviews.Add(accommodationOwnerReview);
            _serializer.ToCSV(FilePath, _accommodationOwnerReviews);
            return accommodationOwnerReview;
        }
    }
}
