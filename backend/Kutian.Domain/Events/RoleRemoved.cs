namespace Kutian.Domain.Events
{
    public class UserRoleRemoved
    {
        public UserRoleRemoved(string value) => Name = value;

        public string Name { get; }
    }
}