namespace Domain
{
    internal class WorkflowTemplateStep
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid RoleId { get; private set; }

        private WorkflowTemplateStep(Guid id, string name, string description, Guid employeeId, Guid roleId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));

            Id = id;
            Name = name;
            Description = description;
            EmployeeId = employeeId;
            RoleId = roleId;
        }

        public static WorkflowTemplateStep Create(string name, string description, Guid employeeId, Guid roleId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleId));

            return new WorkflowTemplateStep(Guid.NewGuid(), name, description, employeeId, roleId);
        }
    }
}