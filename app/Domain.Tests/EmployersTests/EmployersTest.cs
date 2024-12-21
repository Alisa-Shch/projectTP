namespace Domain.Tests
{
    [TestFixture]
    public class EmployersTest
    {
        private Fixture? _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidRoleIdAndName_ShouldCreateEmployers()
        {
            var roleId = Guid.NewGuid();
            var name = _fixture.Create<string>();
            var employer = Employee.Create(roleId, name);

            employer.Should().NotBeNull();
            employer.Id.Should().NotBeEmpty();
            employer.RoleId.Should().Be(roleId);
            employer.Name.Should().Be(name);
        }

        [Test]
        public void Create_EmptyRoleId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Employee.Create(Guid.Empty, name);

            act.Should().Throw<ArgumentException>().WithMessage("*RoleID*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            var roleId = Guid.NewGuid();

            Action act = () => Employee.Create(roleId, null!);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var roleId = Guid.NewGuid();

            Action act = () => Employee.Create(roleId, string.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}