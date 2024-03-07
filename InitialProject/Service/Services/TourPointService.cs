using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Service.Services
{
    public class TourPointService
    {
        private TourPointRepository _tourPointRepository;
        private TourEventRepository _tourEventRepository;

        public TourPointService()
        {
            _tourPointRepository = TourPointRepository.GetInstance();
        }

        public List<TourPoint> GetAll()
        {
            return _tourPointRepository.GetAll();
        }

        public TourPoint Get(int id)
        {
            return _tourPointRepository.Get(id);
        }

        public TourPoint Save(TourPoint tourPoint)
        {

            return _tourPointRepository.Save(tourPoint);
        }

        public void Delete(TourPoint tourPoint)
        {

            _tourPointRepository.Delete(tourPoint);

        }

        public TourPoint Update(TourPoint tourPoint)
        {
            return _tourPointRepository.Update(tourPoint);
        }



        public List<TourPoint> GetByTour(int id)
        {
            return _tourPointRepository.GetByTour(id);
        }

       /* public List<TourPoint> GetAllTourPointsForTourEvent(TourEvent selectedTourEvent)
        {
            List<TourPoint> tourPoints = new List<TourPoint>();
            foreach(TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                if(tourEvent == selectedTourEvent)
                {

                }
            }

            return tourPoints;
        }*/
    }
}
