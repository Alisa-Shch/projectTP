namespace Domain.Tests
{
    [TestFixture]
    public class CandidateWorkflowStepTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidTemplateStep_ShouldCreateWorkflowStep()
        {
            var templateStep = _fixture.Create<WorkflowTemplateStep>();

            var workflowStep = CandidateWorkflowStep.Create(templateStep);

            workflowStep.Should().NotBeNull();

        }

        [Test]
        public void Create_NullTemplateStep_ShouldThrowArgumentException()
        {
            WorkflowTemplateStep templateStep = null;

            Action act = () => CandidateWorkflowStep.Create(templateStep);
            act.Should().Throw<ArgumentException>().WithMessage("*templateStep*");
        }
    }
}