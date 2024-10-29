namespace Domain
{
    internal class WorkflowStep
    {
        public Status Status { get; private set; }
        public string? Message { get; private set; }
        public string Description { get; private set; }
        public Guid RoleID { get; }
        public Guid EmployeeID { get; }
        public DateTime ModifiedDate { get; private set; }
        public int NumberStep { get; }

        private WorkflowStep(string description, Guid roleId, Guid employeeId, DateTime modifiedDate, int numberStep)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(modifiedDate));
            ArgumentException.ThrowIfNullOrEmpty(nameof(numberStep));
            Status = Status.InProgress;
            Description = description;
            RoleID = roleId;
            EmployeeID = employeeId;
            ModifiedDate = modifiedDate;
            NumberStep = numberStep;
        }

        public static WorkflowStep Create(WorkflowStep templateStep)
        {
            return new WorkflowStep(templateStep.Description, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, 1);
        }

        public void Approve(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));
            if ((user.ID != EmployeeID && user.RoleID != RoleID) || Status != Status.InProgress)
            {
                throw new Exception("User cannot approve this step.");
            }
            Status = Status.Approved;
            Message = message;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Reject(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));
            if ((user.ID != EmployeeID && user.RoleID != RoleID) || Status != Status.InProgress)
            {
                throw new Exception("User cannot reject this step.");
            }
            Status = Status.Rejected;
            Message = message;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Restart()
        {
            Status = Status.InProgress;
            Message = null;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}