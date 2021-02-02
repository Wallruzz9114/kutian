namespace Kutian.Domain.Events
{
    public class RoleAdded
    {
        public RoleAdded(string name) => Name = name;

        public string Name { get; }
    }
}