using Domain;

namespace Application.Tests
{
	[TestFixture]
	public class CreateWorkflowCommandHandlerTests
	{
		private Mock<IWorkflowTemplateRepository> _workflowTemplateRepositoryMock;
		private Mock<ICandidateWorkflowRepository> _workflowRepositoryMock;
		private CreateWorkflowCommandHandler _handler;

		[SetUp]
		public void SetUp()
		{
			_workflowTemplateRepositoryMock = new Mock<IWorkflowTemplateRepository>();
			_workflowRepositoryMock = new Mock<ICandidateWorkflowRepository>();
			_handler = new CreateWorkflowCommandHandler(_workflowTemplateRepositoryMock.Object, _workflowRepositoryMock.Object);
		}

		[Test]
		public async Task Handle_Should_Create_Workflow_And_Return_Candidate_Id()
		{
			var userReferaleId = Guid.NewGuid();
			var workflowTemplateId = Guid.NewGuid();
			var document = new Document("Test Document", "5 years experience");

			var workflowTemplate = WorkflowTemplate.Create(
				"Workflow Name",
				"Description",
				new ReadOnlyCollection<WorkflowTemplateStep>(new List<WorkflowTemplateStep>()));

			_workflowTemplateRepositoryMock
				.Setup(repo => repo.GetById(workflowTemplateId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(workflowTemplate);

			_workflowRepositoryMock
				.Setup(repo => repo.Create(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			var command = new CreateWorkflowCommand(userReferaleId, workflowTemplateId, document);

			var result = await _handler.Handle(command, CancellationToken.None);

			result.ShouldBe(userReferaleId);
			_workflowTemplateRepositoryMock.Verify(
				repo => repo.GetById(workflowTemplateId, It.IsAny<CancellationToken>()),
				Times.Once);
			_workflowRepositoryMock.Verify(
				repo => repo.Create(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()),
				Times.Once);
		}

		[Test]
		public void Handle_Should_Throw_Exception_When_Command_Is_Null()
		{
			Should.ThrowAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
		}

		[Test]
		public async Task Handle_Should_Throw_Exception_When_WorkflowTemplate_Not_Found()
		{
			var userReferaleId = Guid.NewGuid();
			var workflowTemplateId = Guid.NewGuid();
			var document = new Document("Test Document", "5 years experience");

			_workflowTemplateRepositoryMock
				.Setup(repo => repo.GetById(workflowTemplateId, It.IsAny<CancellationToken>()))
				.ReturnsAsync((WorkflowTemplate)null);

			var command = new CreateWorkflowCommand(userReferaleId, workflowTemplateId, document);

			var exception = await Should.ThrowAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
			exception.Message.ShouldBe("Workflow template not found.");
		}
	}
}
