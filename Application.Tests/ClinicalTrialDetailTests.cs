using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ClinicalTrials;
using Domain;
using Domain.Interfaces;
using Moq;

namespace Application.Tests
{
    public class ClinicalTrialDetailTests
    {
        private readonly Mock<IGenericRepository<ClinicalTrial>> _repositoryMock;
        private readonly ClinicalTrialDetail.Handler _handler;

        public ClinicalTrialDetailTests()
        {
            _repositoryMock = new Mock<IGenericRepository<ClinicalTrial>>();
            _handler = new ClinicalTrialDetail.Handler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnClinicalTrial_WhenIdIsValid()
        {
            // Arrange
            var trialId = 1;
            var expectedTrial = new ClinicalTrial
            {
                Id = trialId,
                Title = "Andol",
                StartDate = new DateOnly(2025, 1, 1),
                Participants = 3,
                ClinicalTrialStatusId = 2
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(trialId))
                .ReturnsAsync(expectedTrial);

            var query = new ClinicalTrialDetail.Query { Id = trialId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTrial.Id, result.Id);
            Assert.Equal(expectedTrial.StartDate, result.StartDate);
            Assert.Equal(expectedTrial.ClinicalTrialStatusId, result.ClinicalTrialStatusId);
            Assert.Equal(expectedTrial.Participants, result.Participants);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenIdNotFound()
        {
            // Arrange
            var trialId = 999;
            _repositoryMock.Setup(r => r.GetByIdAsync(trialId))
                .ReturnsAsync((ClinicalTrial)null);

            var query = new ClinicalTrialDetail.Query { Id = trialId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}