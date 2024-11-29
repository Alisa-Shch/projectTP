using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class WorkflowStepTest
    {
        [Test]
        public void Create_ValidData_ShouldCreateWorkflowStep()
        {
            var description = "Test Step";
            var roleId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var numberStep = 1;

            var step = WorkflowStep.Create(description, roleId, employeeId, numberStep);

            step.Should().NotBeNull();
            step.Description.Should().Be(description);
            step.RoleId.Should().Be(roleId);
            step.EmployeeId.Should().Be(employeeId);
            step.NumberStep.Should().Be(numberStep);
            step.Status.Should().Be(Status.InProgress);
        }

        [Test]
        public void Approve_ValidUser_ShouldChangeStatusToApproved()
        {
            var step = WorkflowStep.Create("Test Step", Guid.NewGuid(), Guid.NewGuid(), 1);
            var user = new Employers(step.EmployeeId, "Test User");

            step.Approve(user, "Approved!");

            step.Status.Should().Be(Status.Approved);
            step.Message.Should().Be("Approved!");
        }

        [Test]
        public void Approve_InvalidUser_ShouldThrowUnauthorizedAccessException()
        {
            var step = WorkflowStep.Create("Test Step", Guid.NewGuid(), Guid.NewGuid(), 1);
            var user = new Employers(Guid.NewGuid(), "Unauthorized User");

            Assert.Throws<UnauthorizedAccessException>(() => step.Approve(user, "Trying to approve"));
        }

        [Test]
        public void Restart_StepAlreadyInProgress_ShouldThrowInvalidOperationException()
        {
            var step = WorkflowStep.Create("Test Step", Guid.NewGuid(), Guid.NewGuid(), 1);

            Assert.Throws<InvalidOperationException>(() => step.Restart());
        }

        [Test]
        public void Reject_ValidUser_ShouldChangeStatusToRejected()
        {
            var step = WorkflowStep.Create("Test Step", Guid.NewGuid(), Guid.NewGuid(), 1);
            var user = new Employers(step.EmployeeId, "Test User");

            step.Reject(user, "Rejected!");

            step.Status.Should().Be(Status.Rejected);
            step.Message.Should().Be("Rejected!");
        }
    }
}