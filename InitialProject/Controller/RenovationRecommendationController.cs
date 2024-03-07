using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class RenovationRecommendationController
    {
        private readonly RenovationRecommendationService _renovationRecommendationService;

        public RenovationRecommendationController()
        {
            _renovationRecommendationService = new RenovationRecommendationService();
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _renovationRecommendationService.GetAll();
        }

        public RenovationRecommendation Get(int id)
        {
            return _renovationRecommendationService.Get(id);
        }
        public RenovationRecommendation Save(RenovationRecommendation renovationRecommendation)
        {
            return _renovationRecommendationService.Save(renovationRecommendation);
        }
        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            _renovationRecommendationService.Delete(renovationRecommendation);
        }
        public void Update(RenovationRecommendation renovationRecommendation)
        {
            _renovationRecommendationService.Update(renovationRecommendation);
        }
    }
}
