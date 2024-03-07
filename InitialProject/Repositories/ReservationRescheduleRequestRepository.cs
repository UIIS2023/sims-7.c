using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class ReservationRescheduleRequestRepository : IReservationRescheduleRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationRescheduleRequests.csv";


        private readonly Serializer<ReservationRescheduleRequest> _serializer;

        private List<ReservationRescheduleRequest> _reservationRescheduleRequests;

        public ReservationRescheduleRequestRepository()
        {
            _serializer = new Serializer<ReservationRescheduleRequest>();
            _reservationRescheduleRequests = _serializer.FromCSV(FilePath);
        }

        public void BindReservationRescheduleRequestWithUser()
        {
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequests)
            {
                int guestId = reservationRescheduleRequest.Guest.Id;
                User guest = UserRepository.GetInstance().Get(guestId);
                if (guest != null)
                {
                    reservationRescheduleRequest.Guest = guest;
                }
                else
                {
                    Console.WriteLine("Error in reservationRescheduleRequestUser binding");
                }
            }
        }

        public void BindReservationRescheduleRequestWithAccommodationReservation()
        {
            foreach (ReservationRescheduleRequest reservationRescheduleRequest in _reservationRescheduleRequests)
            {
                int accommodationReservationId = reservationRescheduleRequest.Reservation.Id;
                AccommodationReservation accommodationReservation = AccommodationReservationRepository.GetInstance().Get(accommodationReservationId);
                if (accommodationReservation != null)
                {
                    reservationRescheduleRequest.Reservation = accommodationReservation;
                }
                else
                {
                    Console.WriteLine("Error in reservationRescheduleRequestAccommodationReservation binding");
                }
            }
        }

        public ReservationRescheduleRequest Save(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            reservationRescheduleRequest.Id = NextId();
            _reservationRescheduleRequests.Add(reservationRescheduleRequest);
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
            return reservationRescheduleRequest;
        }

        public List<ReservationRescheduleRequest> GetAll()
        {
            return _reservationRescheduleRequests;
        }
        public ReservationRescheduleRequest Get(int id)
        {
            return _reservationRescheduleRequests.Find(rrr => rrr.Id == id);
        }

        public int NextId()
        {
            if (_reservationRescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _reservationRescheduleRequests.Max(rrr => rrr.Id) + 1;
        }
        public void Delete(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            ReservationRescheduleRequest founded = _reservationRescheduleRequests.Find(rrr => rrr.Id == reservationRescheduleRequest.Id);
            _reservationRescheduleRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
        }

        public ReservationRescheduleRequest Update(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            ReservationRescheduleRequest current = _reservationRescheduleRequests.Find(rrr => rrr.Id == reservationRescheduleRequest.Id);
            int index = _reservationRescheduleRequests.IndexOf(current);
            _reservationRescheduleRequests.Remove(current);
            _reservationRescheduleRequests.Insert(index, reservationRescheduleRequest);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _reservationRescheduleRequests);
            return reservationRescheduleRequest;
        }
    }
}
