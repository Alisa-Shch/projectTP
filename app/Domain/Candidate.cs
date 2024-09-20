namespace Domain
{
    internal class Candidate
    {
        public string Name { get; }
        public string Mail { get; }
    
        private Candidate(string name, string mail)
        {
            Name = name;
            Mail = mail;
        }

        public static Candidate Create(string name, string mail)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(mail));
            return new(name, mail);
        }
    }
}