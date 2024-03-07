using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class NewForumNotification: ISerializable
    {
        public int Id { get; set; }
        public Forum Forum { get; set; }
        public bool IsDelivered { get; set; }

        public NewForumNotification() { }
        public NewForumNotification(int id, Forum forum, bool isDelivered)
        {
            Id = id;
            Forum = forum;
            IsDelivered = isDelivered;
        }

        public string[] ToCSV()
        {

            string[] csvValues =
            {
                Id.ToString(),
                Forum.Id.ToString(),
                IsDelivered.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Forum = new Forum() { Id = Convert.ToInt32(values[1]) };
            IsDelivered = Boolean.Parse(values[2]);
        }
    }
}
