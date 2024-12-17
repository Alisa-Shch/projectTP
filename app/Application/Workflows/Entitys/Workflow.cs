namespace Application
{
    public class Workflow
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public Candidate Candidate { get; }
        public IReadOnlyCollection<Step> Steps { get; }

        public Workflow(Guid id, IReadOnlyCollection<Step> steps, DateTime createdAt, Candidate candidate)
        {
            Id = id;
            Steps = steps;
            CreatedAt = createdAt;
            Candidate = candidate;
        }
    }
}