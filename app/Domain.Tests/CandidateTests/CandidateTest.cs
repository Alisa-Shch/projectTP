namespace Domain.Tests
{
    [TestFixture]
    public class CandidateTests
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
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template, employeeId, roleId);
            var candidate = Candidate.Create(name, mail, workflow);

            candidate.Should().NotBeNull();
            candidate.Name.Should().Be(name);
            candidate.Mail.Should().Be(mail);
            candidate.Workflow.Should().NotBeNull();
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentException()
        {
            var mail = _fixture.Create<string>();
            var employeeId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();
            var template = new TemplateBuilder().Create(typeof(WorkflowTemplate), (ISpecimenContext)_fixture) as WorkflowTemplate;
            var workflow = CandidateWorkflow.Create(template, employeeId, roleId);

            Action act = () => Candidate.Create(null!, mail, workflow);

            act.Should().Throw<ArgumentNullException>().WithMessage("*name*");
        }

        [Test]
        public void Create_NullMail_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Candidate.Create(name, null, null!);

            act.Should().Throw<ArgumentNullException>().WithMessage("*mail*");

        }

        [Test]
        public void Create_EmptyMail_ShouldThrowArgumentException()
        {
            var name = _fixture.Create<string>();

            Action act = () => Candidate.Create(name, string.Empty, null!);

            act.Should().Throw<ArgumentException>().WithMessage("*mail*");
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            var mail = _fixture.Create<string>();

            Action act = () => Candidate.Create(string.Empty, mail, null!);

            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }
    }
}