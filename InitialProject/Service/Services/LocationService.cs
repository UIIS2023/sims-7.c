using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class LocationService
    {
        private LocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = LocationRepository.GetInstance();
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location Get(int id)
        {
            return _locationRepository.Get(id);
        }
        public Location Save(Location location)
        {
            return _locationRepository.Save(location);
        }
        public Location Update(Location location)
        {
            return _locationRepository.Update(location);
        }
        public void Delete(Location location)
        {
            _locationRepository.Delete(location);
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            foreach (Location location in _locationRepository.GetAll())
            {
                countries.Add(location.Country);
            }
            return countries.Distinct().ToList(); ;
        }

        public List<string> GetCitiesByCountry(string country)
        {
            List<string> cities = new List<string>();
            foreach (Location location in _locationRepository.GetAll())
            {
                if (location.Country == country)
                {
                    cities.Add(location.City);
                }
            }
            return cities;
        }

        public Location FindLocationByCountryCity(string country, string city)
        {
            foreach (Location location in _locationRepository.GetAll())
            {
                if (location.Country == country && location.City == city)
                {
                    return location;
                }
            }
            return null;
        }



    }
}
