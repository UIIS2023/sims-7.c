using InitialProject.Serializer;
using InitialProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class TourRequestAcceptedNotification : ISerializable
    {
        public int Id { get; set; }
        public TourRequest TourRequest { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDelivered { get; set; }
        public TourRequestAcceptedNotification() { }

        public TourRequestAcceptedNotification(int id, TourRequest tourRequest, DateTime startTime, bool isDelivered)
        {
            Id = id;
            TourRequest = tourRequest;
            StartTime = startTime;
            IsDelivered = isDelivered;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                 Id.ToString(),
                 TourRequest.Id.ToString(),
                 DateHelper.DateToString(StartTime),
                 IsDelivered.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourRequest = new TourRequest() { Id = Convert.ToInt32(values[1]) };
            StartTime = DateHelper.StringToDate(values[2]);
            IsDelivered = bool.Parse(values[3]);
        }
    }
}
