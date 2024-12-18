using System.Collections.ObjectModel;

namespace Domain
{
    public class CandidateWorkflow
    {
        public Guid Id { get; }
        public string? Description { get; private set; }
        public Guid TemplateId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Guid? RoleId { get; private set; }
        public DateTime CreateAt { get; }
        public Status Status { get; private set; }
        public string? Comment { get; private set; }
        public ReadOnlyCollection<CandidateWorkflowStep> Steps { get; }

        private CandidateWorkflow(Guid id, Guid templateId, Guid? employeeId, Guid? roleId, ReadOnlyCollection<CandidateWorkflowStep> steps, DateTime createAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            ArgumentException.ThrowIfNullOrEmpty(nameof(createAt));

            Id = id;
            TemplateId = templateId;
            EmployeeId = employeeId;
            RoleId = roleId;
            Steps = steps;
            CreateAt = createAt;
        }

        public static CandidateWorkflow Create(WorkflowTemplate template, Guid? employeeId, Guid? roleId)
        {
            ArgumentNullException.ThrowIfNull(nameof(template));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));

            return new(Guid.NewGuid(), template.Id, employeeId, roleId, new ReadOnlyCollection<CandidateWorkflowStep>(template.Steps.Select(CandidateWorkflowStep.Create).ToList()), DateTime.UtcNow);
        }

        public void Approve(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Approve(userId, comment);
        }

        public void Reject(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Reject(userId, comment);
        }

        public void Restart()
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