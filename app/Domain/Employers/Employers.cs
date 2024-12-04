namespace Domain
{
    public class Employers
    {
        public Guid Id { get; }
        public Guid RoleId { get; }
        public string Name { get; }
    
        private Employers(Guid id, Guid roleId, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            RoleId = roleId;
            Name = name;
        }

        public static Employers Create(Guid roleId, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            return new(Guid.NewGuid(), roleId, name);
        }
    }
}