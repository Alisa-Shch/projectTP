namespace Domain.Tests
{
    public class DocumentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is CandidateDocument)
            {
                var name = context.Create<string>();
                var workExperience = context.Create<string>();

                return CandidateDocument.Create(name, workExperience);
            }
            return new NoSpecimen();
        }
    }
}