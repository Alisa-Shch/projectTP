namespace Domain
{
    public class Workflow
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TemplateId { get; private set; }
        public Guid CandidateId { get; private set; }
        public Guid InvitingId { get; private set; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }
        public DateTime CreateAt { get; }

        private Workflow(Guid id, Guid templateId, Guid invitingId, Guid candidateId, IReadOnlyCollection<WorkflowStep> steps, DateTime createAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            ArgumentException.ThrowIfNullOrEmpty(nameof(createAt));

            Id = id;
            TemplateId = templateId;
            InvitingId = invitingId;
            CandidateId = candidateId;
            Steps = steps.ToList().AsReadOnly();
            CreateAt = createAt;
        }

        public static Workflow Create(WorkflowTemplate template, Guid invitingId, Guid candidateId)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));

            return new Workflow(Guid.NewGuid(), Guid.NewGuid(), invitingId, candidateId, (IReadOnlyCollection<WorkflowStep>)template.Steps.Select(x => WorkflowStep.Create(x)), DateTime.UtcNow);
        }

        public void Approve(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));

            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("что это?");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Approve(user, message);
        }

        public void Reject(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));

            CheckSteps();
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Reject(user, message);
        }

        public void Restart()
        {
            CheckSteps();
            foreach (var step in Steps)
            {
                step.Restart();
            }
        }

        public void CheckSteps()
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("что это?");
            }
        }
    }
}