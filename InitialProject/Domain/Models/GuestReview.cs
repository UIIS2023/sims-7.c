using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class GuestReview : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public User Guest { get; set; }
        public int Cleanliness { get; set; }
        public int Behaviour { get; set; }
        public string Comment { get; set; }

        public GuestReview() { }
        public GuestReview(int id, AccommodationReservation reservation, User guest, int cleanliness, int behaviour, string comment)
        {
            Id = id;
            Reservation = reservation;
            Guest = guest;
            Cleanliness = cleanliness;
            Behaviour = behaviour;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Reservation.Id.ToString(),
                Cleanliness.ToString(),
                Behaviour.ToString(),
                Comment,
                Guest.Id.ToString(),
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            Behaviour = Convert.ToInt32(values[3]);
            Comment = values[4];
            Guest = new User() { Id = Convert.ToInt32(values[5]) };
        }
    }
}
