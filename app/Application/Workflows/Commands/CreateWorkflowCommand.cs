namespace Application
{
    public class CreateWorkflowCommand : IRequest<Guid>
    {
        public Guid UserReferaleId { get; private set; }
        public Guid WorkflowTemplateId { get; private set; }
        public Document Document { get; private set; }

        public CreateWorkflowCommand(Guid userReferaleId, Guid workflowTemplateId, Document document)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(userReferaleId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(workflowTemplateId));
            ArgumentNullException.ThrowIfNull(nameof(document));

            UserReferaleId = userReferaleId;
            WorkflowTemplateId = workflowTemplateId;
            Document = document;
        }
    }
}