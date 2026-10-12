using CityInfo2;
using CityInfo2.Services;
using System.Diagnostics.Eventing.Reader;

var builder = WebApplication.CreateBuilder(args);
// builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();
//builder.Logging.AddConsole(); 


// Add services to the container.
//Accept xml and json formatters for the controllers:
builder.Services.AddControllers()
        .AddXmlDataContractSerializerFormatters();

// Add new service, maar ik wil meerdere mailservices kunnen gebruiken anders heb je een tightcoupling tussen de controller en de mailservice, dus ik maak een interface IMailService en laat de LocalMailService die implementeren. Dan kan ik in de controller IMailService gebruiken en hoef ik niet te weten welke implementatie er gebruikt wordt.
//builder.Services.AddTransient<IMailService, CloudMailService>();
//builder.Services.AddTransient<IMailService, LocalMailService>();

builder.Services.AddSingleton<CitiesDataStore>(); //singleton: er wordt maar 1 instantie van de class gemaakt en die wordt gedeeld door alle controllers. Transient: er wordt een nieuwe instantie van de class gemaakt voor elke controller. Scoped: er wordt een nieuwe instantie van de class gemaakt voor elke request.

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions.Add("additionalInfo", "This is a custom extension added to the problem details.");
        ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
