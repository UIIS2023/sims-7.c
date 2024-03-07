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
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRenovations.csv";


        private readonly Serializer<AccommodationRenovation> _serializer;

        private List<AccommodationRenovation> _accommodationRenovations;

        public AccommodationRenovationRepository()
        {
            _serializer = new Serializer<AccommodationRenovation>();
            _accommodationRenovations = _serializer.FromCSV(FilePath);
        }

        public void BindAccommodationRenovationWithAccommodation()
        {
            foreach (AccommodationRenovation accommodationRenovation in _accommodationRenovations)
            {
                int accommodationId = accommodationRenovation.Accommodation.Id;
                Accommodation accommodation = AccommodationRepository.GetInstance().Get(accommodationId);
                if (accommodation != null)
                {
                    accommodationRenovation.Accommodation = accommodation;
                }
                else
                {
                    Console.WriteLine("Error in accommodationRenovation-accommodation binding");
                }
            }
        }
        public AccommodationRenovation Save(AccommodationRenovation accommodationRenovation)
        {
            accommodationRenovation.Id = NextId();
            _accommodationRenovations.Add(accommodationRenovation);
            _serializer.ToCSV(FilePath, _accommodationRenovations);
            return accommodationRenovation;
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovations;
        }
        public AccommodationRenovation Get(int id)
        {
            return _accommodationRenovations.Find(ar => ar.Id == id);
        }

        public int NextId()
        {
            if (_accommodationRenovations.Count < 1)
            {
                return 1;
            }
            return _accommodationRenovations.Max(ar => ar.Id) + 1;
        }
        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            AccommodationRenovation founded = _accommodationRenovations.Find(ar => ar.Id == accommodationRenovation.Id);
            _accommodationRenovations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationRenovations);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            AccommodationRenovation current = _accommodationRenovations.Find(ar => ar.Id == accommodationRenovation.Id);
            int index = _accommodationRenovations.IndexOf(current);
            _accommodationRenovations.Remove(current);
            _accommodationRenovations.Insert(index, accommodationRenovation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodationRenovations);
            return accommodationRenovation;
        }

        public List<AccommodationRenovation> GetByAccommodationId(int id)
        {
            List<AccommodationRenovation> renovations = GetAll();
            return renovations.Where(renovations => renovations.Accommodation.Id == id).ToList();
        }
    }
}
