using Grpc.Core;
using GreeterRpc;

namespace GrpcWebServiceStarter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var helloReply = new HelloReply { Message = "Hello " + request.Name };
        return Task.FromResult(helloReply);
    }

    public override Task<HelloReply> SayHelloFrom(HelloRequestFrom requestFrom, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Post Hello" + requestFrom.Name + "From " + requestFrom.From
        });
    }
}