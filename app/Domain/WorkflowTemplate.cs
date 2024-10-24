namespace Domain
{
    internal class WorkflowTemplate
    {
        public string Name { get; }
        public string Description { get; }
        public Guid ID { get; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }        

        private WorkflowTemplate(string name, string description)
        {
            Name = name;
            Description = description;
            ID = Guid.NewGuid();
        }

        public Workflow Create(Guid candidateId, Guid invitingEID)
        {
            return Workflow.Create(new WorkflowTemplate(Name, Description));
        }

        public void Update()
        {
            /*
            Steps.ModifiedDate = DateTime.Now;
            bool isRejectSteps = Steps.Where(x => x.Status == Status.Rejected).Any();
            Steps.Status = isRejectSteps ? Status.Rejected : Status.InProgress;
            */
        }
    }
}