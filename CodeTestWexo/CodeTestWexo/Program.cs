using Blazored.LocalStorage;
using CodeTestWexo.Components;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Repository;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ISerieRepository, SerieRepository>();
builder.Services.AddScoped<ICreditRepository, CreditsRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddBlazoredLocalStorage();

// Singleton means its living as long as the application is living
builder.Services.AddSingleton<IRestClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var options = new RestClientOptions("https://api.themoviedb.org");
    var client = new RestClient(options);
    client.AddDefaultHeader("Authorization", $"Bearer {configuration["MovieDb:BearerToken"]}");
    client.AddDefaultHeader("accept", "application/json");
    return client;
});


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
