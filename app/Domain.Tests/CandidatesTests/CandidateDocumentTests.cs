namespace Domain.Tests
{
    [TestFixture]
    public class CandidateDocumentTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateDocument()
        {
            var name = _fixture.Create<string>();
            var workExperience = _fixture.Create<string>();

            var document = CandidateDocument.Create(name, workExperience);

            document.Should().NotBeNull();
            document.Name.Should().Be(name);
            document.WorkExperience.Should().Be(workExperience);
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var name = string.Empty;
            var workExperience = _fixture.Create<string>();

            Action act = () => CandidateDocument.Create(name, workExperience);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            string name = null;
            var workExperience = _fixture.Create<string>();

            Action act = () => CandidateDocument.Create(name, workExperience);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyWorkExperience_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            var workExperience = string.Empty;

            Action act = () => CandidateDocument.Create(name, workExperience);
            act.Should().Throw<ArgumentException>().WithMessage("*workExperience*");
        }

        [Test]
        public void Create_NullWorkExperience_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();
            string workExperience = null;

            Action act = () => CandidateDocument.Create(name, workExperience);
            act.Should().Throw<ArgumentException>().WithMessage("*workExperience*");
        }
    }
}