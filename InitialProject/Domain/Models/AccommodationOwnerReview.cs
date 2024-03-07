using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class AccommodationOwnerReview : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int Cleanliness { get; set; }
        public int Correctness { get; set; }
        public string Comment { get; set; }
        public List<Image> Images { get; set; }
        public List<RenovationRecommendation> RenovationRecommendations { get; set; }

        public AccommodationOwnerReview()
        {
            Images = new List<Image>();
            RenovationRecommendations = new List<RenovationRecommendation>();
        }

        public AccommodationOwnerReview(int id, AccommodationReservation reservation, int cleanliness, int correctness, string comment)
        {
            Id = id;
            Reservation = reservation;
            Cleanliness = cleanliness;
            Correctness = correctness;
            Comment = comment;
            Images = new List<Image>();
            RenovationRecommendations = new List<RenovationRecommendation>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Reservation.Id.ToString(),
                Cleanliness.ToString(),
                Correctness.ToString(),
                Comment
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            Correctness = Convert.ToInt32(values[3]);
            Comment = values[4];
        }
    }
}
