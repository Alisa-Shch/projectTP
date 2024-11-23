namespace Domain
{
    internal class Company
    {
        public Guid Id { get; }
        public string Name { get; }

        private Company(Guid id, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            Name = name;
        }

        public static Company Create(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            return new(Guid.NewGuid(), name);
        }
    }
}