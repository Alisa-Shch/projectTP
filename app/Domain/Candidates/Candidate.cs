﻿namespace Domain
{
    public class Candidate
    {
        public Guid Id { get; private set; }
        public Guid VacancyId { get; private set; }
        public Guid? ReferralId { get; private set; }
        public CandidateDocument Document { get; private set; }
        public CandidateWorkflow Workflow { get; private set; }

        private Candidate(Guid id, Guid vacancyId, Guid? referralId, CandidateDocument document, CandidateWorkflow workflow)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(nameof(vacancyId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(referralId));
            ArgumentException.ThrowIfNullOrEmpty(nameof(document));
            ArgumentNullException.ThrowIfNull(nameof(workflow));

            Id = id;
            VacancyId = vacancyId;
            ReferralId = referralId;
            Document = document;
            Workflow = workflow;
        }

        public static Candidate Create(Guid vacancyId, Guid? referralId, CandidateDocument document, CandidateWorkflow workflow)
        {
            if (vacancyId == Guid.Empty)
            {
                throw new ArgumentException("VacancyId cannot be empty", nameof(vacancyId));
            }
            if (referralId == Guid.Empty)
            {
                throw new ArgumentException("ReferralId cannot be empty", nameof(referralId));
            }
            if (document == null)
            {
                throw new ArgumentException("Document cannot be null", nameof(document));
            }
            if (workflow == null)
            {
                throw new ArgumentException("Workflow cannot be null", nameof(workflow));
            }

            return new Candidate(Guid.NewGuid(), vacancyId, referralId, document, workflow);
        }

        public void Approve(Employee user, string comment)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null", nameof(user));
            }
            ArgumentException.ThrowIfNullOrEmpty(comment, nameof(comment));

            Workflow.Approve(user, comment);
        }

        public void Reject(Employee user, string comment)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null", nameof(user));
            }
            ArgumentException.ThrowIfNullOrEmpty(comment, nameof(comment));

            Workflow.Reject(user, comment);
        }

        public void Restart()
        {
            Workflow.Restart();
        }
    }
}