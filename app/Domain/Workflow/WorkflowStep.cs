namespace Domain
{
    internal class WorkflowStep
    {
        public Status Status { get; private set; }
        public string? Message { get; private set; }
        public string Description { get; private set; }
        public Guid RoleId { get; }
        public Guid EmployeeId { get; }
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
            RoleId = roleId;
            EmployeeId = employeeId;
            ModifiedDate = modifiedDate;
            NumberStep = numberStep;
        }

        public static WorkflowStep Create(WorkflowStep templateStep)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateStep));

            return new WorkflowStep(templateStep.Description, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, 1);
        }

        public void Approve(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));

            CheckUser(user);
            Status = Status.Approved;
            Message = message;
            ModifiedDate = DateTime.UtcNow;
        }

        public void Reject(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));

            CheckUser(user);
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

        public void CheckUser(Employers user)
        {
            if ((user.Id != EmployeeId && user.RoleID != RoleId) || Status != Status.InProgress)
            {
                throw new Exception("");
            }
        }
    }
}