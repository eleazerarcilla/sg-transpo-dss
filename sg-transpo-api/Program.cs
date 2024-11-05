using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using ss_transpo_dss.services.Interfaces;
using ss_transpo_dss.services.Services;
using ss_transpo_dss.clients.apiclient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new() { Title = "sg-transpo-api", Version = "v1" }));

string? conn = Environment.GetEnvironmentVariable("app_config") ??
               builder.Configuration["app_config"];
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(conn)
        .Select(KeyFilter.Any)
        .Select(KeyFilter.Any, "prod");
});

builder.Services.AddAzureAppConfiguration();
string? licenseKey = builder.Configuration["sg-lta-api-key"]; //supply your own ApiKey from LTA SG DataMall https://datamall.lta.gov.sg/content/datamall/en/request-for-api.html
string? sglta_baseUri = builder.Configuration["sg-lta-baseuri"];

#region Services
builder.Services.AddSingleton<ITimeTableService, TimeTableService>();
builder.Services.AddSingleton<ILTADataService, LTADataService>();
builder.Services.AddSingleton(_ => new ApiClient(sglta_baseUri!, licenseKey!, new HttpClient()));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();