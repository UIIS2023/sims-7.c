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
    public class NotificationRepository : INotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";
        

        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }
       

        public List<Notification> GetAll()
        {
            return _notifications;
        }

        public Notification Get(int id)
        {
            return _notifications.Find(n => n.Id == id);

        }

        public void BindNotificationTourReservation()
        {
            foreach (Notification notification in _notifications)
            {
                int tourReservationId = notification.TourReservation.Id;
                TourReservation reservation = TourReservationRepository.GetInstance().Get(tourReservationId);
                if (reservation != null)
                {
                    notification.TourReservation = reservation;
                }
                else
                {
                    Console.WriteLine("Error in notificationTourReservation binding");
                }
            }
        }

        public Notification Save(Notification notification)
        {

            notification.Id = NextId();
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
        public int NextId()
        {
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(n => n.Id) + 1;
        }
        public void Delete(Notification notification)
        {
            Notification founded = _notifications.Find(n => n.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public Notification Update(Notification notification)
        {
            Notification current = _notifications.Find(n => n.Id == notification.Id);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);        
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
    }

}
