﻿
using buildingblocks.CQRS;
using FluentValidation;
using MediatR;


namespace buildingblocks.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> 
        where TRequest :ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationresults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context , cancellationToken)));

            var failures = validationresults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
