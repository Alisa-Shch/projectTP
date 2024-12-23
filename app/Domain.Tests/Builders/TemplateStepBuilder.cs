namespace Domain.Tests
{
    public class TemplateStepBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is WorkflowTemplateStep)
            {
                var name = context.Create<string>();
                var description = context.Create<string>();
                var employeeId = context.Create<Guid>();
                var roleId = context.Create<Guid>();

                return WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            }
            return new NoSpecimen();
        }
    }
}