namespace Domain.Tests
{
    internal class TemplateBuilder : ISpecimenBuilder
    {
        private Fixture _fixture = new();

        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(WorkflowTemplate).Equals(request))
            {
                return new NoSpecimen();
            }
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture);

            return WorkflowTemplate.Create(context.Create<string>(), context.Create<string>(), (List<WorkflowTemplateStep>)step);
        }
    }
}