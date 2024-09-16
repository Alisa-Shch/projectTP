namespace app
{
    internal class WorkflowStep
    {
        public string Status { get; private set; }
        public string Feedback { get; private set; }
        public string Description { get; private set; }
        public Guid RoleID { get; private set; }
        public Guid EmployeeID { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public int NumberStep { get; private set; }

    }
}