using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationService _accommodationService;

        public AccommodationController()
        {
            _accommodationService = new AccommodationService();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationService.GetAll();
        }


        public Accommodation Get(int id)
        {
            return _accommodationService.Get(id);
        }

        public Accommodation Save(Accommodation accommodation)
        {

            return _accommodationService.Save(accommodation);
        }

        public Accommodation SaveImages(Accommodation accommodation)
        {
            return _accommodationService.SaveImages(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {

            _accommodationService.Delete(accommodation);

        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationService.Update(accommodation);
        }

        public int NextId()
        {

            return _accommodationService.NextId();

        }

        public List<Accommodation> GetByOwner(int id)
        {

            return _accommodationService.GetByOwner(id);
        }

        public List<AccommodationDto> GetDtos(List<Accommodation> accommodations)
        {
            return _accommodationService.GetDtos(accommodations);
        }

    }
}
