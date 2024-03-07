using InitialProject.Serializer;
using System;
using InitialProject.Util;

namespace InitialProject.Domain.Models
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public GuestReview GuestReview { get; set; }
        public AccommodationOwnerReview AccommodationReview { get; set; }
        public bool IsCancelled { get; set; }

        public AccommodationReservation()
        {

        }

        public AccommodationReservation(int id, Accommodation accommodation, User guest, DateTime start, DateTime end, GuestReview guestReview, AccommodationOwnerReview accommodationReview, bool isCancelled)
        {
            Id = id;
            Accommodation = accommodation;
            Guest = guest;
            Start = start;
            End = end;
            GuestReview = guestReview;
            AccommodationReview = accommodationReview;
            IsCancelled = isCancelled;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Guest.Id.ToString(),
                DateHelper.DateToString(Start),
                DateHelper.DateToString(End),
                GuestReview.Id.ToString(),
                AccommodationReview.Id.ToString(),
                IsCancelled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Guest = new User() { Id = Convert.ToInt32(values[2]) };
            Start = DateHelper.StringToDate(values[3]);
            End = DateHelper.StringToDate(values[4]);
            GuestReview = new GuestReview() { Id = Convert.ToInt32(values[5]) };
            AccommodationReview = new AccommodationOwnerReview() { Id = Convert.ToInt32(values[6]) };
            IsCancelled = Convert.ToBoolean(values[7]);
        }
    }
}
