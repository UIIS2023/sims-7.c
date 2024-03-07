using InitialProject.Domain.Models;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class CommentController
    {
        private readonly CommentService _commentService;

        public CommentController()
        {
            _commentService = new CommentService();
        }

        public List<Comment> GetAll()
        {
            return _commentService.GetAll();
        }

        public Comment Get(int id)
        {
            return _commentService.Get(id);
        }
        public Comment Save(Comment comment)
        {
            return _commentService.Save(comment);
        }
        public void Delete(Comment comment)
        {
            _commentService.Delete(comment);
        }
        public void Update(Comment comment)
        {
            _commentService.Update(comment);
        }

        public List<Comment> GetByForumId(int id)
        {

            return _commentService.GetByForumId(id);
        }
    }
}
