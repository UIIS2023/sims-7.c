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
    public class TourController
    {
        private readonly TourService _tourService;

        public TourController()
        {
            _tourService = new TourService();
        }

        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }


        public Tour Get(int id)
        {
            return _tourService.Get(id);
        }

        public Tour Save(Tour tour)
        {

            return _tourService.Save(tour);
        }

        public void Delete(Tour tour)
        {

            _tourService.Delete(tour);

        }

        public Tour Update(Tour tour)
        {
            return _tourService.Update(tour);
        }


        public List<Tour> GetByGuide(int id)
        {

            return _tourService.GetByGuide(id);
        }

        public List<Tour> SearchTours(string country, string city, string language, string numberOfPeople, string duration)
        {
            return _tourService.SearchTours(country, city, language, numberOfPeople, duration);
        }
        /*public Tour SaveCascadeImages(Tour tour)
        {
            return _tourService.SaveCascadeImages(tour);
        }

        public Tour SaveCascadeTourPoints(Tour tour)
        {
            return _tourService.SaveCascadeTourPoints(tour);
        }*/

        public Tour SaveImagesTourPoints(Tour tour)
        {

            return _tourService.SaveImagesTourPoints(tour);
        }

        public List<Tour> GetAllToursForGuide(int guideId) {
            return _tourService.GetAllToursForGuide(guideId);
        }
    }
}
