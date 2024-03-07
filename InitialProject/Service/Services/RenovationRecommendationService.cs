using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class RenovationRecommendationService
    {
        private RenovationRecommendationRepository _renovationRecommendationRepository;

        public List<RenovationRecommendation> GetAll()
        {
            return _renovationRecommendationRepository.GetAll();
        }

        public RenovationRecommendation Get(int id)
        {
            return _renovationRecommendationRepository.Get(id);
        }


        public RenovationRecommendation Save(RenovationRecommendation renovationRecommendation)
        {
            return _renovationRecommendationRepository.Save(renovationRecommendation);
        }
        public RenovationRecommendation Update(RenovationRecommendation renovationRecommendation)
        {
            return _renovationRecommendationRepository.Update(renovationRecommendation);
        }
        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            _renovationRecommendationRepository.Delete(renovationRecommendation);
        }
    }
}
