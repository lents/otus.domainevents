using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using MaualDomainEvents.Configs;
using MaualDomainEvents.DomainEvents;
using MaualDomainEvents.DomainEvents.Handlers;
using MaualDomainEvents.Entities;
using MaualDomainEvents.Infrastructure;
using MaualDomainEvents.Infrastructure.Mailing;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Fpe;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.WriteIndented = true; });

builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));
builder.Services.AddSingleton<MELProtocolLogger>();
builder.Services.AddScoped<IEmailSender, MailKitSmtpEmailSender>();
builder.Services.AddHostedService<ProductAddedEventHandler>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

//ValidateAllConfigsBeta();

var app = builder.Build();

async IAsyncEnumerable<string> SendMails(
    IEmailSender emailSender,
    [EnumeratorCancellation] CancellationToken cancellationToken)
{
    var cts = CancellationTokenSource.CreateLinkedTokenSource(
        cancellationToken,
        new CancellationTokenSource(TimeSpan.FromSeconds(3)).Token
    );
    for (int i = 0; i < 1; i++)
    {
        var sw = Stopwatch.StartNew();
        emailSender.Send(
            "Почтальен Печкин",
            "income@rodion-m.ru",
            $"Тема {i}",
            $"<strong>Время сервера: {DateTimeOffset.Now}</strong>",
            cancellationToken: cts.Token
        );
        yield return $"Sent in {sw.ElapsedMilliseconds} ms";
        await Task.Delay(TimeSpan.FromMinutes(15));
    }
}

app.MapGet("/", () => "Откройте страницы /send или /product");
app.MapGet("/send", SendMails);



app.MapPost("/product", (string name) =>
{
    // логика добавления товара
    _ = new Product("OtusLesson");
    return "Событие 'добавление товара' вызвано";
});




app.MapControllers();

app.Run();


void ValidateAllConfigsBeta()
{
    //TODO перенести в background service
    using var provider = builder.Services.BuildServiceProvider();
    foreach (var service in builder.Services
                 .Where(it => it.ServiceType.GenericTypeArguments.FirstOrDefault()
                     ?.IsSubclassOf(typeof(ValidableConfig)) == true))
    {
        var configType = service.ServiceType.GenericTypeArguments[0];
        var options = provider.GetRequiredService(typeof(IOptions<>)
            .MakeGenericType(configType)) as IOptions<ValidableConfig>;
        options!.Value.ValidateProperties();
        Console.WriteLine(configType.FullName);
    }
}
