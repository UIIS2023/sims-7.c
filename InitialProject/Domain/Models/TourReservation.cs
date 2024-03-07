using InitialProject.Enumerations;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }

        public int NumberOfPeople { get; set; }

        public TourEvent TourEvent { get; set; }

        public User Guest { get; set; }

        public TourPoint TourPointWhenGuestCame { get; set; }

        public Voucher Voucher { get; set; }  
        
        public bool IsRewarded { get; set; }

        public TourReservation()
        {

        }


        public TourReservation(int id, int numberOfPeople,TourEvent tourEvent, User guest, TourPoint tourPointWhenGuestCame, Voucher voucher, bool isRewarded)
        {
            Id = id;
            NumberOfPeople = numberOfPeople;
            TourEvent = tourEvent;
            Guest = guest;
            TourPointWhenGuestCame = tourPointWhenGuestCame;
            Voucher = voucher;
            IsRewarded = isRewarded;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                NumberOfPeople.ToString(),
                TourEvent.Id.ToString(),
                Guest.Id.ToString(),
                TourPointWhenGuestCame.Id.ToString(),
                Voucher.Id.ToString(),
                IsRewarded.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfPeople = Convert.ToInt32(values[1]);
            TourEvent = new TourEvent() { Id = Convert.ToInt32(values[2]) };
            Guest = new User() { Id = Convert.ToInt32(values[3]) };
            TourPointWhenGuestCame = new TourPoint() { Id= Convert.ToInt32(values[4]) };
            Voucher = new Voucher() { Id = Convert.ToInt32(values[5]) };
            IsRewarded = bool.Parse(values[6]);
        }
    }
}
