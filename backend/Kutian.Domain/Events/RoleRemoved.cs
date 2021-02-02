namespace Kutian.Domain.Events
{
    public class RoleRemoved
    {
        public RoleRemoved(string value) => Name = value;

        public string Name { get; }
    }
}