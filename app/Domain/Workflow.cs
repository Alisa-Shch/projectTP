namespace Domain
{
    internal class Workflow
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TemplateID { get; private set; }
        public Guid CandidateId { get; private set; }
        public Guid InvitingEID { get; private set; }
        public Guid ID { get; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }
        public DateTime CreateAt { get; }

        private Workflow(Guid id, IReadOnlyCollection<WorkflowStep> steps, DateTime createAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ID = id;
            Steps = steps;
            CreateAt = createAt;
        }

        public static Workflow Create(WorkflowTemplate template)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));
            return new(Guid.NewGuid(), template.Steps.Select(x => WorkflowStep.Create(x)), DateTime.UtcNow);
        }

        public void Approve(Employers user, string message)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("!");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Approve(user, message);
        }

        public void Reject(Employers user, string message)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("!");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Reject(user, message);
        }

        public void Restart(Employers user)
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("!");
            }
        }
    }
}