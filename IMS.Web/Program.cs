using AutoMapper;
using IMS.Web.Components;
using IMS.Web.Helpers;
using IMS.Web.Mappings;
using IMS.Web.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging.Abstractions;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<ClientApiService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<LoginState>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MemoryBufferThreshold = int.MaxValue;
});

#region Http Clients
builder.Services.AddHttpClient("IMS", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"));
});
#endregion

#region Auto Mapper
var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new WebMappingProfile());
}, new NullLoggerFactory());
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
