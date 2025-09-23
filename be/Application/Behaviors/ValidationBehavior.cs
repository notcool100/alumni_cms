using MediatR;
using FluentValidation;
using Alumni.Application.DTOs;

namespace Alumni.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                // If the response type is ApiResponse<T>, we can return a validation error
                if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    var responseType = typeof(TResponse);
                    var genericType = responseType.GetGenericArguments()[0];
                    var apiResponseType = typeof(ApiResponse<>).MakeGenericType(genericType);
                    
                    var response = Activator.CreateInstance(apiResponseType);
                    var successProperty = apiResponseType.GetProperty("Success");
                    var messageProperty = apiResponseType.GetProperty("Message");
                    var errorsProperty = apiResponseType.GetProperty("Errors");

                    successProperty?.SetValue(response, false);
                    messageProperty?.SetValue(response, "Validation failed");
                    errorsProperty?.SetValue(response, failures.Select(f => f.ErrorMessage).ToList());

                    return (TResponse)response!;
                }

                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
