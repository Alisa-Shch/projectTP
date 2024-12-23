namespace Domain.Tests
{
    public class EmployeeBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Employee)
            {
                var roleId = context.Create<Guid>();
                var name = context.Create<string>();

                return Employee.Create(roleId, name);
            }
            return new NoSpecimen();
        }
    }
}