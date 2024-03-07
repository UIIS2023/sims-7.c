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
    public class NewTourNotificationRepository : INewTourNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/newTourNotifications.csv";

        private readonly Serializer<NewTourNotification> _serializer;

        private List<NewTourNotification> _notifications;

        public NewTourNotificationRepository()
        {
            _serializer = new Serializer<NewTourNotification>();
            _notifications = _serializer.FromCSV(FilePath);
        }


        public List<NewTourNotification> GetAll()
        {
            return _notifications;
        }

        public NewTourNotification Get(int id)
        {
            return _notifications.Find(n => n.Id == id);

        }


       
        public NewTourNotification Save(NewTourNotification notification)
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
        public void Delete(NewTourNotification notification)
        {
            NewTourNotification founded = _notifications.Find(n => n.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public NewTourNotification Update(NewTourNotification notification)
        {
            NewTourNotification current = _notifications.Find(n => n.Id == notification.Id);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
    }
}
