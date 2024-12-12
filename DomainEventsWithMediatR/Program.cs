using DomainEventsWithMediatR;
using DomainEventsWithMediatR.Domain.Models;
using DomainEventsWithMediatR.Domain.Services;
using DomainEventsWithMediatR.DomainEvents.Events;
using DomainEventsWithMediatR.DomainEvents.Users.GetUser;
using DomainEventsWithMediatR.DomainEvents.Users.UpdateUser;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ScopedDependency>();
builder.Services.AddScoped<UserService>();


builder.Services.AddMediatR(cfg =>
{
    //Обычно все обработчики событий находятся в слое Application
    cfg.RegisterServicesFromAssemblyContaining<Program>();


    //cfg.NotificationPublisher = new ForeachAwaitPublisher(); // По умолчанию
    cfg.NotificationPublisher = new TaskWhenAllPublisher();
});







var app = builder.Build();
app.MapGet("/", async (IMediator mediator, ILogger<Program> logger, IServiceProvider serviceProvider) =>
{
    logger.LogInformation("Index page opened");
    logger.LogInformation("Service provider Id: {Id}", serviceProvider.GetHashCode());

    await mediator.Publish(new IndexPageOpened(DateTimeOffset.Now));
    return "Hello World!";
});


app.MapGet("/user/{id}", (int id, IMediator mediator) =>
{


    return mediator.Send(new GetUserQuery(id));
});


app.MapPost("/user", ([FromBody] User user, IMediator mediator) =>
{


    return mediator.Send(new CreateUserCommand { Name = user.Name });
});




app.Run();