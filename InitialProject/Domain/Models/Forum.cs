using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public User Author { get; set; }
        public bool IsOpen { get; set; }
        public List<Comment> Comments { get; set; }

        public Forum()
        {
            Comments = new List<Comment>();
        }

        public Forum(int id, Location location, User author, bool isOpen)
        {
            Id = id;
            Location = location;
            Author = author;
            IsOpen = isOpen;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Location.Id.ToString(),
                Author.Username.ToString(),
                IsOpen.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            Author = new User() { Username = Convert.ToString(values[2]) };
            IsOpen = Convert.ToBoolean(values[3]);
        }
    }
}
