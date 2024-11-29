﻿using AutoFixture;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class EmployersTest
    {
        private Fixture _fixture;

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

            var employer = Employers.Create(roleId, name);

            employer.Should().NotBeNull();
            employer.Id.Should().NotBeEmpty();
            employer.RoleID.Should().Be(roleId);
            employer.Name.Should().Be(name);
        }

        [Test]
        public void Create_EmptyRoleId_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Employers.Create(Guid.Empty, name);

            act.Should().Throw<ArgumentException>().WithMessage("*RoleID*");
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            var roleId = Guid.NewGuid();

            Action act = () => Employers.Create(roleId, null);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var roleId = Guid.NewGuid();

            Action act = () => Employers.Create(roleId, string.Empty);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}