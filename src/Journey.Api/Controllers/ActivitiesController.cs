using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {

        [HttpPost("trip/{tripId:Guid}")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity(
            [FromRoute] Guid tripId,
            [FromBody] RequestRegisterActivityJson request)
        {
            var registerActivityForTripUseCase = new RegisterActivityForTripUseCase();

            var response = registerActivityForTripUseCase.Execute(tripId, request);

            return Created(string.Empty, response);
        }

        [HttpPatch("{activityId:Guid}/trip/{tripId:Guid}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult CompleteActivity(
            [FromRoute] Guid activityId,
            [FromRoute] Guid tripId)
        {
            var completeActivityForTripUseCase = new CompleteActivityForTripUseCase();

            completeActivityForTripUseCase.Execute(activityId, tripId);

            return NoContent();
        }
    }
}
