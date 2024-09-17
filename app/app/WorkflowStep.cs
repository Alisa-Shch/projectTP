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

        WorkflowStep(Guid roleID, string description)
        {
            Status = Position.InProgress;
            Description = description;
            RoleID = roleID;
            ModifiedDate = DateTime.Now;
        }

        public void Approve(Employers employer, string feedback)
        {
            Workflow.Approve(employer, feedback);
            WorkflowTemplate.Update();
        }

        public void Reject(Employers employer, string feedback)
        {
            Workflow.Reject(employer, feedback);
            WorkflowTemplate.Update();
        }
    }
}