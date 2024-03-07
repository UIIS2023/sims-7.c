using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        public UserType Role { get; set; }
        public int ForumId { get; set; }
        public int ReportsNumber { get; set; }

        public Comment() { }

        public Comment(int id, string text, User author, UserType role, int forumId, int reportsNumber)
        {
            Id = id;
            Text = text;
            Author = author;
            Role = role;
            ForumId = forumId;
            ReportsNumber = reportsNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(),
                Text,
                Author.Username.ToString(),
                Role.ToString(),
                ForumId.ToString(),
                ReportsNumber.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Text = values[1];
            Author = new User() { Username = Convert.ToString(values[2]) };
            Role = (UserType)Enum.Parse(typeof(UserType), values[3]);
            ForumId = Convert.ToInt32(values[4]);
            ReportsNumber = Convert.ToInt32(values[5]);
        }
    }
}
