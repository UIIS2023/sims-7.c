using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class NewTourNotification : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Tour Tour { get; set; }
        public bool IsDelivered { get; set; }

        public NewTourNotification() { }
        public NewTourNotification(int id,User guest,Tour tour,bool isDelivered)
        {
            Id = id;
            Guest = guest;  
            Tour = tour;
            IsDelivered = isDelivered;
        }


        public string[] ToCSV()
        {

            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Tour.Id.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = new User() { Id = Convert.ToInt32(values[1]) };
            Tour = new Tour() { Id = Convert.ToInt32(values[2]) };
            IsDelivered = Boolean.Parse(values[3]); 
        }
    }
}
