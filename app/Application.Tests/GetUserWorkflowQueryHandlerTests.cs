namespace Application.Tests
{
    [TestFixture]
    public class GetUserWorkflowQueryHandlerTests
    {
        private Mock<ICandidateWorkflowRepository> _workflowRepositoryMock;
        private GetUserWorkflowQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _workflowRepositoryMock = new Mock<ICandidateWorkflowRepository>();
            _handler = new GetUserWorkflowQueryHandler(_workflowRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_Return_Workflows()
        {
            var userId = Guid.NewGuid();
            var isOpenOnly = true;
            var workflows = new List<CandidateWorkflow>
            {
                new CandidateWorkflow(
                    Guid.NewGuid(),
                    new List<Step>(),
                    DateTime.Now,
                    new Candidate(Guid.NewGuid(), "Test Candidate"))
            };

            _workflowRepositoryMock
                .Setup(repo => repo.GetByUserId(userId, isOpenOnly, It.IsAny<CancellationToken>()))
                .ReturnsAsync((IReadOnlyCollection<Domain.CandidateWorkflow>)workflows);

            var query = new GetUserWorkflowsQuery(userId, isOpenOnly);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(workflows.Count);
            _workflowRepositoryMock.Verify(
                repo => repo.GetByUserId(userId, isOpenOnly, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public void Handle_Should_Throw_Exception_When_Query_Is_Null()
        {
            Should.ThrowAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
        }
    }
}
