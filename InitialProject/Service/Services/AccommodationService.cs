using InitialProject.Domain.Dto;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class AccommodationService
    {
        private AccommodationRepository _accommodationRepository;
        private IAccommodationRenovationRepository _accommodationRenovationRepository;

        public AccommodationService()
        {
            _accommodationRepository = AccommodationRepository.GetInstance();
            _accommodationRenovationRepository = Injector.Injector.CreateInstance<IAccommodationRenovationRepository>();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public Accommodation Get(int id)
        {
            return _accommodationRepository.Get(id);
        }

        public Accommodation Save(Accommodation accommodation)
        {

            return _accommodationRepository.Save(accommodation);
        }

        public Accommodation SaveImages(Accommodation accommodation)
        {

            return _accommodationRepository.SaveImages(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {

            _accommodationRepository.Delete(accommodation);

        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationRepository.Update(accommodation);
        }

        public int NextId()
        {

            return _accommodationRepository.NextId();

        }

        public List<Accommodation> GetByOwner(int id)
        {

            return _accommodationRepository.GetByOwner(id);
        }

        private bool IsRecentlyRenovated(int accommodationId)
        {
            List<AccommodationRenovation> renovations = _accommodationRenovationRepository.GetByAccommodationId(accommodationId);
            foreach(AccommodationRenovation renovation in renovations)
            {
                if(renovation.End <= DateTime.Now && DateTime.Now <= renovation.End.AddDays(365) && renovation.IsCancelled == false)
                {
                    return true;
                }
            }

            return false;
        }

        public List<AccommodationDto> GetDtos(List<Accommodation> accommodations)
        {
            List<AccommodationDto> accommodationDtos = new List<AccommodationDto>();
            foreach(Accommodation accommodation in accommodations)
            {
                AccommodationDto accommodationDto = new AccommodationDto(accommodation);
                accommodationDto.IsRecentlyRenovated = IsRecentlyRenovated(accommodation.Id);
                accommodationDtos.Add(accommodationDto);
            }

            return accommodationDtos;
        }
    }
}
