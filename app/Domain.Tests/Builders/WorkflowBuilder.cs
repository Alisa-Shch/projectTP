namespace Domain.Tests
{
    public class WorkflowBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is CandidateWorkflow)
            {
                var template = context.Create<WorkflowTemplate>();

                return CandidateWorkflow.Create(template);
            }
            return new NoSpecimen();
        }
    }
}