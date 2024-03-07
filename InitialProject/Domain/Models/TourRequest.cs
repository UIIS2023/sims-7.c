using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;
using InitialProject.Enumerations;
using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public User Guest { get; set; }
        public RequestStatusType Status { get; set; }

        public int ComplexTourRequestId { get; set; }

        public TourRequest() { }

        public TourRequest(int id, Location location, string language, int maxGuests, string description, DateTime start, DateTime end, User guest, RequestStatusType status, int complexTourRequestId)
        {

            Id = id;
            Location = location;
            Language = language;
            MaxGuests = maxGuests;
            Description = description;
            Start = start;
            End = end;
            Guest = guest;
            Status = status;
            ComplexTourRequestId = complexTourRequestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Id.ToString(),
               Location.Id.ToString(),
               Language.ToString(),
               MaxGuests.ToString(),
               Description,   
               Start.ToString(),
               End.ToString(),
               Guest.Id.ToString(),
               Status.ToString(),
               ComplexTourRequestId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            Language = values[2];
            MaxGuests = Convert.ToInt32(values[3]);
            Description = values[4];
            Start = Convert.ToDateTime(values[5]);
            End = Convert.ToDateTime(values[6]);
            Guest = new User() { Id = Convert.ToInt32(values[7]) };
            Status = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), values[8]);
            ComplexTourRequestId = int.Parse(values[9]);
        }
    }
}
