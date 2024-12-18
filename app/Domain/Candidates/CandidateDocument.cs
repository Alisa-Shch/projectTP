namespace Domain
{
    public class CandidateDocument
    {
        public string Name { get; }
        public string WorkExperience { get; }

        private CandidateDocument(string name, string workExperience)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(workExperience));

            Name = name;
            WorkExperience = workExperience;
        }

        public static CandidateDocument Create(string name, string workExperience)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(workExperience));

            return new(name, workExperience);
        }
    }
}