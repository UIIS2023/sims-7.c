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
    public class TourRequestAcceptedNotificationService
    {

        private ITourRequestAcceptedNotificationRepository _tourRequestAcceptedNotificationRepository;
        public TourRequestAcceptedNotificationService()
        {

            _tourRequestAcceptedNotificationRepository = Injector.Injector.CreateInstance<ITourRequestAcceptedNotificationRepository>();
        }


        public List<TourRequestAcceptedNotification> GetAll()
        {
            return _tourRequestAcceptedNotificationRepository.GetAll();
        }
        public TourRequestAcceptedNotification Get(int id)
        {

            return _tourRequestAcceptedNotificationRepository.Get(id);

        }

        public TourRequestAcceptedNotification Save(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {

            return _tourRequestAcceptedNotificationRepository.Save(tourRequestAcceptedNotification);
        }


        public TourRequestAcceptedNotification Update(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {
            return _tourRequestAcceptedNotificationRepository.Update(tourRequestAcceptedNotification);
        }

        public void Delete(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {

            _tourRequestAcceptedNotificationRepository.Delete(tourRequestAcceptedNotification);

        }

        public List<TourRequestAcceptedNotification> GetNotificationForUser(int userId)
        {
            List<TourRequestAcceptedNotification> notificationList = new List<TourRequestAcceptedNotification>();
            var allNotifications = _tourRequestAcceptedNotificationRepository.GetAll();
            for (int i = 0; i < allNotifications.Count(); i++)
            {
                var notification = allNotifications.ElementAt(i);
                if (!notification.IsDelivered && notification.TourRequest.Guest.Id == userId)
                {
                    notification.IsDelivered = true;
                    _tourRequestAcceptedNotificationRepository.Update(notification);
                    notificationList.Add(notification);
                }
            }
            return notificationList;
        }

    }
}
