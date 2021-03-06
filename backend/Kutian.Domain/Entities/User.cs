using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Kutian.Domain.Events;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Utils;

namespace Kutian.Domain.Entities
{
    public class User : AggregateRoot
    {
        public User(string username, string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var transformedPassword = new PasswordHasher().HashPassword(salt, password);

            Apply(new UserCreated(Guid.NewGuid(), username, transformedPassword, salt));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreated userCreated)
        {
            UserId = userCreated.UserId;
            Salt = userCreated.Salt;
            Username = userCreated.Username;
            Password = userCreated.Password;
            Roles = new HashSet<Role>();
        }

        protected void When(PasswordChanged userPasswordChanged) => Password = userPasswordChanged.Password;

        protected void When(UserRoleAdded userRoleAdded) => Roles.Add(new Role(userRoleAdded.Name));

        protected void When(UserRoleRemoved userRoleRemoved) => Roles.Remove(new Role(userRoleRemoved.Name));

        protected override void EnsureValidState() { }

        public void ChangePassword(string password) => Apply(new PasswordChanged(password));

        public void AddRole(string name) => Apply(new UserRoleAdded(name));

        public void RemoveRole(string value) => Apply(new UserRoleRemoved(value));

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }

        public record Role
        {
            public Role(string name) => Name = name;
            public string Name { get; private set; }
        }
    }
}