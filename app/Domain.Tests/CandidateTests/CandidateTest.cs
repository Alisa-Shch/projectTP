namespace Domain.Tests
{
    [TestFixture]
    public class CandidateTests
    {
        private Fixture? _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateCandidate()
        {
            var document = _fixture.Create<CandidateDocument>();
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template);
            var candidate = Candidate.Create(workflow, document);

            candidate.Should().NotBeNull();
            candidate.Document.Should().Be(document);
            candidate.Workflow.Should().NotBeNull();
        }

        [Test]
        public void Create_NullDocument_ShouldThrowArgumentException()
        {
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template);

            Action act = () => Candidate.Create(workflow, null!);

            act.Should().Throw<ArgumentNullException>().WithMessage("*document*");
        }
    }
}