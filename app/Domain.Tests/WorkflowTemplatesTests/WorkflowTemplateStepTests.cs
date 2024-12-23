namespace Domain.Tests
{
    [TestFixture]
    public class WorkflowTemplateStepTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateWorkflowTemplateStep()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            var workflowTemplateStep = WorkflowTemplateStep.Create(name, description, employeeId, roleId);

            workflowTemplateStep.Should().NotBeNull();
            workflowTemplateStep.Name.Should().Be(name);
            workflowTemplateStep.Description.Should().Be(description);
            workflowTemplateStep.EmployeeId.Should().Be(employeeId);
            workflowTemplateStep.RoleId.Should().Be(roleId); 
            workflowTemplateStep.NumberStep.Should().NotBe(0);
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            string name = null;
            var description = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var name = string.Empty;
            var description = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullDescription_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            string description = null;
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*description*");
        }

        [Test]
        public void Create_EmptyDescription_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = string.Empty;
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*description*");
        }

        [Test]
        public void Create_EmptyEmployeeId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var employeeId = Guid.Empty;
            var roleId = _fixture.Create<Guid>();

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*employeeId*");
        }

        [Test]
        public void Create_EmptyRoleId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = Guid.Empty;

            Action act = () => WorkflowTemplateStep.Create(name, description, employeeId, roleId);
            act.Should().Throw<ArgumentException>().WithMessage("*roleId*");
        }
    }
}