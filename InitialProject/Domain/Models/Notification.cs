using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public TourReservation TourReservation { get; set; }
        public bool IsDelivered { get; set; }

        public Notification() { }

        public Notification(int id, TourReservation tourReservation, bool isDelivered)
        {
            Id = id;
            TourReservation = tourReservation;
            IsDelivered = isDelivered;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourReservation.Id.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourReservation = new TourReservation() { Id = Convert.ToInt32(values[1]) };
            IsDelivered = Boolean.Parse(values[2]);
        }

    }
}
