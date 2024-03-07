using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INewForumNotificationRepository
    {
        List<NewForumNotification> GetAll();
        NewForumNotification Get(int id);
        NewForumNotification Save(NewForumNotification newForumNotification);
        void Delete(NewForumNotification newForumNotification);
        NewForumNotification Update(NewForumNotification newForumNotification);
    }
}
