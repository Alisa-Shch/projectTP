namespace Domain
{
    public class Candidate
    {
        public Guid Id { get; }
        public Guid VacancyId { get; }
        public CandidateDocument Document { get; }
        public CandidateWorkflow Workflow { get; }

        private Candidate(Guid id, CandidateWorkflow workflow, CandidateDocument document)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentNullException.ThrowIfNull(nameof(workflow));
            ArgumentException.ThrowIfNullOrEmpty(nameof(document));

            Id = id;
            Workflow = workflow;
            Document = document;
        }

        public static Candidate Create(CandidateWorkflow workflow, CandidateDocument document)
        {
            ArgumentNullException.ThrowIfNull(nameof(workflow));
            ArgumentException.ThrowIfNullOrEmpty(nameof(document));

            return new Candidate(Guid.NewGuid(), workflow, document);
        }

        public void Approve(Employers user, string comment)
        {
            Workflow.Approve(user, comment);
        }

        public void Reject(Employers user, string comment)
        {
            Workflow.Reject(user, comment);
        }

        public void Restart()
        {
            Workflow.Restart();
        }
    }
}