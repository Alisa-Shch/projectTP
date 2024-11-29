using AutoFixture;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class RoleTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ValidRoleName_ShouldCreateRole()
        {
            var roleName = _fixture.Create<string>();

            var role = Role.Create(roleName);

            role.Should().NotBeNull();
            role.Id.Should().NotBeEmpty();
            role.RoleName.Should().Be(roleName);
        }

        [Test]
        public void Create_NullRoleName_ShouldThrowArgumentException()
        {
            Action act = () => Role.Create(null);

            act.Should().Throw<ArgumentException>().WithMessage("*roleName*");
        }

        [Test]
        public void Create_EmptyRoleName_ShouldThrowArgumentException()
        {
            Action act = () => Role.Create(string.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*roleName*");
        }
    }
}