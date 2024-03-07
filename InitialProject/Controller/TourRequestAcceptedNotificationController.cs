using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourRequestAcceptedNotificationController
    {
        private readonly TourRequestAcceptedNotificationService _tourRequestAcceptedNotificationService;

        public TourRequestAcceptedNotificationController()
        {

            _tourRequestAcceptedNotificationService = new TourRequestAcceptedNotificationService();
        }

        public List<TourRequestAcceptedNotification> GetAll()
        {
            return _tourRequestAcceptedNotificationService.GetAll();
        }

        public TourRequestAcceptedNotification Get(int id)
        {
            return _tourRequestAcceptedNotificationService.Get(id);
        }

        public TourRequestAcceptedNotification Save(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {

            return _tourRequestAcceptedNotificationService.Save(tourRequestAcceptedNotification);
        }

        public void Delete(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {
            _tourRequestAcceptedNotificationService.Delete(tourRequestAcceptedNotification);
        }


        public void Update(TourRequestAcceptedNotification tourRequestAcceptedNotification)
        {
            _tourRequestAcceptedNotificationService.Update(tourRequestAcceptedNotification);
        }

        public List<TourRequestAcceptedNotification> GetNotificationForUser(int userId)
        {
            return _tourRequestAcceptedNotificationService.GetNotificationForUser(userId);
        }

    }
}
