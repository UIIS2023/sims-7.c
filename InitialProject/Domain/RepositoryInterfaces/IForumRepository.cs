using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IForumRepository
    {
        public List<Forum> GetAll();
        public Forum Get(int id);
        public Forum Save(Forum forum);
        public void Delete(Forum forum);
        public Forum Update(Forum forum);
        public int NextId();

        public Forum SaveForumComment(Forum forum);

        public void BindForumLocation();

        public List<Forum> GetByAuthorId(int id);
    }
}
