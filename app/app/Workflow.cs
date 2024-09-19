namespace app
{
    internal class Workflow
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TemplateID { get; private set; }
        public Guid CandidateId { get; private set; }
        public Guid InvitingEID { get; private set; }
        public Position Status { get; private set; }
        public Guid ID { get; private set; }

        public List<WorkflowStep> WSteps = new();

        public Workflow(WorkflowTemplate template, Guid candidateId, Guid invitingEID)
        {
            Name = template.Name;
            Description = template.Description;
            TemplateID = template.ID;
            CandidateId = candidateId;
            InvitingEID = invitingEID;
            ID = Guid.NewGuid();
        }

        public void Approve(Employers employer, string feedback)
        {
            InvitingEID = employer.ID;
            WSteps.Feedback = feedback;
            WSteps.ModifiedDate = DateTime.Now;
            Status = Position.Approved;
        }

        public void Reject(Employers employer, string feedback)
        {
            InvitingEID = employer.ID;
            WSteps.Feedback = feedback;
            WSteps.ModifiedDate = DateTime.Now;
            Status = Position.Reject;
        }

        public void Restart(Employers employer)
        {
            InvitingEID = employer.ID;
            WSteps.ModifiedDate = DateTime.Now;
            Status = Position.InProgress;
        }

        public enum Position
        {
            Reject,
            InProgress,
            Approved
        }
    }
}