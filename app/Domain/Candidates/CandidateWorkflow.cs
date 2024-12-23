﻿namespace Domain
{
    public class CandidateWorkflow
    {
        public Guid Id { get; private set; }
        public Guid TemplateId { get; private set; }
        public IReadOnlyCollection<CandidateWorkflowStep> Steps { get; private set; }

        private CandidateWorkflow(Guid id, Guid templateId, IReadOnlyCollection<CandidateWorkflowStep> steps)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(templateId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(steps));

            Id = id;
            TemplateId = templateId;
            Steps = steps;
        }        

        public static CandidateWorkflow Create(WorkflowTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentException("Template cannot be null", nameof(template));
            }

            return new(Guid.NewGuid(), template.Id, new List<CandidateWorkflowStep>(template.Steps.Select(CandidateWorkflowStep.Create)));
        }

        internal void Approve(Employee user, string comment)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null", nameof(user));
            }
            ArgumentException.ThrowIfNullOrEmpty(comment, nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Approve(user, comment);
        }

        internal void Reject(Employee user, string comment)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null", nameof(user));
            }
            ArgumentException.ThrowIfNullOrEmpty(comment, nameof(comment));

            CheckStatus();

            var step = GetStepInProgress();
            step.Reject(user, comment);
        }

        internal void Restart()
        {
            CheckStatus();

            foreach (var step in Steps)
            {
                step.Restart();
            }
        }

        public void CheckStatus()
        {
            if (Steps.All(x => x.Status == Status.Approved))
            {
                throw new Exception("All steps are already approved.");
            }
            if (Steps.Any(x => x.Status == Status.Rejected))
            {
                throw new Exception("Workflow contains rejected steps.");
            }
        }

        public CandidateWorkflowStep GetStepInProgress()
        {
            return Steps.OrderBy(x => x.NumberStep).FirstOrDefault(x => x.Status == Status.InProgress)
                   ?? throw new InvalidOperationException("No steps are in progress.");
        }
    }
}