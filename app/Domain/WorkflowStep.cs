namespace Domain
{
    internal class WorkflowStep
    {
        public Status Status { get; private set; }
        public string Message { get; private set; }
        public string Description { get; private set; }
        public Guid RoleID { get; }
        public Guid EmployeeID { get; }
        public DateTime ModifiedDate { get; private set; }
        public int NumberStep { get; }

        private WorkflowStep(Guid roleID, string description)
        {
            Status = Status.InProgress;
            Description = description;
            RoleID = roleID;
            ModifiedDate = DateTime.Now;
        }

        public static WorkflowStep Create()
        {
            return new();
        }

        public void Approve(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));
            if ((user.ID != EmployeeID && user.RoleID != RoleID) || Status != Status.InProgress)
            {
                throw new Exception("!");
            }
            Status = Status.Approved;
            Message = message;
        }

        public void Reject(Employers user, string message)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            ArgumentException.ThrowIfNullOrEmpty(nameof(message));
            if ((user.ID != EmployeeID && user.RoleID != RoleID) || Status != Status.InProgress)
            {
                throw new Exception("!");
            }
            Status = Status.Rejected;
            Message = message;
        }
    }
}