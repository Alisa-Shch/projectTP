﻿namespace Application
{
    public interface ICandidateWorkflowRepository
    {
        Task<IReadOnlyCollection<Domain.CandidateWorkflow>> GetByUserId(Guid userId, bool isOpenOnly, CancellationToken cancellationToken);

        Task Create(Candidate candidate, CancellationToken cancellationToken);
    }
}