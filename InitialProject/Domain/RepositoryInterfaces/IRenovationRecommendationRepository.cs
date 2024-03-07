using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Get(int id);
        RenovationRecommendation Save(RenovationRecommendation renovationRecommendation);
        void Delete(RenovationRecommendation renovationRecommendation);
        RenovationRecommendation Update(RenovationRecommendation renovationRecommendation);
    }
}
