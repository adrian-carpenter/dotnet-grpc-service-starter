using Grpc.Core;
using GrpcGreeterRpc;

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
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override Task<HelloReply> SayHelloFrom(HelloRequestFrom requestFrom, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Post Hello" + requestFrom.Name + "From" + requestFrom.From
        });
    }
}