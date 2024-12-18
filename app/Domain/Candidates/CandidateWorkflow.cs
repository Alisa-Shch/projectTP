namespace Domain
{
    public class CandidateWorkflow
    {
        public Guid Id { get; }
        public string? Description { get; private set; }
        public Guid TemplateId { get; private set; }
        public string? Comment { get; private set; }
        public IReadOnlyCollection<CandidateWorkflowStep> Steps { get; }

        private CandidateWorkflow(Guid id, Guid templateId, IReadOnlyCollection<CandidateWorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            Id = id;
            TemplateId = templateId;
            Steps = steps;
        }

        public static CandidateWorkflow Create(WorkflowTemplate template)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));

            return new(Guid.NewGuid(), template.Id, new List<CandidateWorkflowStep>(template.Steps.Select(CandidateWorkflowStep.Create)));
        }

        internal void Approve(Employers user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Approve(user, comment);
        }

        internal void Reject(Employers user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Reject(user, comment);
        }

        internal void Restart()
        {
            CheckStatus();

            foreach (var step in Steps)
            {
                step.Restart();
            }
        }

        public void CheckStatus()
        {
            if (Steps.All(x => x.Status == Status.Approved))
            {
                throw new Exception("All steps are already approved.");
            }
            if (Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("Workflow contains rejected steps.");
            }
        }

        public CandidateWorkflowStep GetStepInProgress()
        {
            return Steps.OrderBy(x => x.NumberStep).FirstOrDefault(x => x.Status == Status.InProgress)
                   ?? throw new InvalidOperationException("No steps are in progress.");
        }
    }
}