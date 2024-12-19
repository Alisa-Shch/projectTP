namespace Domain.Tests
{
    public class CandidateBuilder
    {
        public static Candidate Create(Fixture fixture)
        {
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template);

            string name = fixture.Create<string>();
            string workExperience = fixture.Create<string>();

            return Candidate.Create(workflow, CandidateDocument.Create(name, workExperience));
        }
    }
}