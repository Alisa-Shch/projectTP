using Domain;

namespace Application
{
    public interface IWorkflowTemplateRepository
    {
        Task<WorkflowTemplate> GetById(Guid id, CancellationToken cancellationToken);
    }
}