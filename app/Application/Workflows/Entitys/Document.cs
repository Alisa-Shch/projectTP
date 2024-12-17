namespace Application
{
    public class Document
    {
        public string Name { get; }
        public string WorkExperience { get; }

        public Document(string name, string workExperience)
        {
            Name = name;
            WorkExperience = workExperience;
        }
    }
}