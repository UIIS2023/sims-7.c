using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Enumerations;
using InitialProject.Properties;
using InitialProject.Serializer;

namespace InitialProject.Domain.Models
{
    public class RenovationRecommendation : ISerializable
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string Description { get; set; }
        public int UrgencyLevel { get; set; }

        public RenovationRecommendation() { }

        public RenovationRecommendation(int id, int resourceId, string description, int urgencyLevel)
        {
            Id = id;
            ResourceId = resourceId;
            Description = description;
            UrgencyLevel = urgencyLevel;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ResourceId.ToString(),
                Description,
                UrgencyLevel.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ResourceId = Convert.ToInt32(values[1]);
            Description = values[2];
            UrgencyLevel = Convert.ToInt32(values[3]);
        }
    }
}
