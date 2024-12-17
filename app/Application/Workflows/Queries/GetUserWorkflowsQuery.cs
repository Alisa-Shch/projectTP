using Domain;

namespace Application
{
    public class GetUserWorkflowsQuery : IRequest<IReadOnlyCollection<CandidateWorkflow>>
    {
        public Guid UserId { get; private set; }
        public bool IsOpenOnly { get; private set; }

        public GetUserWorkflowsQuery(Guid userId, bool isOpenOnly)
        {
            UserId = userId;
            IsOpenOnly = isOpenOnly;
        }
    }
}