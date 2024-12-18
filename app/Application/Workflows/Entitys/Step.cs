using Domain;

namespace Application
{
    public class Step
    {
        public Status Status { get; private set; }
        public string? Description { get; private set; }
        public Guid RoleId { get; }
        public Guid EmployeeId { get; }
        public DateTime ModifiedDate { get; private set; }
        public int NumberStep { get; }

        public Step(string description, Guid roleId, Guid employeeId, DateTime modifiedDate, int numberStep)
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
    }
}