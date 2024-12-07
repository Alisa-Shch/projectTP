using Domain;

namespace Application
{
    internal interface IWorkflowRepository
    {
        Task<IReadOnlyCollection<CandidateWorkflow>> GetByUserId(Guid userId, bool isOpenOnly, CancellationToken cancellationToken);
        Task Create(Candidate candidate, CancellationToken cancellationToken);
    }
}