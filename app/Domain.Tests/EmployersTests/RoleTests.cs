namespace Domain.Tests
{
    [TestFixture]
    public class RoleTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }


        [Test]
        public void Create_ValidName_ShouldCreateRole()
        {
            var name = _fixture.Create<string>();

            var role = Role.Create(name);

            role.Should().NotBeNull();
            role.Name.Should().Be(name);
            role.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            string name = string.Empty;

            Action act = () => Role.Create(name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            string name = null;

            Action act = () => Role.Create(name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}