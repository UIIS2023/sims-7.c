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
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservation.csv";

        private static TourReservationRepository instance = null;

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;

        private TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public static TourReservationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourReservationRepository();
            }
            return instance;
        }

        public void BindTourReservationUser()
        {
            foreach (TourReservation tourReservation in _tourReservations)
            {
                int userId = tourReservation.Guest.Id;
                User user = UserRepository.GetInstance().Get(userId);
                if (user != null)
                {
                    tourReservation.Guest = user;
                }
                else
                {
                    Console.WriteLine("Error in tourReservationUser binding");
                }
            }
        }



        public void BindTourReservationTourEvent()
        {
            foreach (TourReservation tourReservation in _tourReservations)
            {
                int tourEventId = tourReservation.TourEvent.Id;
                TourEvent tourEvent = TourEventRepository.GetInstance().Get(tourEventId);
                if (tourEvent != null)
                {
                    tourReservation.TourEvent = tourEvent;
                }
                else
                {
                    Console.WriteLine("Error in tourReservationTourEvent binding");
                }
            }
        }

        public void BindTourReservationVoucher()
        {
            foreach (TourReservation tourReservation in _tourReservations)
            {
                int voucherId = tourReservation.Voucher.Id;
                Voucher voucher = Injector.Injector.CreateInstance<IVoucherRepository>().Get(voucherId);
                if (voucher != null)
                {
                    tourReservation.Voucher = voucher;
                }
                else
                {
                    Console.WriteLine("Error in tourReservationVoucher binding");
                }
            }
        }



        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }
        public TourReservation Get(int id)
        {
            return _tourReservations.Find(t => t.Id == id);
        }
        public TourReservation Save(TourReservation tourreservation)
        {
            tourreservation.Id = NextId();
            _tourReservations.Add(tourreservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourreservation;
        }
        public int NextId()
        {
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(a => a.Id) + 1;
        }
        public void Delete(TourReservation tourreservation)
        {
            TourReservation founded = _tourReservations.Find(t => t.Id == tourreservation.Id);
            _tourReservations.Remove(founded);
            _serializer.ToCSV(FilePath, _tourReservations);
        }

        public TourReservation Update(TourReservation tourreservation)
        {
            TourReservation current = _tourReservations.Find(a => a.Id == tourreservation.Id);
            int index = _tourReservations.IndexOf(current);
            _tourReservations.Remove(current);
            _tourReservations.Insert(index, tourreservation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourreservation;
        }

        public List<TourReservation> GetByGuest(int guestId)
        {
            return _tourReservations.FindAll(i => i.Guest.Id == guestId);
        }

        public void BindTourReservationTourPoint()
        {
            foreach (TourReservation tourReservation in _tourReservations)
            {
                int tourPointId = tourReservation.TourPointWhenGuestCame.Id;
                TourPoint tourPoint = TourPointRepository.GetInstance().Get(tourPointId);
                if (tourPoint != null)
                {
                    tourReservation.TourPointWhenGuestCame = tourPoint;

                }
                else
                {
                    Console.WriteLine("Error in tourReservationnTourPoint binding");
                }
            }
        }
    }
}
