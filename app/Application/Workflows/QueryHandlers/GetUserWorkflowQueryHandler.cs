using Domain;

namespace Application
{
    public class GetUserWorkflowQueryHandler : IRequestHandler<GetUserWorkflowsQuery, IReadOnlyCollection<CandidateWorkflow>>
    {
        private readonly ICandidateWorkflowRepository _workflowRepository;

        public GetUserWorkflowQueryHandler(ICandidateWorkflowRepository workflowRepository)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
        }

        public async Task<IReadOnlyCollection<CandidateWorkflow>> Handle(GetUserWorkflowsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var workflows = await _workflowRepository.GetByUserId(request.UserId, request.IsOpenOnly, cancellationToken).ConfigureAwait(false);

            return workflows.Select(w => CandidateWorkflow.Create(WorkflowTemplate.Create("", w.Description, []), w.EmployeeId, w.CandidateId)).ToArray();
        }
    }
}