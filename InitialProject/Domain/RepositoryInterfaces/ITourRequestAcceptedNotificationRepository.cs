using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourRequestAcceptedNotificationRepository
    {

        TourRequestAcceptedNotification Get(int id);
        List<TourRequestAcceptedNotification> GetAll();

        TourRequestAcceptedNotification Save(TourRequestAcceptedNotification tourRequestAcceptedNotification);

        void Delete(TourRequestAcceptedNotification tourRequestAcceptedNotification);

        TourRequestAcceptedNotification Update(TourRequestAcceptedNotification tourRequestAcceptedNotification);
    }
}