using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.IO.Packaging;

namespace InitialProject.Domain.Models
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public int Age { get; set; }       

        public User() { }

        public User(int id, string firstName, string lastName, string username, string password, UserType type, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Type = type;
            Age = age;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), FirstName, LastName, Username, Password, Type.ToString(), Age.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FirstName = values[1];
            LastName = values[2];
            Username = values[3];
            Password = values[4];
            Type = (UserType)Enum.Parse(typeof(UserType), values[5]);
            Age = Convert.ToInt32(values[6]);
        }
    }
}
