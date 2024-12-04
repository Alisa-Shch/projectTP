namespace Domain
{
    internal class CandidateWorkflowStep
    {
        public Guid UserId { get; }
        public Guid RoleId { get; }
        public string? Description { get; private set; }
        public Status Status { get; private set; }
        public string? Comment { get; private set; }
        public int NumberStep { get; }

        public void Approve(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUser(userId);
            Status = Status.Approved;
            Comment = comment;
        }

        public void Reject(Guid userId, string comment)
        {
            ArgumentNullException.ThrowIfNull(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(comment));

            CheckUser(userId);
            Status = Status.Rejected;
            Comment = comment;
        }

        public void Restart()
        {
            Status = Status.InProgress;
            Comment = null;
        }

        public void CheckUser(Guid userId)
        {
            if (userId != UserId || Status != Status.InProgress)
            {
                throw new Exception("exception");
            }
        }
    }
}