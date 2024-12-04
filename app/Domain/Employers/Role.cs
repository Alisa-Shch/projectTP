namespace Domain
{
    public class Role
    {
        public Guid Id { get; }
        public string Name { get; }

        private Role(Guid id,string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            Name = name;
        }

        public static Role Create(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            return new(Guid.NewGuid(), name);
        }
    }
}