namespace Domain.Tests
{
    [TestFixture]
    public class CompanyTests
    {
        private Fixture _fixture;

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
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            string name = string.Empty;

            Action act = () => Company.Create(name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            string name = null;

            Action act = () => Company.Create(name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}