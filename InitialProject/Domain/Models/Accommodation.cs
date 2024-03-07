using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace InitialProject.Domain.Models
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinDaysForReservation { get; set; }
        public int CancelationPeriod { get; set; }
        public List<Image> Images { get; set; }
        public User Owner { get; set; }


        public Accommodation()
        {
            Images = new List<Image>();
        }

        public Accommodation(int id, string name, Location location, AccommodationType type, int maxGuests, int minDaysForReservation, int cancelationPeriod, User owner)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinDaysForReservation = minDaysForReservation;
            CancelationPeriod = cancelationPeriod;
            Images = new List<Image>();
            Owner = owner;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Name,
                Location.Id.ToString(),
                Type.ToString(),
                MaxGuests.ToString(),
                MinDaysForReservation.ToString(),
                CancelationPeriod.ToString(),
                Owner.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinDaysForReservation = Convert.ToInt32(values[5]);
            CancelationPeriod = Convert.ToInt32(values[6]);
            Owner = new User() { Id = Convert.ToInt32(values[7]) };
        }

    }
}
