namespace app
{
    internal class WorkflowTemplateStep
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid EmployeeID { get; private set; }
        public Guid RoleID { get; private set; }
    }
}