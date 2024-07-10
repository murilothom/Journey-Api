using FluentValidation.Results;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {

            var dbContext = new JourneyDbContext();

            var trip = dbContext
                .Trips
                .FirstOrDefault(trip => trip.Id == tripId);

            if (trip is null)
            {
                throw new NotFoundExcpetion(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            Validate(trip, request);

            var entity = new Activity
            {
                Name = request.Name,
                Date = request.Date,
                TripId = tripId,
            };


            dbContext.Activities.Add(entity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Status = (ActivityStatus)entity.Status,
            };
        }

        private void Validate(Trip trip, RequestRegisterActivityJson request)
        {
            var validator = new RegisterActivityValidator();

            var result = validator.Validate(request);

            if ((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
            {
                var errorMessage = new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRIP_PERIOD);

                result.Errors.Add(errorMessage);
            }

            if (result.IsValid == false)
            {
                var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new BadRequestException(errorsMessages);
            }
        }
    }
}
