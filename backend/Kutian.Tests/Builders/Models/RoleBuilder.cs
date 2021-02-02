using static Kutian.Domain.Entities.User;

namespace Kutian.Tests.Builders.Models
{
    public class RoleBuilder
    {
        private readonly Role _role;

        public RoleBuilder(string name) => _role = new Role(name);

        public Role Build() => _role;
    }
}