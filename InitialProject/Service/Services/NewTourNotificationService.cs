using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.Services
{
    public class NewTourNotificationService 
    {
        private INewTourNotificationRepository _notificationRepository;

        public NewTourNotificationService()
        {
            _notificationRepository = Injector.Injector.CreateInstance<INewTourNotificationRepository>();
        }

        public List<NewTourNotification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public NewTourNotification Get(int id)
        {
            return _notificationRepository.Get(id);
        }

        public NewTourNotification Save(NewTourNotification notification)
        {
            return _notificationRepository.Save(notification);
        }

        public void Delete(NewTourNotification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public void Update(NewTourNotification notification)
        {
            _notificationRepository.Update(notification);
        }
        public List<NewTourNotification> GetNotificationForUser(int userId)
        {
            List<NewTourNotification> notificationList = new List<NewTourNotification>();
            var allNotifications = _notificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.Guest.Id == userId)
                {
                    notification.IsDelivered = true;
                    _notificationRepository.Update(notification);
                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }

    }
}
