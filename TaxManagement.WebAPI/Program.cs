using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using TaxManagement.Application.Services;
using TaxManagement.Domain.Entities;
using TaxManagement.Infrastructure;
using TaxManagement.WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TaxEntryStatusEnum>());
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer((doc, context, cancellationToken) =>
    {
        doc.Info.Title = "Tax Resolver API";
        doc.Info.Version = "v1";
        doc.Info.Description = "API de resolução de taxas fiscais.";
        return Task.CompletedTask;
    });
});

builder.Services.AddScoped<TaxCalculatorService>();
builder.Services.AddScoped<TaxEntryRegistrationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(opt =>
    {
        opt.Title = "Documentação API Tax Resolver";
        opt.Theme = ScalarTheme.DeepSpace;
        opt.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapTaxEntriesEndpoint();
app.MapTaxRulesEndpoint();

app.Run();