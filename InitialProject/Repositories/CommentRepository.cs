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
    public class CommentRepository : ICommentRepository
    {
        private const string FilePath = "../../../Resources/Data/comments.csv";

        private readonly Serializer<Comment> _serializer;

        private List<Comment> _comments;

        public CommentRepository()
        {
            _serializer = new Serializer<Comment>();
            _comments = _serializer.FromCSV(FilePath);
        }

        public List<Comment> GetAll()
        {
            return _comments;
        }
        public Comment Get(int id)
        {
            return _comments.Find(c => c.Id == id);
        }
        public Comment Save(Comment comment)
        {
            comment.Id = NextId();
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }
        public int NextId()
        {
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(c => c.Id) + 1;
        }
        public void Delete(Comment comment)
        {
            Comment founded = _comments.Find(c => c.Id == comment.Id);
            _comments.Remove(founded);
            _serializer.ToCSV(FilePath, _comments);
        }

        public Comment Update(Comment comment)
        {
            Comment current = _comments.Find(c => c.Id == comment.Id);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }

        public List<Comment> GetByForumId(int id)
        {
            return _comments.FindAll(c => c.ForumId == id);
        }
    }
}
