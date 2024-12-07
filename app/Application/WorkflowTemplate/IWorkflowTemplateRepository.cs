using Domain;

namespace Application
{
    internal interface IWorkflowTemplateRepository
    {
        Task<WorkflowTemplate> GetById(Guid id, CancellationToken cancellationToken);
    }
}