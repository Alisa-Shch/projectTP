namespace Domain
{
    public class WorkflowTemplateStep
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid RoleId { get; private set; }
        public int NumberStep { get; private set; }
        
        private WorkflowTemplateStep(string name, string description, Guid employeeId, Guid roleId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));

            Name = name;
            Description = description;
            EmployeeId = employeeId;
            RoleId = roleId;
            NumberStep = 1;
        }

        public static WorkflowTemplateStep Create(string name, string description, Guid employeeId, Guid roleId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("EmployeeId cannot be empty", nameof(employeeId));
            }
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("RoleId cannot be empty", nameof(roleId));
            }
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

            return new WorkflowTemplateStep(name, description, employeeId, roleId);
        }
    }
}