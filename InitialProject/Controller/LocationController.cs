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
    public class LocationController
    {
        private readonly LocationService _locationService;

        public LocationController()
        {
            _locationService = new LocationService();
        }

        public List<Location> GetAll()
        {
            return _locationService.GetAll();
        }

        public Location Get(int id)
        {
            return _locationService.Get(id);
        }
        public Location Save(Location location)
        {
            return _locationService.Save(location);
        }
        public void Delete(Location location)
        {
            _locationService.Delete(location);
        }
        public void Update(Location location)
        {
            _locationService.Update(location);
        }

        public List<String> GetAllCountries()
        {
           return  _locationService.GetAllCountries();
        }
        public List<string> GetCitiesByCountry(string country)
        {
            return _locationService.GetCitiesByCountry(country);
        }

        public Location FindLocationByCountryCity(string country, string city)
        {
            return _locationService.FindLocationByCountryCity(country, city);
        }



    }
}
