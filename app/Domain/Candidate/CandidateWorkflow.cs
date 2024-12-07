namespace Domain
{
    public class CandidateWorkflow
    {
        public Guid Id { get; }
        public string? Description { get; private set; }
        public Guid TemplateId { get; private set; }
        public Guid CandidateId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public DateTime CreateAt { get; }
        public Status Status { get; private set; }
        public string? Comment { get; private set; }
        public IEnumerable<CandidateWorkflowStep> Steps { get; }
        
        private CandidateWorkflow(Guid id, Guid templateId, Guid employeeId, Guid candidateId, IEnumerable<CandidateWorkflowStep> steps, DateTime createAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            ArgumentException.ThrowIfNullOrEmpty(nameof(createAt));

            Id = id;
            TemplateId = templateId;
            EmployeeId = employeeId;
            CandidateId = candidateId;
            Steps = steps.ToList().AsReadOnly();
            CreateAt = createAt;
        }

        public static CandidateWorkflow Create(WorkflowTemplate template, Guid employeeId, Guid candidateId)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));

            return new(Guid.NewGuid(), template.Id, employeeId, candidateId, template.Steps.Select(CandidateWorkflowStep.Create), DateTime.UtcNow);
        }

        public void Approve(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckSteps();
            var step = StepsInProgress();
            step.Approve(userId, comment);
        }

        public void Reject(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckSteps();
            var step = StepsInProgress();
            step.Reject(userId, comment);
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
            if (Steps.All(x => x.Status == Status.Approved))
            {
                throw new Exception("exception");
            }
            if (Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("exception");
            }
        }

        public CandidateWorkflowStep StepsInProgress()
        {
            return Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
        }
    }
}