namespace Domain.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidData_ShouldCreateEmployee()
        {
            var roleId = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();

            var employee = Employee.Create(roleId, name);

            employee.Should().NotBeNull();
            employee.RoleId.Should().Be(roleId);
            employee.Name.Should().Be(name);
            employee.Id.Should().NotBeEmpty();
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var roleId = _fixture.Create<Guid>();
            string name = string.Empty;

            Action act = () => Employee.Create(roleId, name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            var roleId = _fixture.Create<Guid>();
            string name = null;

            Action act = () => Employee.Create(roleId, name);
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyRoleId_ShouldThrowArgumentException()
        {
            var roleId = Guid.Empty;
            var name = _fixture.Create<string>();

            Action act = () => Employee.Create(roleId, name);
            act.Should().Throw<ArgumentException>().WithMessage("*roleId*");
        }
    }
}