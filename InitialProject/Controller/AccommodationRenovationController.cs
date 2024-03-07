using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class AccommodationRenovationController
    {
        private readonly AccommodationRenovationService _accommodationRenovationService;

        public AccommodationRenovationController()
        {
            _accommodationRenovationService = new AccommodationRenovationService();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovationService.GetAll();
        }


        public AccommodationRenovation Get(int id)
        {
            return _accommodationRenovationService.Get(id);
        }

        public AccommodationRenovation Save(AccommodationRenovation accommodationRenovation)
        {

            return _accommodationRenovationService.Save(accommodationRenovation);
        }

        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            _accommodationRenovationService.Delete(accommodationRenovation);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            return _accommodationRenovationService.Update(accommodationRenovation);
        }

        public List<AvailableTermsDto> FindAllAvailableTerms(Accommodation accommodation, DateTime Start, DateTime End, int Duration)
        {
            return _accommodationRenovationService.FindAllAvailableTerms(accommodation, Start, End, Duration);
        }

        public List<AccommodationRenovation> GetAllValidRenovations(Accommodation accommodation)
        {
            return _accommodationRenovationService.GetAllValidRenovations(accommodation);
        }

        public void CancelRenovation(AccommodationRenovation renovation)
        {
            _accommodationRenovationService.CancelRenovation(renovation);
        }

       
    }
}
