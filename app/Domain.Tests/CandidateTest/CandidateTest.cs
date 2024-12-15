namespace Domain.Tests
{
    [TestFixture]
    public class CandidateTest
    {
        private Fixture? _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateCandidate()
        {
            var name = _fixture.Create<string>();
            var mail = _fixture.Create<string>();
            var candidate = Candidate.Create(name, mail);

            candidate.Should().NotBeNull();
            candidate.Name.Should().Be(name);
            candidate.Mail.Should().Be(mail);
            candidate.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            var mail = _fixture.Create<string>();

            Action act = () => Candidate.Create(null!, mail);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullMail_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Candidate.Create(name, null!);

            act.Should().Throw<ArgumentException>().WithMessage("*mail*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var mail = _fixture.Create<string>();

            Action act = () => Candidate.Create(string.Empty, mail);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyMail_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Candidate.Create(name, string.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*mail*");
        }
    }
}