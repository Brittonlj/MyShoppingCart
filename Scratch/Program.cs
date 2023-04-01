


using MediatR;
using MyShoppingCart.Application.PipelineBehaviors;
using MyShoppingCart.Domain.Mediator;

var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
           .Where(t => !t.IsAbstract && !t.IsInterface).ToList();

var pipelineInterface = typeof(IPipelineBehavior<,>);
var exceptionPipeline = typeof(ExceptionLoggingPipelineBehavior<,>);
var validationPipeline = typeof(ValidationPipelineBehavior<,>);


var requests = types.Where(x => x.IsAssignableTo(typeof(IRequestMarker))).ToList();


foreach(var request in requests)
{
    var iquery = request.GetInterfaces().First(x => x.Name.Contains("IRequest`1"));
    var response = iquery.GetGenericArguments().First();
    var entity = response.GetGenericArguments().First();
    var interfaceToInject = pipelineInterface.MakeGenericType(new Type[] { request, response });
    var concreteExceprion = exceptionPipeline.MakeGenericType(new Type[] { request, entity });
    var concreteValidation = validationPipeline.MakeGenericType(new Type[] { request, entity });
    var y = 1;
}


















var x = 1;