namespace Domain
{
    public class CandidateWorkflowStep
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? RoleId { get; private set; }
        public int NumberStep { get; private set; }
        public string? Description { get; private set; }
        public string? Comment { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public Status Status { get; private set; }

        private CandidateWorkflowStep(Guid? employeeId, Guid? roleId, int numberStep, string description, DateTime modifiedDate)
        {
            ArgumentNullException.ThrowIfNull(nameof(employeeId));
            ArgumentNullException.ThrowIfNull(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(numberStep));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(modifiedDate));

            EmployeeId = employeeId;
            RoleId = roleId;
            NumberStep = numberStep;
            Description = description;
            ModifiedDate = modifiedDate;
            Status = Status.InProgress;
        }

        public static CandidateWorkflowStep Create(WorkflowTemplateStep templateStep)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateStep));

            return new(templateStep.RoleId, templateStep.EmployeeId, templateStep.NumberStep, templateStep.Description, DateTime.UtcNow);
        }

        internal void Approve(Employee user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUserAndStatus(user);

            Status = Status.Approved;
            Comment = comment;
            ModifiedDate = DateTime.UtcNow;
        }

        internal void Reject(Employee user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUserAndStatus(user);

            Status = Status.Rejected;
            Comment = comment;
            ModifiedDate = DateTime.UtcNow;
        }

        internal void Restart()
        {
            Status = Status.InProgress;
            Comment = null;
            ModifiedDate = DateTime.UtcNow;
        }

        public void CheckUserAndStatus(Employee user)
        {
            if (user.Id != EmployeeId)
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