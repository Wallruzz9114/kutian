namespace Kutian.Domain.Events
{
    public class UserRoleAdded
    {
        public UserRoleAdded(string name) => Name = name;

        public string Name { get; }
    }
}