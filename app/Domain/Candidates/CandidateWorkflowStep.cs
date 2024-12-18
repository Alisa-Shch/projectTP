namespace Domain
{
    public class CandidateWorkflowStep
    {
        public Guid? EmployeeId { get; }
        public Guid? RoleId { get; }
        public string? Description { get; private set; }
        public Status Status { get; private set; }
        public string? Comment { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public int NumberStep { get; }

        private CandidateWorkflowStep(string description, Guid? employeeId, Guid? roleId, DateTime modifiedDate, int numberStep)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(modifiedDate));
            ArgumentException.ThrowIfNullOrEmpty(nameof(numberStep));

            Status = Status.InProgress;
            Description = description;
            EmployeeId = employeeId;
            RoleId = roleId;
            ModifiedDate = modifiedDate;
            NumberStep = numberStep;
        }

        public static CandidateWorkflowStep Create(WorkflowTemplateStep templateStep)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateStep));

            return new(templateStep.Description, templateStep.RoleId, templateStep.EmployeeId, DateTime.UtcNow, 1);
        }

        public void Approve(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUser(userId);

            Status = Status.Approved;
            Comment = comment;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Reject(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUser(userId);

            Status = Status.Rejected;
            Comment = comment;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Restart()
        {
            Status = Status.InProgress;
            Comment = null;
            ModifiedDate = DateTime.UtcNow;
        }

        public void CheckUser(Guid userId)
        {
            if (userId != EmployeeId)
            {
                throw new Exception("User is not authorized to approve/reject this step.");
            }
            if (Status != Status.InProgress)
            {
                throw new Exception("The step cannot be processed.");
            }
        }
    }
}