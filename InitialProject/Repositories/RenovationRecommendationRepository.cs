using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Controller;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Enumerations;
using InitialProject.Serializer;

namespace InitialProject.Repositories
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovationRecommendations.csv";

        private readonly Serializer<RenovationRecommendation> _serializer;

        private List<RenovationRecommendation> _renovationRecommendations;


        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<RenovationRecommendation>();
            _renovationRecommendations = _serializer.FromCSV(FilePath);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _renovationRecommendations;
        }
        public RenovationRecommendation Get(int id)
        {
            return _renovationRecommendations.Find(rr => rr.Id == id);
        }
        public RenovationRecommendation Save(RenovationRecommendation renovationRecommendation)
        {
            renovationRecommendation.Id = NextId();
            _renovationRecommendations.Add(renovationRecommendation);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
            return renovationRecommendation;
        }
        public int NextId()
        {
            if (_renovationRecommendations.Count < 1)
            {
                return 1;
            }
            return _renovationRecommendations.Max(rr => rr.Id) + 1;
        }
        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            RenovationRecommendation founded = _renovationRecommendations.Find(rr => rr.Id == renovationRecommendation.Id);
            _renovationRecommendations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
        }

        public RenovationRecommendation Update(RenovationRecommendation renovationRecommendation)
        {
            RenovationRecommendation current = _renovationRecommendations.Find(rr => rr.Id == renovationRecommendation.Id);
            int index = _renovationRecommendations.IndexOf(current);
            _renovationRecommendations.Remove(current);
            _renovationRecommendations.Insert(index, renovationRecommendation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _renovationRecommendations);
            return renovationRecommendation;
        }

        
        
    }
}
