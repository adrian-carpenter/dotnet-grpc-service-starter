using GrpcWebServiceStarter.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

// Add RESTful Support with Endpoint Annotation
builder.Services.AddGrpcHttpApi();

// Add Swashbuckle Swagger Gen
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" }); });

// Add Grpc Swagger Implemenation
builder.Services.AddGrpcSwagger();

// Add Cors for Grpc Web
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var app = builder.Build();

// Set ENV
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Use Swagger
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// This Allows Grpc Annotation to Restful Paths
app.UseRouting();
// Add Grpc Web Support for Browsers
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true});
// Enable CORS
app.UseCors();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();