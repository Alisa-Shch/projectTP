using Domain;

namespace Application
{
    public class Document
    {
        public string Name { get; }
        public string WorkExperience { get; }

        public Document(string name, string workExperience)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(workExperience));

            Name = name;
            WorkExperience = workExperience;
        }
    }
}