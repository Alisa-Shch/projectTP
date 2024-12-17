using Domain;

namespace Application
{
    public interface ICandidateWorkflowRepository
    {
        Task<IReadOnlyCollection<CandidateWorkflow>> GetByUserId(Guid userId, bool isOpenOnly, CancellationToken cancellationToken);

        Task Create(Candidate candidate, CancellationToken cancellationToken);
    }
}