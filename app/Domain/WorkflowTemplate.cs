using System.Xml.Linq;

namespace Domain
{
    internal class WorkflowTemplate
    {
        public string Name { get; }
        public string Description { get; }
        public Guid ID { get; }
        public IReadOnlyCollection<WorkflowStep> Steps { get; }        

        private WorkflowTemplate(string name, string description, Guid id, IReadOnlyCollection<WorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            Name = name;
            Description = description;
            ID = id;
            Steps = steps.ToList().AsReadOnly();
        }

        public WorkflowTemplate Create(string name, string description, IReadOnlyCollection<WorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));
            return new WorkflowTemplate(name, description, Guid.NewGuid(), steps);
        }

        public Workflow Create(Guid candidateId, Guid invitingEID)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(candidateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(invitingEID));
            return Workflow.Create(this, candidateId, invitingEID);
        }
    }
}