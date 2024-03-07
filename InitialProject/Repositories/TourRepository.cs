using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Repositories
{
    public class TourRepository
    {

        private const string FilePath = "../../../Resources/Data/tours.csv";

        private static TourRepository instance = null;

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        private ImageRepository _imageRepository;
        private TourPointRepository _tourPointRepository;

        private TourRepository()
        {

            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
            _imageRepository = ImageRepository.GetInstance();
            _tourPointRepository = TourPointRepository.GetInstance();
        }

        public static TourRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourRepository();
            }
            return instance;
        }

        public void BindTourGuide()
        {
            foreach (Tour tour in _tours)
            {
                int guideId = tour.Guide.Id;
                User guide = UserRepository.GetInstance().Get(guideId);
                if (guide != null)
                {
                    tour.Guide = guide;
                }
                else
                {
                    Console.WriteLine("Error in tourGuide binding");
                }
            }
        }

        public void BindTourLocation()
        {
            foreach (Tour tour in _tours)
            {
                int locationId = tour.Location.Id;
                Location location = LocationRepository.GetInstance().Get(locationId);
                if (location != null)
                {
                    tour.Location = location;
                }
                else
                {
                    Console.WriteLine("Error in tourLocation binding");
                }
            }
        }
        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;

        }

        public int NextId()
        {
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }

        public void Delete(Tour tour)
        {
            Tour founded = _tours.Find(t => t.Id == tour.Id);
            _tours.Remove(founded);
            _serializer.ToCSV(FilePath, _tours);
        }

        public Tour Update(Tour tour)
        {
            Tour current = _tours.Find(t => t.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public List<Tour> GetAll()
        {

            return _tours;

        }

        public Tour Get(int id)
        {

            return _tours.Find(x => x.Id == id);

        }

        public List<Tour> GetByGuide(int guideId)
        {
            return _tours.FindAll(t => t.Guide.Id == guideId);
        }

        /* public Tour SaveCascadeImages(Tour tour)
         {
             tour.Id = NextId();

             foreach (Image image in tour.Images)
             {
                 image.ResourceId = tour.Id;
                 _imageRepository.Save(image);
             }

             _tours.Add(tour);
             _serializer.ToCSV(FilePath, _tours);
             return tour;
         }
         public Tour SaveCascadeTourPoints(Tour tour)
         {
             tour.Id = NextId();

             foreach (TourPoint tourPoint in tour.TourPoints)
             {
                 tourPoint.Tour = tour;
                 _tourPointRepository.Save(tourPoint);
             }

             _tours.Add(tour);
             _serializer.ToCSV(FilePath, _tours);
             return tour;
         }
        */

        public Tour SaveImagesTourPoints(Tour tour)
        {
            tour.Id = NextId();

            foreach (Image image in tour.Images)
            {
                image.ResourceId = tour.Id;
                _imageRepository.Save(image);

            }
            foreach (TourPoint tourPoint in tour.TourPoints)
            {
                tourPoint.Tour = tour;
                _tourPointRepository.Save(tourPoint);
            }

            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }


    }
}
