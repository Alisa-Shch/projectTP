namespace Domain.Tests
{
    public class WorkflowBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is CandidateWorkflow)
            {
                var step = context.Create<CandidateWorkflowStep>();
                var template = WorkflowTemplate.Create(context.Create<string>(), context.Create<string>(), (IReadOnlyCollection<WorkflowTemplateStep>)step);

                return CandidateWorkflow.Create(template);
            }
            return new NoSpecimen();
        }
    }
}