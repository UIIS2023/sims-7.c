using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestRepository
    {
        List<ComplexTourRequest> GetAll();
        ComplexTourRequest Get(int id);
        ComplexTourRequest Save(ComplexTourRequest tourRequest);
        void Delete(ComplexTourRequest tourRequest);
        ComplexTourRequest Update(ComplexTourRequest tourRequest);
        int NextId();
    }
}
