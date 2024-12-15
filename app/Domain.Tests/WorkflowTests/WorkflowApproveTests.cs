namespace Domain.Tests
{
    public class WorkflowApproveTests
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
            var employeeId = _fixture.Create<Guid>();
            var candidateId = _fixture.Create<Guid>();
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;

            template.Should().NotBeNull();
            template.Should().BeOfType<WorkflowTemplate>();

            var workflow = CandidateWorkflow.Create(template, employeeId, candidateId);

            workflow.Should().NotBeNull();
            workflow.EmployeeId.Should().Be(employeeId);
            workflow.CandidateId.Should().Be(candidateId);
            workflow.Steps.Should().BeEquivalentTo(template.Steps);
        }

        [Test]
        public void CreateWorkflowWithNullStep()
        {
            _fixture.Customize<CandidateWorkflowStep>(_ => new StepBuilder());
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var employeeId = _fixture.Create<Guid>();
            var candidateId = _fixture.Create<Guid>();

            template.Should().NotBeNull();
            template.Should().BeOfType<WorkflowTemplate>();

            FluentActions.Invoking(() => CandidateWorkflow.Create(template, employeeId, candidateId)).Should().Throw<ArgumentException>().WithMessage("*steps*");
        }
    }
}