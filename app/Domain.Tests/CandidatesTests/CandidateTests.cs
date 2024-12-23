namespace Domain.Tests
{
    [TestFixture]
    public class CandidateTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateCandidate()
        {
            var vacancyId = _fixture.Create<Guid>();
            var referralId = _fixture.Create<Guid>();
            var document = _fixture.Create<CandidateDocument>();
            var workflow = _fixture.Create<CandidateWorkflow>();  

            var candidate = Candidate.Create(vacancyId, referralId, document, workflow);

            candidate.Should().NotBeNull();
            candidate.VacancyId.Should().Be(vacancyId);
            candidate.ReferralId.Should().Be(referralId);
            candidate.Document.Should().Be(document);
            candidate.Workflow.Should().Be(workflow);
            candidate.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_EmptyVacancyId_ShouldThrowArgumentException()
        {
            var vacancyId = Guid.Empty;
            var referralId = _fixture.Create<Guid>();
            var document = CandidateDocument.Create(_fixture.Create<string>(), _fixture.Create<string>());
            var workflow = _fixture.Create<CandidateWorkflow>();

            Action act = () => Candidate.Create(vacancyId, referralId, document, workflow);
            act.Should().Throw<ArgumentException>().WithMessage("*vacancyId*");
        }

        [Test]
        public void Create_EmptyReferralId_ShouldThrowArgumentException()
        {
            var vacancyId = _fixture.Create<Guid>();
            var referralId = Guid.Empty;
            var document = CandidateDocument.Create(_fixture.Create<string>(), _fixture.Create<string>());
            var workflow = _fixture.Create<CandidateWorkflow>();

            Action act = () => Candidate.Create(vacancyId, referralId, document, workflow);
            act.Should().Throw<ArgumentException>().WithMessage("*referralId*");
        }

        [Test]
        public void Create_NullDocument_ShouldThrowArgumentException()
        {
            var vacancyId = _fixture.Create<Guid>();
            var referralId = _fixture.Create<Guid>();
            CandidateDocument document = null;
            var workflow = _fixture.Create<CandidateWorkflow>();

            Action act = () => Candidate.Create(vacancyId, referralId, document, workflow);
            act.Should().Throw<ArgumentException>().WithMessage("*document*");
        }

        [Test]
        public void Create_NullWorkflow_ShouldThrowArgumentException()
        {
            var vacancyId = _fixture.Create<Guid>();
            var referralId = _fixture.Create<Guid>();
            var document = CandidateDocument.Create(_fixture.Create<string>(), _fixture.Create<string>());
            CandidateWorkflow workflow = null;

            Action act = () => Candidate.Create(vacancyId, referralId, document, workflow);
            act.Should().Throw<ArgumentException>().WithMessage("*workflow*");
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