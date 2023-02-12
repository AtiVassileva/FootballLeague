﻿using FootballLeague.API.Controllers;
using FootballLeague.API.Services.Contracts;
using FootballLeague.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FootballLeague.Tests.Controllers
{
    [TestFixture]
    public class TeamsControllerTests
    {
        private readonly Guid ValidTestId = new Guid("3507c9d2-89de-4186-863a-94afc8e1a019");
        private readonly Guid InvalidTestId = new Guid("53e4b155-4caf-4a0d-bc27-59027c5ba689");

        [Test]
        public async Task GetAllShouldReturnOkResponseWithData()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetAllTeams())
                .ReturnsAsync(new List<TeamResponseModel>(1));

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllShouldReturnBadRequestWithoutData()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetAllTeams())
                .ThrowsAsync(new ArgumentException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }
        
        [Test]
        public async Task GetAllShouldReturnInternalServerErrorWhenErrorOccurs()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetAllTeams())
                .ThrowsAsync(new FormatException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task GetTeamsRankingShouldReturnOkResponseWithData()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamsRanking())
                .ReturnsAsync(new List<TeamResponseModel>(1));

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetTeamsRanking();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetTeamsRankingShouldReturnBadRequestWithoutData()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamsRanking())
                .ThrowsAsync(new ArgumentException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetTeamsRanking();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task GetTeamsRankingShouldReturnInternalServerErrorWhenErrorOccurs()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamsRanking())
                .ThrowsAsync(new FormatException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetTeamsRanking();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task GetByIdShouldReturnOkResponseWithValidId()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamById(ValidTestId))
                .ReturnsAsync(new TeamResponseModel());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetById(ValidTestId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetByIdShouldReturnBadRequestWithoutData()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamById(InvalidTestId))
                .ThrowsAsync(new ArgumentException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetById(InvalidTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task GetByIdShouldReturnInternalServerErrorWhenErrorOccurs()
        {
            var mockService = new Mock<ITeamsService>();
            mockService.Setup(x => x.GetTeamById(InvalidTestId))
                .ThrowsAsync(new FormatException());

            var controller = new TeamsController(mockService.Object);
            var result = await controller.GetById(InvalidTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }
    }
}