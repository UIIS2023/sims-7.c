using InitialProject.Domain.Models;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repositories
{
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private static UserRepository instance = null;

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        private UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public static UserRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new UserRepository();
            }
            return instance;
        }

        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public User Get(int id)
        {
            return _users.Find(u => u.Id == id);
        }
    }
}
