using static app.Workflow;

namespace app
{
    internal class WorkflowTemplate
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid ID { get; private set; }

        public List<WorkflowStep> WSteps = new();

        public WorkflowTemplate(string name, string description)
        {
            Name = name;
            Description = description;
            ID = Guid.NewGuid();
        }

        public void Update()
        {
            WSteps.ModifiedDate = DateTime.Now;
            bool isRejectSteps = WSteps.Where(x => x.Status == Position.Reject).Any();
            Workflow.Status = isRejectSteps ? Position.Reject : Position.InProgress;
        }

        public Workflow Create(Guid candidateId, Guid invitingEID)
        {
            return null;
        }
    }
}