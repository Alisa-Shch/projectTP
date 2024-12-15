using System.Xml.Linq;

namespace Domain.Tests
{
    [TestFixture]
    public class WorkflowStepTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateWorkflowStep()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as CandidateWorkflowStep;

            step.Should().NotBeNull();
            step.Description.Should().NotBeNullOrEmpty();
            step.RoleId.Should().NotBeEmpty();
            step.EmployeeId.Should().NotBeEmpty();
            step.NumberStep.Should().BeGreaterThan(0);
            step.Status.Should().Be(Status.InProgress);
        }

        [Test]
        public void Approve_ValidUser_ShouldChangeStatusToApproved()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as CandidateWorkflowStep;
            var user = Employers.Create(_fixture.Create<Guid>(), _fixture.Create<string>());

            step.Approve(user.Id, "Approved!");

            step.Status.Should().Be(Status.Approved);
            step.Comment.Should().Be("Approved!");
        }

        [Test]
        public void Approve_InvalidUser_ShouldThrowUnauthorizedAccessException()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as CandidateWorkflowStep;
            var user = Employers.Create(_fixture.Create<Guid>(), _fixture.Create<string>());

            step.Invoking(x => x.Approve(user.Id, "Trying to approve")).Should().Throw<UnauthorizedAccessException>();
        }

        [Test]
        public void Restart_StepAlreadyInProgress_ShouldThrowInvalidOperationException()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as CandidateWorkflowStep;

            step.Invoking(x => x.Restart()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Reject_ValidUser_ShouldChangeStatusToRejected()
        {
            var step = new StepBuilder().Create(typeof(CandidateWorkflowStep), (ISpecimenContext)_fixture) as CandidateWorkflowStep;
            var user = Employers.Create(_fixture.Create<Guid>(), _fixture.Create<string>());

            step.Reject(user.Id, "Rejected!");

            step.Status.Should().Be(Status.Rejected);
            step.Comment.Should().Be("Rejected!");
        }
    }
}