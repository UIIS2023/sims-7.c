using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INewTourNotificationRepository
    {
        List<NewTourNotification> GetAll();

        NewTourNotification Get(int id);

        public NewTourNotification Save(NewTourNotification notification);

        void Delete(NewTourNotification notification);

        NewTourNotification Update(NewTourNotification notification);
    }
}
