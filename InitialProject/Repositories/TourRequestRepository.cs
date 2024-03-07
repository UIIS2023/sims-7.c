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
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequest.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public void BindTourRequestUser()
        {
            foreach (TourRequest tourRequest in _tourRequests)
            {
                int userId = tourRequest.Guest.Id;
                User user = UserRepository.GetInstance().Get(userId);
                if (user != null)
                {
                    tourRequest.Guest = user;
                }
                else
                {
                    Console.WriteLine("Error in tourRequestUser binding");
                }
            }
        }

        public void BindTourRequestLocation()
        {
            foreach (TourRequest request in _tourRequests)
            {
                int locationId = request.Location.Id;
                Location location = LocationRepository.GetInstance().Get(locationId);
                if (location != null)
                {
                    request.Location = location;
                }
                else
                {
                    Console.WriteLine("Error in tourRequestLocation binding");
                }
            }
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequests;
        }
        public TourRequest Get(int id)
        {
            return _tourRequests.Find(t => t.Id == id);
        }
        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }
        public int NextId()
        {
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(a => a.Id) + 1;
        }
        public void Delete(TourRequest tourRequest)
        {
            TourRequest founded = _tourRequests.Find(t => t.Id == tourRequest.Id);
            _tourRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            TourRequest current = _tourRequests.Find(a => a.Id == tourRequest.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public List<TourRequest> GetByGuest(int guestId)
        {
            return _tourRequests.FindAll(i => i.Guest.Id == guestId);
        }
    }
}
