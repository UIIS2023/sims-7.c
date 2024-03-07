using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationRenovationRepository
    {
        List<AccommodationRenovation> GetAll();
        AccommodationRenovation Get(int id);
        AccommodationRenovation Save(AccommodationRenovation accommodationRenovation);
        void Delete(AccommodationRenovation accommodationRenovation);
        AccommodationRenovation Update(AccommodationRenovation accommodationRenovation);
        List<AccommodationRenovation> GetByAccommodationId(int id);
    }
}
