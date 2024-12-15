namespace Domain.Tests
{
    public class WorkflowTemplateStepTest
    {
        private Fixture? _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateWorkflowTemplateStep()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as WorkflowTemplateStep;

            step.Should().NotBeNull();
            step.Description.Should().NotBeNullOrEmpty();
            step.EmployeeId.Should().NotBeEmpty();
            step.RoleId.Should().NotBeEmpty();
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var description = _fixture.Create<string>();
            var employeeId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            Action act = () => WorkflowTemplateStep.Create(string.Empty, description, employeeId, roleId);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullDescription_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var employeeId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            Action act = () => WorkflowTemplateStep.Create(name, null!, employeeId, roleId);

            act.Should().Throw<ArgumentException>().WithMessage("*description*");
        }

        [Test]
        public void Create_EmptyEmployeeId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var roleId = Guid.NewGuid();

            Action act = () => WorkflowTemplateStep.Create(name, description, Guid.Empty, roleId);

            act.Should().Throw<ArgumentException>().WithMessage("*EmployeeId*");
        }

        [Test]
        public void Create_EmptyRoleId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var employeeId = Guid.NewGuid();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, Guid.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*RoleId*");
        }
    }
}