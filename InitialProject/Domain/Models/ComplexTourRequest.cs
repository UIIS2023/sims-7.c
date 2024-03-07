using InitialProject.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public List<TourRequest> SimpleTourRequests { get; set; }
        public User Guest { get; set; }
        public RequestStatusType Status { get; set; }

        public ComplexTourRequest()
        {
            SimpleTourRequests = new List<TourRequest>();
        }

        public ComplexTourRequest(int id, List<TourRequest> simpleTourRequests, User guest, RequestStatusType status)
        {
            Id = id;
            SimpleTourRequests = simpleTourRequests;
            Guest = guest;
            Status = status;
        }

        public string[] ToCSV()
        {
            StringBuilder requestList = new StringBuilder();
            foreach (TourRequest tourRequest in SimpleTourRequests)
            {
                requestList.Append(tourRequest.Id.ToString());
                requestList.Append(",");
            }
            if (requestList.Length > 0)
            {
                requestList.Remove(requestList.Length - 1, 1);
            }
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Status.ToString(),
                requestList.ToString(),

            };
            return csvValues;
        }




        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Guest = new User() { Id = Convert.ToInt32(values[1]) };
            Status = (RequestStatusType)Enum.Parse(typeof(RequestStatusType), values[2]);
            string[] tourRequestIds = values[3].Split(",");
            SimpleTourRequests = new List<TourRequest>();

            foreach (string tourRequestId in tourRequestIds)
            {
                int requestId = int.Parse(tourRequestId.Trim());
                TourRequest tourRequest = new TourRequest { Id = requestId };
                SimpleTourRequests.Add(tourRequest);
            }

        }

    }
}
