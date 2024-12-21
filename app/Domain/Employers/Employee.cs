namespace Domain
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public Guid RoleId { get; private set; }
        public string Name { get; private set; }

        private Employee(Guid id, Guid roleId, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            RoleId = roleId;
            Name = name;
        }

        public static Employee Create(Guid roleId, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            return new(Guid.NewGuid(), roleId, name);
        }
    }
}