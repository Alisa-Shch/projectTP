namespace Application
{
    public class GetUserWorkflowsQuery : IRequest<IReadOnlyCollection<Domain.CandidateWorkflow>>
    {
        public Guid UserId { get; private set; }
        public bool IsOpenOnly { get; private set; }

        public GetUserWorkflowsQuery(Guid userId, bool isOpenOnly)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(userId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(isOpenOnly));

            UserId = userId;
            IsOpenOnly = isOpenOnly;
        }
    }
}