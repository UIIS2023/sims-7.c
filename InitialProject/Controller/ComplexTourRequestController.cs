using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class ComplexTourRequestController
    {
        private readonly ComplexTourRequestService _tourRequestService;

        public ComplexTourRequestController()
        {
            _tourRequestService = new ComplexTourRequestService();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }

        public ComplexTourRequest Get(int id)
        {
            return _tourRequestService.Get(id);
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return _tourRequestService.Save(complexTourRequest);
        }

        public void Delete(ComplexTourRequest complexTourRequest)
        {
            _tourRequestService.Delete(complexTourRequest);
        }

        public int NextId()
        {
            return _tourRequestService.NextId();
        }


        public ComplexTourRequest Update(ComplexTourRequest complexTourRequest)
        {
            return _tourRequestService.Update(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAllComplexTourRequestsForUser(int userId)
        {
            return _tourRequestService.GetAllComplexTourRequestsForUser(userId);
        }
    }
}
