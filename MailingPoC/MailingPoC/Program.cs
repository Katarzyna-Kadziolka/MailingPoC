using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleEmail;
using LocalStack.Client.Extensions;
using MailingPoC.Features.Emails.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddLocalStack(builder.Configuration);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSServiceLocalStack<IAmazonSimpleEmailService>();

if (builder.Environment.IsProduction())
{
    builder.Services.AddTransient<IEmailService, SesService>();
}
else
{
    // builder.Services.AddTransient<IEmailService, SesService>();
    
    builder.Services.AddTransient<IEmailService, MailhogService>();
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();