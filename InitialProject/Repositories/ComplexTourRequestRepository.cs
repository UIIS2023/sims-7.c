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
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequest.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _tourRequests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public void BindComplexTourRequestUser()
        {
            foreach (ComplexTourRequest ComplexTourRequest in _tourRequests)
            {
                int userId = ComplexTourRequest.Guest.Id;
                User user = UserRepository.GetInstance().Get(userId);
                if (user != null)
                {
                    ComplexTourRequest.Guest = user;
                }
                else
                {
                    Console.WriteLine("Error in complexTourRequestUser binding");
                }
            }
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequests;
        }
        public ComplexTourRequest Get(int id)
        {
            return _tourRequests.Find(t => t.Id == id);
        }
        public ComplexTourRequest Save(ComplexTourRequest ComplexTourRequest)
        {
            ComplexTourRequest.Id = NextId();
            _tourRequests.Add(ComplexTourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return ComplexTourRequest;
        }
        public int NextId()
        {
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(a => a.Id) + 1;
        }
        public void Delete(ComplexTourRequest ComplexTourRequest)
        {
            ComplexTourRequest founded = _tourRequests.Find(t => t.Id == ComplexTourRequest.Id);
            _tourRequests.Remove(founded);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public ComplexTourRequest Update(ComplexTourRequest ComplexTourRequest)
        {
            ComplexTourRequest current = _tourRequests.Find(a => a.Id == ComplexTourRequest.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, ComplexTourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return ComplexTourRequest;
        }

        public List<ComplexTourRequest> GetByGuest(int guestId)
        {
            return _tourRequests.FindAll(i => i.Guest.Id == guestId);
        }
    }
}
