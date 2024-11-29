using AutoFixture;
using NUnit.Framework;

namespace Domain.Tests
{
    public class WorkflowApproveTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void CreateWorkflow()
        {
            _fixture.Customize<Step>(_ => new StepBuilder(true));
            var id = _fixture.Create<Guid>();
            var steps = _fixture.CreateMany<Step>().ToArray();
            var workflow = new Workflow(id, steps, _fixture.Create<DateTime>());

            NUnit.Framework.Assert.That(workflow, Is.Not.Null);
            workflow.Should().NotBeNull();
            workflow.Id.Should().Be(id);
            workflow.Steps.Should().BeEquivalentTo(steps);
            NUnit.Framework.Assert.That(id, Is.EqualTo(workflow.Id));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CreateWorkflowWithNullStep(bool isUser)
        {
            _fixture.Customize<Step>(_ => new StepBuilder(isUser));
            NUnit.Framework.Assert.Throws<ArgumentException>(() => new Workflow(_fixture.Create<Guid>(), null, _fixture.Create<DateTime>()), "steps");
        }
    }
}