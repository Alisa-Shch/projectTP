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

            return new(templateStep.Description, templateStep.RoleId, templateStep.EmployeeId, DateTime.UtcNow, templateStep.NumberStep);
        }

        internal void Approve(Employers user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            Check(user);

            Status = Status.Approved;
            Comment = comment;
            ModifiedDate = DateTime.UtcNow;
        }

        internal void Reject(Employers user, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            Check(user);

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

        public void Check(Employers user)
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