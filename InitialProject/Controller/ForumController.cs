using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class ForumController
    {
        private readonly ForumService _forumService;

        public ForumController()
        {
            _forumService = new ForumService();
        }

        public List<Forum> GetAll()
        {
            return _forumService.GetAll();
        }

        public Forum Get(int id)
        {
            return _forumService.Get(id);
        }
        public Forum Save(Forum forum)
        {
            return _forumService.Save(forum);
        }
        public void Delete(Forum forum)
        {
            _forumService.Delete(forum);
        }
        public void Update(Forum forum)
        {
            _forumService.Update(forum);
        }

        public Forum SaveForumComment(Forum forum)
        {
            return _forumService.SaveForumComment(forum);
        }

        public Boolean AvailableForum(Forum forum)
        {
            return _forumService.AvailableForum(forum);
        }

        public List<Forum> GetByAuthorId(int id)
        {
            return _forumService.GetByAuthorId(id);
        }
    }
}
