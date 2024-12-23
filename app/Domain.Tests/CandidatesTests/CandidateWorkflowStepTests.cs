namespace Domain.Tests
{
    [TestFixture]
    public class CandidateWorkflowStepTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidTemplateStep_ShouldCreateWorkflowStep()
        {
            var templateStep = _fixture.Create<WorkflowTemplateStep>();

            var workflowStep = CandidateWorkflowStep.Create(templateStep);

            workflowStep.Should().NotBeNull();

        }

        [Test]
        public void Create_NullTemplateStep_ShouldThrowArgumentException()
        {
            WorkflowTemplateStep templateStep = null;

            Action act = () => CandidateWorkflowStep.Create(templateStep);
            act.Should().Throw<ArgumentException>().WithMessage("*templateStep*");
        }


        [Test]
        public void Approve_Valid_ShouldApproveCandidate()
        {
            var user = _fixture.Create<Employee>();
            var comment = _fixture.Create<string>();

            var templateStep = WorkflowTemplateStep.Create(
                name: _fixture.Create<string>(),
                description: _fixture.Create<string>(),
                employeeId: user.Id,
                roleId: _fixture.Create<Guid>());

            var template = WorkflowTemplate.Create(
                name: _fixture.Create<string>(),
                description: _fixture.Create<string>(),
                steps: [templateStep]);

            var workflow = CandidateWorkflow.Create(template);

            var candidate = Candidate.Create(
                vacancyId: _fixture.Create<Guid>(),
                referralId: _fixture.Create<Guid>(),
                document: _fixture.Create<CandidateDocument>(),
                workflow);

            candidate.Approve(user, comment);

            foreach (var step in candidate.Workflow.Steps)
            {
                step.Status.Should().Be(Status.Approved);
                step.Comment.Should().Be(comment);
            }
        }

        [Test]
        public void Approve_NullUser_ShouldThrowArgumentException()
        {
            Employee user = null;
            var comment = _fixture.Create<string>();

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Approve(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*user*");
        }

        [Test]
        public void Approve_NullComment_ShouldThrowArgumentException()
        {
            var user = _fixture.Create<Employee>();
            string comment = null;

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Approve(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*comment*");
        }

        [Test]
        public void Approve_EmptyComment_ShouldThrowArgumentException()
        {
            var user = _fixture.Create<Employee>();
            var comment = string.Empty;

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Approve(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*comment*");
        }

        [Test]
        public void Reject_Valid_ShouldRejectCandidate()
        {
            var user = _fixture.Create<Employee>();
            var comment = _fixture.Create<string>();

            var templateStep = WorkflowTemplateStep.Create(
                name: _fixture.Create<string>(),
                description: _fixture.Create<string>(),
                employeeId: user.Id,
                roleId: _fixture.Create<Guid>());

            var template = WorkflowTemplate.Create(
                name: _fixture.Create<string>(),
                description: _fixture.Create<string>(),
                steps: [templateStep]);

            var workflow = CandidateWorkflow.Create(template);

            var candidate = Candidate.Create(
                vacancyId: _fixture.Create<Guid>(),
                referralId: _fixture.Create<Guid>(),
                document: _fixture.Create<CandidateDocument>(),
                workflow);

            candidate.Reject(user, comment);

            foreach (var step in candidate.Workflow.Steps)
            {
                step.Status.Should().Be(Status.Rejected);
                step.Comment.Should().Be(comment);
            }
        }

        [Test]
        public void Reject_NullUser_ShouldThrowArgumentException()
        {
            Employee user = null;
            var comment = _fixture.Create<string>();

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Reject(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*user*");
        }

        [Test]
        public void Reject_NullComment_ShouldThrowArgumentException()
        {
            var user = _fixture.Create<Employee>();
            string comment = null;

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Reject(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*comment*");
        }

        [Test]
        public void Reject_EmptyComment_ShouldThrowArgumentException()
        {
            var user = _fixture.Create<Employee>();
            var comment = string.Empty;

            var candidate = _fixture.Create<Candidate>();

            Action act = () => candidate.Reject(user, comment);
            act.Should().Throw<ArgumentException>().WithMessage("*comment*");
        }

        [Test]
        public void Restart_Valid_ShouldRestartCandidate()
        {
            var candidate = _fixture.Create<Candidate>();

            candidate.Restart();

            foreach (var step in candidate.Workflow.Steps)
            {
                step.Status.Should().Be(Status.InProgress);
                step.Comment.Should().Be(null);
            }
        }
    }
}