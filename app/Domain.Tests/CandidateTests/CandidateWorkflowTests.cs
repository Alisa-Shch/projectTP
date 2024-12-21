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
        public void Create_ValidData_ShouldCreateWorkflow()
        {
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template);

            workflow.Should().NotBeNull();
            workflow.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Approve_InvalidWorkflow_ShouldThrowException()
        {
            var workflow = GetSampleWorkflow();
            var user = Employee.Create(_fixture.Create<Guid>(), _fixture.Create<string>());

            var candidate = CandidateBuilder.Create(_fixture);

            candidate.Invoking(x => x.Approve(user, "Approval message")).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Reject_CompletedWorkflow_ShouldThrowInvalidOperationException()
        {
            var workflow = GetSampleWorkflow();
            var user = Employee.Create(_fixture.Create<Guid>(), _fixture.Create<string>());

            var candidate = CandidateBuilder.Create(_fixture);

            candidate.Reject(user, "Rejection reason");
            candidate.Invoking(x => x.Reject(user, "Rejection reason")).Should().Throw<InvalidOperationException>();
        }

        private CandidateWorkflow GetSampleWorkflow()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            List<WorkflowTemplateStep> steps =
            [
                WorkflowTemplateStep.Create(name, description, employeeId, roleId),
                WorkflowTemplateStep.Create(name, description, employeeId, roleId)
            ];

            var template = WorkflowTemplate.Create("Template", "Description", new List<WorkflowTemplateStep>(steps));

            return CandidateWorkflow.Create(template);
        }
    }
}