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
    public class TourRequestAcceptedNotificationRepository : ITourRequestAcceptedNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequestAcceptedNotifications.csv";


        private readonly Serializer<TourRequestAcceptedNotification> _serializer;

        private List<TourRequestAcceptedNotification> _tourRequestAcceptedNotifications;

        public TourRequestAcceptedNotificationRepository()
        {
            _serializer = new Serializer<TourRequestAcceptedNotification>();
            _tourRequestAcceptedNotifications = _serializer.FromCSV(FilePath);
        }



        public List<TourRequestAcceptedNotification> GetAll()
        {
            return _tourRequestAcceptedNotifications;
        }

        public int NextId()
        {

            if (_tourRequestAcceptedNotifications.Count < 1)
            {
                return 1;
            }
            return _tourRequestAcceptedNotifications.Max(t => t.Id) + 1;
        }

        public TourRequestAcceptedNotification Get(int id)
        {

            return _tourRequestAcceptedNotifications.Find(x => x.Id == id);

        }
        public TourRequestAcceptedNotification Save(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {
            tourRequestAcceptedNotification.Id = NextId();
            _tourRequestAcceptedNotifications.Add(tourRequestAcceptedNotification);
            _serializer.ToCSV(FilePath, _tourRequestAcceptedNotifications);
            return tourRequestAcceptedNotification;

        }

        public void Delete(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {
            TourRequestAcceptedNotification founded = _tourRequestAcceptedNotifications.Find(t => t.Id == tourRequestAcceptedNotification.Id);
            _tourRequestAcceptedNotifications.Remove(founded);
            _serializer.ToCSV(FilePath, _tourRequestAcceptedNotifications);
        }


        public TourRequestAcceptedNotification Update(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {

            TourRequestAcceptedNotification current = _tourRequestAcceptedNotifications.Find(v => v.Id == tourRequestAcceptedNotification.Id);
            int index = _tourRequestAcceptedNotifications.IndexOf(current);
            _tourRequestAcceptedNotifications.Remove(current);
            _tourRequestAcceptedNotifications.Insert(index, tourRequestAcceptedNotification);
            _serializer.ToCSV(FilePath, _tourRequestAcceptedNotifications);
            return tourRequestAcceptedNotification;
        }

        public void BindTourRequestAcceptedNotificationTourRequest()
        {
            foreach (TourRequestAcceptedNotification notification in _tourRequestAcceptedNotifications)
            {
                int tourRequestId = notification.TourRequest.Id;

                TourRequest request = Injector.Injector.CreateInstance<ITourRequestRepository>().Get(tourRequestId);
                if (request != null)
                {
                    notification.TourRequest = request;
                }
                else
                {
                    Console.WriteLine("Error in notificationTourReservation binding");
                }
            }
        }
    }
}
