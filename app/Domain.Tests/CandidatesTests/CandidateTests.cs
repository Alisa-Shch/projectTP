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
    }
}