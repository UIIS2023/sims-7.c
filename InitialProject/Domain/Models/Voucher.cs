using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public bool Used { get; set; }
        public int Duration { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Voucher()
        {


        }

        public Voucher(int id, string name, User user, bool used,int duration, DateTime date)
        {

            Id = id;
            Name = name;
            User = user;
            Used = used;
            Duration = duration;    
            ExpirationDate = date;


        }

        public string[] ToCSV()
        {


            string[] csvValues =
            {
               Id.ToString(),
               Name,
               User.Id.ToString(),
               Used.ToString(),
               Duration.ToString(),
               ExpirationDate.ToString()


            };

            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            User = new User() { Id = Convert.ToInt32(values[2]) };
            Used = bool.Parse(values[3]);
            Duration = Convert.ToInt32(values[4]);
            ExpirationDate = Convert.ToDateTime(values[5]); 

        }
    }
}
