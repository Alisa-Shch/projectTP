namespace Application
{
    public class CreateWorkflowCommand : IRequest<Guid>
    {
        public Guid UserReferaleId { get; private set; }
        public Guid WorkflowTemplateId { get; private set; }
        public Document Document { get; private set; }
    }
}