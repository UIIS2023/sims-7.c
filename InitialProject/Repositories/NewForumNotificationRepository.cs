using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class NewForumNotificationRepository: INewForumNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/newForumNotifications.csv";

        private readonly Serializer<NewForumNotification> _serializer;

        private List<NewForumNotification> _notifications;

        public NewForumNotificationRepository()
        {
            _serializer = new Serializer<NewForumNotification>();
            _notifications = _serializer.FromCSV(FilePath);
        }


        public List<NewForumNotification> GetAll()
        {
            return _notifications;
        }

        public NewForumNotification Get(int id)
        {
            return _notifications.Find(n => n.Id == id);

        }

        public NewForumNotification Save(NewForumNotification notification)
        {

            notification.Id = NextId();
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }
        public int NextId()
        {
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(n => n.Id) + 1;
        }
        public void Delete(NewForumNotification notification)
        {
            NewForumNotification founded = _notifications.Find(n => n.Id == notification.Id);
            _notifications.Remove(founded);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public NewForumNotification Update(NewForumNotification notification)
        {
            NewForumNotification current = _notifications.Find(n => n.Id == notification.Id);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);
            _serializer.ToCSV(FilePath, _notifications);
            return notification;
        }

        public void BindNotificationForum()
        {
            foreach (NewForumNotification notification in _notifications)
            {
                int forumId = notification.Forum.Id;
                Forum forum = Injector.Injector.CreateInstance<IForumRepository>().Get(forumId);
                if (forum != null)
                {
                    notification.Forum = forum;
                }
                else
                {
                    Console.WriteLine("Error in notificationForum binding");
                }
            }
        }

    }
}
