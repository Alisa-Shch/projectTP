namespace Domain
{
    public class WorkflowTemplate
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public List<WorkflowTemplateStep> Steps { get; }

        private WorkflowTemplate(Guid id, string name, string description, List<WorkflowTemplateStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            Id = id;
            Name = name;
            Description = description;
            Steps = steps;
        }

        public static WorkflowTemplate Create(string name, string description, List<WorkflowTemplateStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            return new(Guid.NewGuid(), name, description, steps);
        }

        public CandidateWorkflow Create(Guid candidateId, Guid invitingId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingId));

            return CandidateWorkflow.Create(this, candidateId, invitingId);
        }
    }
}