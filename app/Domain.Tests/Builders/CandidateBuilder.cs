namespace Domain.Tests
{
    public class CandidateBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Candidate)
            {
                var vacancyId = context.Create<Guid>();
                var referralId = context.Create<Guid>();
                var document = context.Create<CandidateDocument>();
                var workflow = context.Create<CandidateWorkflow>();

                return Candidate.Create(vacancyId, referralId, document, workflow);
            }
            return new NoSpecimen();
        }
    }
}