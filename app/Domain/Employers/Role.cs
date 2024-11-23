namespace Domain
{
    internal class Role
    {
        public Guid Id { get; }
        public string RoleName { get; }

        private Role(Guid id,string roleName)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleName));

            Id = id;
            RoleName = roleName;
        }

        public static Role Create(string roleName)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleName));

            return new(Guid.NewGuid(), roleName);
        }
    }
}