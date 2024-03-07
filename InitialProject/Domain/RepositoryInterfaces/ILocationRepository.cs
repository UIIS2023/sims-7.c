using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        Location Get(int id);
        Location Save(Location location);
        void Delete(Location location);
        Location Update(Location location);
    }
}
