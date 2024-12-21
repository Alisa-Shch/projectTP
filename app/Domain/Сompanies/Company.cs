namespace Domain
{
    public class Company
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

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