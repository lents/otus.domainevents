using DomainEventsWithMediatR.Domain.Models;

namespace DomainEventsWithMediatR.Domain.Services
{
    public class UserService
    {
        private static int _id = 1;

        private static List<User> _users = new();

        public User? GetUser(int userId)
            => _users.FirstOrDefault(x => x.Id == userId);

        public int CreateUser(string name)
        {
            var u = new User { Id = _id++, Name = name };
            _users.Add(u);
            return u.Id;
        }
    }
}
