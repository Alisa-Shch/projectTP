namespace Domain.Tests
{
    public class StepBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is CandidateWorkflowStep)
            {
                var templateStep = WorkflowTemplateStep.Create(context.Create<string>(), context.Create<string>(), context.Create<Guid>(), context.Create<Guid>());

                return CandidateWorkflowStep.Create(templateStep);
            }
            return new NoSpecimen();
        }
    }
}