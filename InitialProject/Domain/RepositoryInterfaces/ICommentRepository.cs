using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        public List<Comment> GetAll();
        public Comment Get(int id);
        public Comment Save(Comment comment);
        public void Delete(Comment comment);
        public Comment Update(Comment comment);
        public int NextId();

        public List<Comment> GetByForumId(int id);
    }
}
