
using System.Reflection;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application_Layer.PipelineBehaviour
{
    //public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    //    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        // Log request type
    //        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

    //        // Log request properties, excluding sensitive ones
    //        Type myType = request.GetType();
    //        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
    //        foreach (PropertyInfo prop in props)
    //        {
    //            // Check if the property has the [JsonIgnore] attribute
    //            if (!prop.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any())
    //            {
    //                object propValue = prop.GetValue(request, null)!;
    //                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
    //            }
    //        }

    //        var response = await next();

    //        // Log response type
    //        _logger.LogInformation($"Handled {typeof(TResponse).Name}");

    //        return response;
    //    }
    //}
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null)!;
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }
            var response = await next();
            //Response
            _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
