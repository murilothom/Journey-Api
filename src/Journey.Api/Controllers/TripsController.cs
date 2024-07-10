using Journey.Application.UseCases.Trips.DeleteById;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            var registerTripUseCase = new RegisterTripUseCase();

            var response = registerTripUseCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var getAllTripsUseCase = new GetAllTripsUseCase();

            var result = getAllTripsUseCase.Execute();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var getByIdUseCase = new GetTripByIdUseCase();

            var response = getByIdUseCase.Execute(id);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var deleteTripByIdUseCase = new DeleteTripByIdUseCase();

            deleteTripByIdUseCase.Execute(id);

            return NoContent();
        }
    }
}