namespace Domain
{
    internal class WorkflowTemplateStep
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid EmployeeID { get; private set; }
        public Guid RoleID { get; private set; }

        private WorkflowTemplateStep(string name, string description, Guid employeeID, Guid roleID)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleID));
            Name = name;
            Description = description;
            EmployeeID = employeeID;
            RoleID = roleID;
        }

        public static WorkflowTemplateStep Create(string name, string description, Guid employeeID, Guid roleID)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(employeeID));
            ArgumentException.ThrowIfNullOrEmpty(nameof(roleID));
            return new WorkflowTemplateStep(name, description, employeeID, roleID);
        }
    }
}