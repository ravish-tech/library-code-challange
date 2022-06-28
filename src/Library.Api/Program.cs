using Library.Api.Configuration;
using Library.Api.Repositories;
using Library.Api.Services;
using Library.Api.TexlyzerEngine;

var builder = WebApplication.CreateBuilder(args);

// Registering typed configuration
builder.Services.Configure<LibraryOptions>(builder.Configuration.GetSection(LibraryOptions.SectionName));

// Adding local services
builder.Services.AddControllers();
// TexlyzerEngine is stateless processing engine and thus created every time its needed.
builder.Services.AddTransient<ITexlyzerEngine, TexlyzerEngine>();
// BooksService is a stateless layer between controller and repository.
builder.Services.AddTransient<IBooksService, BooksService>();
// Repository is state-full. Storing data about already processed books. Thus singleton.
// This is very basic and in a more enterprise-level solution a cache manager will be used. Either external like
// Aerospike or internal implementation.
builder.Services.AddSingleton<IBooksRepository, BooksRepository>();

#if DEBUG
// Services to support API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endif

// Building APP
var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

// For serving our UI (HTML & Js)
app.UseDefaultFiles();
app.UseStaticFiles();

// ToDo: There is no security on this API yet. We can easily achieve this using Identity Server.
// app.UseAuthorization();

// Auto add routes from controllers
app.MapControllers();
app.Run();