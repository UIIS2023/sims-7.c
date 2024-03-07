using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class NewTourNotificationController
    {
        private readonly NewTourNotificationService _notificationService;

        public NewTourNotificationController()
        {
            _notificationService = new NewTourNotificationService();
        }

        public List<NewTourNotification> GetAll()
        {
            return _notificationService.GetAll();
        }

        public NewTourNotification Get(int id)
        {
            return _notificationService.Get(id);
        }

        public NewTourNotification Save(NewTourNotification notification)
        {

            return _notificationService.Save(notification);
        }

        public void Delete(NewTourNotification notification)
        {
            _notificationService.Delete(notification);
        }


        public void Update(NewTourNotification notification)
        {
            _notificationService.Update(notification);
        }

        public List<NewTourNotification> GetNotificationForUser(int userId)
        {
            return _notificationService.GetNotificationForUser(userId);
        }

    }
}
