using Kutian.Domain.Entities;
using Xunit;

namespace Kutian.Tests.Unit.Models
{
    public class UserTests
    {
        [Fact]
        public void ShouldCreateUser()
        {
            var user = new User("testuser2@gmail.com", "Pa$$w0rd");

            Assert.NotEqual(default, user.UserId);
            Assert.NotEqual(default, user.Roles);
        }

        [Fact]
        public void ShouldAddRole()
        {
            var user = new User("testuser2@gmail.com", "Pa$$w0rd");
            user.AddRole("Admin");
            Assert.Single(user.Roles);
        }

        [Fact]
        public void ShouldRemoveRole()
        {
            var user = new User("testuser2@gmail.com", "Pa$$w0rd");
            user.AddRole("Admin");
            user.RemoveRole("Admin");
            Assert.Empty(user.Roles);
        }

        [Fact]
        public void ShouldChangePassword()
        {
            var user = new User("testuser2@gmail.com", "");
            var salt1 = user.Salt;
            user.ChangePassword("Foo");
            var salt2 = user.Salt;

            Assert.Equal("Foo", user.Password);
            Assert.Equal(salt1, salt2);
        }
    }
}