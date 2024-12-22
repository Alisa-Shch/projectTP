namespace Domain
{
    public class CandidateDocument
    {
        public string Name { get; private set; }
        public string WorkExperience { get; private set; }

        private CandidateDocument(string name, string workExperience)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(workExperience, nameof(workExperience));

            Name = name;
            WorkExperience = workExperience;
        }

        public static CandidateDocument Create(string name, string workExperience)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(workExperience, nameof(workExperience));

            return new(name, workExperience);
        }
    }
}