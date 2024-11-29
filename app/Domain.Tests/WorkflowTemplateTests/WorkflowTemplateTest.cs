namespace Domain.Tests
{
    internal class WorkflowTemplateTest
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }

        private WorkflowTemplate(Guid id, string name, string description, IReadOnlyCollection<WorkflowStep> steps)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

            if (steps == null || !steps.Any())
                throw new ArgumentException("Steps cannot be null or empty.", nameof(steps));

            Id = id;
            Name = name;
            Description = description;
            Steps = steps.ToList().AsReadOnly();
        }

        public static WorkflowTemplate Create(string name, string description, IReadOnlyCollection<WorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

            if (steps == null || !steps.Any())
                throw new ArgumentException("Steps cannot be null or empty.", nameof(steps));

            return new WorkflowTemplate(Guid.NewGuid(), name, description, steps);
        }

        public Workflow CreateWorkflow(Guid candidateId, Guid invitingId)
        {
            if (candidateId == Guid.Empty)
                throw new ArgumentException("CandidateId cannot be empty.", nameof(candidateId));

            if (invitingId == Guid.Empty)
                throw new ArgumentException("InvitingId cannot be empty.", nameof(invitingId));

            return Workflow.Create(this, candidateId, invitingId);
        }
    }
}