using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        Accommodation Get(int id);
        Accommodation Save(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
    }
}
