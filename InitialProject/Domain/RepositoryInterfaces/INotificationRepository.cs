using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INotificationRepository 
    {
        List<Notification> GetAll();

        Notification Get(int id);

        public Notification Save(Notification notification);

        void Delete(Notification notification);

        Notification Update(Notification notification);

        public int NextId();


    }
}

