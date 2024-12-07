namespace Domain
{
    public class Candidate
    {
        public Guid Id { get; }
        public Guid VacancyId { get; }
        public Employers? ReferralId { get; }
        public string Name { get; }
        public string Mail { get; }
    
        private Candidate(Guid id, string name, string mail)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(mail));

            Id = id;
            Name = name;
            Mail = mail;
        }

        public static Candidate Create(string name, string mail)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(mail));

            return new(Guid.NewGuid(), name, mail);
        }
    }
}