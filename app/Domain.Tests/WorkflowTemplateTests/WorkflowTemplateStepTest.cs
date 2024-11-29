using AutoFixture;
using NUnit.Framework;

namespace Domain.Tests
{
    public class WorkflowTemplateStepTest
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
            var employeeId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            var step = WorkflowTemplateStep.Create(name, description, employeeId, roleId);

            step.Should().NotBeNull();
            step.Id.Should().NotBeEmpty();
            step.Name.Should().Be(name);
            step.Description.Should().Be(description);
            step.EmployeeId.Should().Be(employeeId);
            step.RoleId.Should().Be(roleId);
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

            Action act = () => WorkflowTemplateStep.Create(name, null, employeeId, roleId);

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