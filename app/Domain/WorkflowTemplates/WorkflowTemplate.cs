namespace Domain
{
    public class WorkflowTemplate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyCollection<WorkflowTemplateStep> Steps { get; private set; }

        private WorkflowTemplate(Guid id, string name, string description, IReadOnlyCollection<WorkflowTemplateStep> steps)
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

        public static WorkflowTemplate Create(string name, string description, IReadOnlyCollection<WorkflowTemplateStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            return new(Guid.NewGuid(), name, description, steps);
        }

        public CandidateWorkflow Create()
        {
            return CandidateWorkflow.Create(this);
        }
    }
}