namespace Domain
{
    internal class Role
    {
        public Guid NameID { get; }
        public string RoleName { get; }

        private Role(Guid nameID, string roleName)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(nameID));
            NameID = nameID;
            RoleName = roleName;
        }

        public static Role Create(string roleName)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleName));
            return new(Guid.NewGuid(), roleName);
        }
    }
}