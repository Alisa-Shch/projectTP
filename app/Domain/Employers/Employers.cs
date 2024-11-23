namespace Domain
{
    internal class Employers
    {
        public Guid Id { get; }
        public Guid RoleID { get; }
        public string Name { get; }
    
        private Employers(Guid id, Guid roleId, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            RoleID = roleId;
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