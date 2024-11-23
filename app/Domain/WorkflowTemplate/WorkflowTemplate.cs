namespace Domain
{
    internal class WorkflowTemplate
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }        

        private WorkflowTemplate(Guid id, string name, string description,IReadOnlyCollection<WorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            Id = id;
            Name = name;
            Description = description;
            Steps = steps.ToList().AsReadOnly();
        }

        public WorkflowTemplate Create(string name, string description, IReadOnlyCollection<WorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            return new WorkflowTemplate(Guid.NewGuid(), name, description,steps);
        }

        public Workflow Create(Guid candidateId, Guid invitingId)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingId));

            return Workflow.Create(this, candidateId, invitingId);
        }
    }
}