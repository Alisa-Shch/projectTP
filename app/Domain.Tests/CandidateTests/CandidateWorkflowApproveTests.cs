namespace Domain.Tests
{
    [TestFixture]
    public class CandidateWorkflowApproveTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void CreateWorkflow()
        {
            _fixture.Customize<CandidateWorkflowStep>(_ => new StepBuilder());
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;

            template.Should().NotBeNull();
            template.Should().BeOfType<WorkflowTemplate>();

            var workflow = CandidateWorkflow.Create(template);

            workflow.Should().NotBeNull();
            workflow.Steps.Should().BeEquivalentTo(template.Steps);
        }

        [Test]
        public void CreateWorkflowWithNullStep()
        {
            _fixture.Customize<CandidateWorkflowStep>(_ => new StepBuilder());
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;

            template.Should().NotBeNull();
            template.Should().BeOfType<WorkflowTemplate>();

            FluentActions.Invoking(() => CandidateWorkflow.Create(template)).Should().Throw<ArgumentException>().WithMessage("*steps*");
        }
    }
}