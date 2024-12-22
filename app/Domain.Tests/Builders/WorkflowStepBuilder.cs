namespace Domain.Tests
{
    public class WorkflowStepBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is CandidateWorkflowStep)
            {
                var templateStep = context.Create<WorkflowTemplateStep>();

                return CandidateWorkflowStep.Create(templateStep);
            }
            return new NoSpecimen();
        }
    }
}