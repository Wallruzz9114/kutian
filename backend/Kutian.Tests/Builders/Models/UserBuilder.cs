using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder(string username, string password) => _user = new User(username, password);

        public User Build() => _user;
    }
}