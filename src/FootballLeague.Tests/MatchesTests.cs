using FootballLeague.API.Controllers;
using FootballLeague.API.Services.Contracts;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FootballLeague.Tests
{
    [TestFixture]
    public class MatchesTests
    {
        private Guid _validTestId;
        private Guid _invalidTestId;
        private MatchRequestModel _validModelState;
        private MatchRequestModel _invalidModelState;
        private MatchEditModel _validEditModel;
        private MatchEditModel _invalidEditModel;

        [SetUp]
        public void Init()
        {
            _validTestId = new Guid("3507c9d2-89de-4186-863a-94afc8e1a019");
            _invalidTestId = new Guid("53e4b155-4caf-4a0d-bc27-59027c5ba689");
            _validModelState = new MatchRequestModel
            {
                PlayedOn = DateTime.Today,
                HostId = new Guid(),
                GuestId = new Guid(),
                HostGoals = 1,
                GuestGoals = 2
            };
            _invalidModelState = new MatchRequestModel
            {
                PlayedOn = DateTime.Today
            };
            _validEditModel = new MatchEditModel
            {
                PlayedOn = DateTime.Today,
                HostGoals = 1,
                GuestGoals = 2
            };
            _invalidEditModel = new MatchEditModel
            {
                PlayedOn = DateTime.Now
            };
        }


        [Test]
        public async Task GetAllShouldReturnOkResponseWithData()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetAllMatches())
                .ReturnsAsync(new List<MatchResponseModel>(1));

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllShouldReturnBadRequestWithoutData()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetAllMatches())
                .ThrowsAsync(new ArgumentException());

            var controller = new MatchesController(mockService.Object);
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
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetAllMatches())
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task GetByIdShouldReturnOkResponseWithValidId()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchById(_validTestId))
                .ReturnsAsync(new MatchResponseModel());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetById(_validTestId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetByIdShouldReturnBadRequestWithoutData()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchById(_invalidTestId))
                .ThrowsAsync(new ArgumentException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetById(_invalidTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task GetByIdShouldReturnInternalServerErrorWhenErrorOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchById(_invalidTestId))
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetById(_invalidTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task CreateMatchShouldReturnOkResultWithValidModelState()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.CreateMatch(_validModelState))
                .ReturnsAsync(_validTestId);

            var controller = new MatchesController(mockService.Object);
            var result = await controller.CreateMatch(_validModelState);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateMatchShouldReturnBadRequestResultWithInvalidModelState()
        {
            var mockService = new Mock<IMatchesService>();

            var controller = new MatchesController(mockService.Object);
            controller.ModelState.AddModelError("HostId", "Required");

            var result = await controller.CreateMatch(_invalidModelState);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task CreateMatchShouldReturnInternalServerErrorIfErrorOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.CreateMatch(_validModelState))
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.CreateMatch(_validModelState);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task UpdateMatchShouldReturnOkResultWithValidModelState()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.UpdateMatch(_validTestId, _validEditModel))
                .ReturnsAsync(true);

            var controller = new MatchesController(mockService.Object);
            var result = await controller.UpdateMatch(_validTestId, _validEditModel);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateMatchShouldReturnBadRequestResultWithInvalidModelState()
        {
            var mockService = new Mock<IMatchesService>();

            var controller = new MatchesController(mockService.Object);
            controller.ModelState.AddModelError("HostGoals", "Required");

            var result = await controller.UpdateMatch(_invalidTestId, _invalidEditModel);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task UpdateMatchShouldReturnBadRequestWhenArgumentExceptionOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.UpdateMatch(_validTestId, _validEditModel))
                .ThrowsAsync(new ArgumentException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.UpdateMatch(_validTestId, _validEditModel);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task UpdateMatchShouldReturnInternalServerErrorIfErrorOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.UpdateMatch(_validTestId, _validEditModel))
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.UpdateMatch(_validTestId, _validEditModel);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task DeleteMatchShouldReturnOkResultWithValidData()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.DeleteMatch(_validTestId))
                .ReturnsAsync(true);

            var controller = new MatchesController(mockService.Object);
            var result = await controller.DeleteMatch(_validTestId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task DeleteMatchShouldReturnBadRequestWhenArgumentExceptionOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.DeleteMatch(_validTestId))
                .ThrowsAsync(new ArgumentException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.DeleteMatch(_validTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task DeleteMatchShouldReturnInternalServerErrorIfErrorOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.DeleteMatch(_validTestId))
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.DeleteMatch(_validTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }

        [Test]
        public async Task GetMatchesByTeamIdShouldReturnOkResultWithValidData()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchesByTeam(_validTestId))
                .ReturnsAsync(new List<MatchResponseModel>(1));

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetMatchesByTeamId(_validTestId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetMatchesByTeamIdShouldReturnBadRequestWhenArgumentExceptionOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchesByTeam(_validTestId))
                .ThrowsAsync(new ArgumentException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetMatchesByTeamId(_validTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            });
        }

        [Test]
        public async Task GetMatchesByTeamIdShouldReturnInternalServerErrorIfErrorOccurs()
        {
            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatchesByTeam(_validTestId))
                .ThrowsAsync(new FormatException());

            var controller = new MatchesController(mockService.Object);
            var result = await controller.GetMatchesByTeamId(_validTestId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ObjectResult>());
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            });
        }
    }
}