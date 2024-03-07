using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest Get(int id);
        TourRequest Save(TourRequest tourRequest);
        void Delete(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);

        int NextId();
    }
}
