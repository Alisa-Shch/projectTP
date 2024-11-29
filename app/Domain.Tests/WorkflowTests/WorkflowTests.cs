using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    internal class WorkflowTests
    {
        [Test]
        public void Create_ValidData_ShouldCreateWorkflow()
        {
            var template = new WorkflowTemplate(Guid.NewGuid(), "Test Template", "Description", new List<WorkflowStep>());
            var invitingId = Guid.NewGuid();
            var candidateId = Guid.NewGuid();

            var workflow = Workflow.Create(template, invitingId, candidateId);

            workflow.Should().NotBeNull();
            workflow.Id.Should().NotBeEmpty();
            workflow.InvitingId.Should().Be(invitingId);
            workflow.CandidateId.Should().Be(candidateId);
        }

        [Test]
        public void Approve_InvalidWorkflow_ShouldThrowException()
        {
            var workflow = GetSampleWorkflow();
            var user = new Employers(Guid.NewGuid(), "Test User");

            workflow.Approve(user, "Approval message");
        }

        [Test]
        public void Reject_CompletedWorkflow_ShouldThrowInvalidOperationException()
        {
            var workflow = GetSampleWorkflow();
            var user = new Employers(Guid.NewGuid(), "Test User");

            workflow.Reject(user, "Rejection reason");

            Assert.Throws<InvalidOperationException>(() => workflow.Reject(user, "Rejection reason"));
        }

        private Workflow GetSampleWorkflow()
        {
            var template = WorkflowTemplate.Create("Template", "Description", new List<WorkflowStep>
            {
                WorkflowStep.Create("Step 1", Status.InProgress),
                WorkflowStep.Create("Step 2", Status.Pending)
            });

            return Workflow.Create(template, Guid.NewGuid(), Guid.NewGuid());
        }
    }
}