﻿using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Id = entity.Id,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BadRequestException(ResourceErrorMessages.NAME_EMPTY);
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new BadRequestException(ResourceErrorMessages.START_DATE_TRIP_MUST_BE_GREATER_THAN_TODAY);
            }

            if (request.EndDate.Date <= request.StartDate.Date)
            {
                throw new BadRequestException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_GREATER_THAN_START_DATE);
            }
        }
    }
}
