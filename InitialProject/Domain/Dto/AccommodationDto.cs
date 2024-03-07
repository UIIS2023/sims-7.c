using InitialProject.Domain.Models;
using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Dto
{
    public class AccommodationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinDaysForReservation { get; set; }
        public int CancelationPeriod { get; set; }
        public bool IsRecentlyRenovated { get; set; }
        public AccommodationDto(Accommodation accommodation)
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            Country = accommodation.Location.Country;
            City = accommodation.Location.City;
            Type = accommodation.Type.ToString();
            MaxGuests = accommodation.MaxGuests;
            MinDaysForReservation = accommodation.MinDaysForReservation;
            CancelationPeriod = accommodation.CancelationPeriod;
        }

        public AccommodationDto() { }
    }
}
