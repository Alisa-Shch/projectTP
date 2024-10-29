namespace Domain
{
    internal class Workflow
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TemplateID { get; private set; }
        public Guid CandidateID { get; private set; }
        public Guid InvitingEID { get; private set; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }
        public DateTime CreateAt { get; }

        private Workflow(Guid templateID, Guid invitingEID, Guid candidateID, IReadOnlyCollection<WorkflowStep> steps, DateTime createAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingEID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            ArgumentException.ThrowIfNullOrEmpty(nameof(createAt));
            TemplateID = templateID;
            CandidateID = candidateID;
            InvitingEID = invitingEID;
            Steps = steps.ToList().AsReadOnly();
            CreateAt = createAt;
        }

        public static Workflow Create(WorkflowTemplate template, Guid invitingEID, Guid candidateID)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingEID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateID));
            return new Workflow(Guid.NewGuid(), invitingEID, candidateID, (IReadOnlyCollection<WorkflowStep>)template.Steps.Select(x => WorkflowStep.Create(x)), DateTime.UtcNow);
        }

        public void Approve(Employers user, string message)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("User cannot approve.");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Approve(user, message);
        }

        public void Reject(Employers user, string message)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("User cannot reject.");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Reject(user, message);
        }

        public void Restart(Employers user)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("It is not possible to restart, the workflow is completed.");
            }
            foreach (var step in Steps)
            {
                step.Restart();
            }
        }
    }
}