namespace Domain.Tests
{
    [TestFixture]
    public class WorkflowTemplateTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateWorkflowTemplate()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var steps = _fixture.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

            var workflowTemplate = WorkflowTemplate.Create(name, description, steps);

            workflowTemplate.Should().NotBeNull();
            workflowTemplate.Name.Should().Be(name);
            workflowTemplate.Description.Should().Be(description);
            workflowTemplate.Steps.Should().NotBeEmpty();
            workflowTemplate.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            string name = null;
            var description = _fixture.Create<string>();
            var steps = _fixture.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

            Action act = () => WorkflowTemplate.Create(name, description, steps);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var name = string.Empty;
            var description = _fixture.Create<string>();
            var steps = _fixture.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

            Action act = () => WorkflowTemplate.Create(name, description, steps);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullDescription_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            string description = null;
            var steps = _fixture.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

            Action act = () => WorkflowTemplate.Create(name, description, steps);
            act.Should().Throw<ArgumentException>().WithMessage("*description*");
        }

        [Test]
        public void Create_EmptyDescription_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = string.Empty;
            var steps = _fixture.Create<IReadOnlyCollection<WorkflowTemplateStep>>();

            Action act = () => WorkflowTemplate.Create(name, description, steps);
            act.Should().Throw<ArgumentException>().WithMessage("*description*");
        }

        [Test]
        public void Create_NullSteps_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            IReadOnlyCollection<WorkflowTemplateStep> steps = null;

            Action act = () => WorkflowTemplate.Create(name, description, steps);
            act.Should().Throw<ArgumentException>().WithMessage("*steps*");
        }

        [Test]
        public void Create_ValidCandidateWorkflow_ShouldCreateWorkflowTemplate()
        {
            var workflowTemplate = _fixture.Create<WorkflowTemplate>();

            var candidateWorkflow = workflowTemplate.Create();

            candidateWorkflow.Should().NotBeNull();
            candidateWorkflow.Id.Should().NotBeEmpty();
            candidateWorkflow.TemplateId.Should().NotBeEmpty();
            candidateWorkflow.Steps.Should().NotBeEmpty();
        }
    }
}