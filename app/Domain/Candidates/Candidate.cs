namespace Domain
{
    public class Candidate
    {
        public Guid Id { get; }
        public Guid VacancyId { get; }
        public string Name { get; }
        public string Mail { get; }
        public CandidateDocument? Document { get; }
        public CandidateWorkflow Workflow { get; }

        private Candidate(Guid id, string name, string mail, CandidateWorkflow workflow, CandidateDocument? document = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(mail));
            ArgumentNullException.ThrowIfNull(nameof(workflow));

            Id = id;
            Name = name;
            Mail = mail;
            Workflow = workflow;
            Document = document;
        }

        public static Candidate Create(string name, string mail, CandidateWorkflow workflow, CandidateDocument? document = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(mail));
            ArgumentNullException.ThrowIfNull(nameof(workflow));

            return new Candidate(Guid.NewGuid(), name, mail, workflow, document);
        }

        public void Approve(Guid userId, string comment)
        {
            Workflow.Approve(userId, comment);
        }

        public void Reject(Guid userId, string comment)
        {
            Workflow.Reject(userId, comment);
        }

        public void Restart()
        {
            Workflow.Restart();
        }
    }
}