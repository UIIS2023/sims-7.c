using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class GuideReview : ISerializable
    {
        public int Id { get; set; }
        public TourReservation Reservation { get; set; }
        public int Knowledge { get; set; }
        public int Language { get; set; }
        public int Interestingness { get; set; }
        public string Comment { get; set; }
        
        public List<string> Images { get; set; }

        public bool Validity { get; set; }

        public GuideReview()
        {
            Images = new List<string>();
        }
        public GuideReview(int id, TourReservation reservation, int knowledge, int language, int interestingness, string comment, List<string> images, bool validity)
        {
            Id = id;
            Reservation = reservation;
            Knowledge = knowledge;
            Language = language;
            Interestingness = interestingness;
            Comment = comment;
            Images = images;
            Validity = validity;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                Knowledge.ToString(),
                Language.ToString(),
                Interestingness.ToString(),
                Comment,
                string.Join(",", Images),
                Validity.ToString(),
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation = new TourReservation() { Id = Convert.ToInt32(values[1]) };
            Knowledge = Convert.ToInt32(values[2]);
            Language = Convert.ToInt32(values[3]);
            Interestingness = Convert.ToInt32(values[4]);
            Comment = values[5];
            Images = values[6].Split(',').ToList();
            Validity = bool.Parse(values[7]);
        }
    }
}
