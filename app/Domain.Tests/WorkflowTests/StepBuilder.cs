using AutoFixture.Kernel;

namespace Domain.Tests
{
    public class StepBuilder : ISpecimenBuilder
    {
        private readonly bool _isUser;

        public StepBuilder(bool isUser)
        {
            _isUser = isUser;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(Step).Equals(request))
            {
                return new NoSpecimen();
            }
            return new Step(context.Create<int>(), context.Create<Status>(), _isUser ? context.Create<Guid>() : null, _isUser ? null : context.Create<Guid>(), null);
        }
    }
}