using InitialProject.Serializer;
using InitialProject.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public bool IsCancelled { get; set; }

        public AccommodationRenovation() { }

        public AccommodationRenovation(int id, Accommodation accommodation, DateTime start, DateTime end, string description, bool isCancelled)
        {
            Id = id;
            Accommodation = accommodation;
            Start = start;
            End = end;
            Description = description;
            IsCancelled = isCancelled;  
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                DateHelper.DateToString(Start),
                DateHelper.DateToString(End),
                Description,
                IsCancelled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[1]) };
            Start = DateHelper.StringToDate(values[2]);
            End = DateHelper.StringToDate(values[3]);
            Description = values[4];
            IsCancelled = bool.Parse(values[5]);
        }
    }
}
