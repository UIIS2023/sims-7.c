using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class TourEventRepository
    {

        private const string FilePath = "../../../Resources/Data/tourEvents.csv";

        private static TourEventRepository instance = null;

        private readonly Serializer<TourEvent> _serializer;

        private List<TourEvent> _tourEvents;

        private TourEventRepository()
        {

            _serializer = new Serializer<TourEvent>();
            _tourEvents = _serializer.FromCSV(FilePath);
        }

        public static TourEventRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourEventRepository();
            }
            return instance;
        }

        public void BindTourEventTour()
        {
            foreach (TourEvent tourEvent in _tourEvents)
            {
                int tourId = tourEvent.Tour.Id;
                Tour tour = TourRepository.GetInstance().Get(tourId);
                if (tour != null)
                {
                    tourEvent.Tour = tour;
                    tour.TourEvents.Add(tourEvent);
                }
                else
                {
                    Console.WriteLine("Error in tourEventTour binding");
                }
            }
        }
        public TourEvent Save(TourEvent tourEvent)
        {
            tourEvent.Id = NextId();
            _tourEvents.Add(tourEvent);
            _serializer.ToCSV(FilePath, _tourEvents);
            return tourEvent;

        }

        public int NextId()
        {
            if (_tourEvents.Count < 1)
            {
                return 1;
            }
            return _tourEvents.Max(te => te.Id) + 1;
        }

        public void Delete(TourEvent tourEvent)
        {
            TourEvent founded = _tourEvents.Find(tp => tp.Id == tourEvent.Id);
            _tourEvents.Remove(founded);
            _serializer.ToCSV(FilePath, _tourEvents);
        }

        public TourEvent Update(TourEvent tourEvent)
        {
            TourEvent current = _tourEvents.Find(te => te.Id == tourEvent.Id);
            int index = _tourEvents.IndexOf(current);
            _tourEvents.Remove(current);
            _tourEvents.Insert(index, tourEvent);
            _serializer.ToCSV(FilePath, _tourEvents);
            return tourEvent;
        }

        public List<TourEvent> GetAll()
        {

            return _tourEvents;

        }


        public TourEvent Get(int id)
        {

            return _tourEvents.Find(x => x.Id == id);

        }

        public List<TourEvent> GetByTour(int tourId)
        {
            return _tourEvents.FindAll(t => t.Tour.Id == tourId);
        }
    }
}
