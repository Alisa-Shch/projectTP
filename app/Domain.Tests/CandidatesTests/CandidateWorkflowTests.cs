namespace Domain.Tests
{
    [TestFixture]
    public class CandidateWorkflowTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidTemplate_ShouldCreateWorkflow()
        {
            var template = _fixture.Create<WorkflowTemplate>();

            var workflow = CandidateWorkflow.Create(template);

            workflow.Should().NotBeNull();
            workflow.TemplateId.Should().Be(template.Id);
            workflow.Id.Should().NotBeEmpty();
            workflow.Steps.Should().NotBeEmpty();
        }

        [Test]
        public void Create_NullTemplate_ShouldThrowArgumentException()
        {
            WorkflowTemplate template = null;

            Action act = () => CandidateWorkflow.Create(template);
            act.Should().Throw<ArgumentException>().WithMessage("*template*");
        }


    }
}