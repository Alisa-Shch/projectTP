namespace Domain.Tests
{
    [TestFixture]
    public class CompanyTest
    {
        private Fixture? _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidName_ShouldCreateCompany()
        {
            var name = _fixture.Create<string>();
            var company = Company.Create(name);

            company.Should().NotBeNull();
            company.Name.Should().Be(name);
            company.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            Action act = () => Company.Create(null!);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            Action act = () => Company.Create(string.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}