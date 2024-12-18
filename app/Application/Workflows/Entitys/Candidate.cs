using Domain;

namespace Application
{
    public class Candidate
    {
        public Guid Id { get; }
        public string Name { get; }

        public Candidate(Guid id, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            Id = id;
            Name = name;
        }
    }
}