namespace Application
{
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, Guid>
    {
        private readonly ICandidateWorkflowRepository _workflowRepository;
        private readonly IWorkflowTemplateRepository _workflowTemplateRepository;

        public CreateWorkflowCommandHandler(IWorkflowTemplateRepository workflowTemplateRepository, ICandidateWorkflowRepository workflowRepository)
        {
            _workflowTemplateRepository = workflowTemplateRepository;
            _workflowRepository = workflowRepository;
        }

        public async Task<Guid> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var workflowTemplate = await _workflowTemplateRepository.GetById(request.WorkflowTemplateId, cancellationToken).ConfigureAwait(false);

            var workflow = Domain.WorkflowTemplate.Create(request.Document.Name, request.Document.WorkExperience, []);
            var candidate = new Candidate(request.UserReferaleId, workflow.Name);
            await _workflowRepository.Create(candidate, cancellationToken).ConfigureAwait(false);

            return candidate.Id;
        }
    }
}