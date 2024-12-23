namespace Domain.Tests
{
    public class TemplateBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is WorkflowTemplate)
            {
                var name = context.Create<string>();
                var description = context.Create<string>();
                var step = context.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

                return WorkflowTemplate.Create(name, description, step);
            }
            return new NoSpecimen();
        }
    }
}