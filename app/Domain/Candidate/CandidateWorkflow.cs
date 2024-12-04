namespace Domain
{
    internal class CandidateWorkflow
    {
        public Status Status { get; private set; }
        public string? Comment { get; private set; }
        public IReadOnlyCollection<CandidateWorkflowStep> Steps { get; }

        public void Approve(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("exception");
            }
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Approve(userId, comment);
        }

        public void Reject(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckSteps();
            var step = Steps.OrderBy(x => x.NumberStep).First(x => x.Status == Status.InProgress);
            step.Reject(userId, comment);
        }

        public void Restart()
        {
            CheckSteps();
            foreach (var step in Steps)
            {
                step.Restart();
            }
        }

        public void CheckSteps()
        {
            if (Steps.All(x => x.Status == Status.Approved) || Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("exception");
            }
        }
    }
}