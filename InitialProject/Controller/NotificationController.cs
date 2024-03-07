using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class NotificationController
    {
        private readonly NotificationService _notificationService;

        public NotificationController()
        {
            _notificationService = new NotificationService();
        }

        public List<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }

        public Notification Get(int id)
        {
            return _notificationService.Get(id);
        }

        public Notification Save(Notification notification)
        {

            return _notificationService.Save(notification);
        }

        public void Delete(Notification notification)
        {
            _notificationService.Delete(notification);
        }

      
        public void Update(Notification notification)
        {
            _notificationService.Update(notification);
        }

        public List<Notification> GetNotificationForUser(int userId)
        {
            return _notificationService.GetNotificationForUser(userId);
        }

        public int NextId()
        {
            return _notificationService.NextId();
        }

    }
}
