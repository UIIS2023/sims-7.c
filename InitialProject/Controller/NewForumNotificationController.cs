using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class NewForumNotificationController
    {
        private readonly NewForumNotificationService _notificationService;

        public NewForumNotificationController()
        {
            _notificationService = new NewForumNotificationService();
        }

        public List<NewForumNotification> GetAll()
        {
            return _notificationService.GetAll();
        }

        public NewForumNotification Get(int id)
        {
            return _notificationService.Get(id);
        }

        public NewForumNotification Save(NewForumNotification notification)
        {

            return _notificationService.Save(notification);
        }

        public void Delete(NewForumNotification notification)
        {
            _notificationService.Delete(notification);
        }


        public void Update(NewForumNotification notification)
        {
            _notificationService.Update(notification);
        }

        public List<NewForumNotification> GetNotificationForUser(int userId)
        {
            return _notificationService.GetNotificationForUser(userId);
        }
    }
}
