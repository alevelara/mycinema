using MediatR;
using Microsoft.Extensions.Logging;

namespace Mycinema.Application.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly ILogger<TRequest> logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {

            return await next();
        }
        catch (Exception e)
        {
            var requestName = typeof(TRequest).Name;
            logger.LogError(e, $"Application Request: {e.Message}");
            throw;
        }
    }
}
