using Microsoft.AspNetCore.ResponseCompression;
using StudioSync.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseCors(b =>
{
    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    //b.WithOrigins("https://localhost:7038").WithMethods("GET","POST").AllowCredentials();
});

var webSockeOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

webSockeOptions.AllowedOrigins.Add("https://localhost:7038");

app.UseWebSockets(webSockeOptions);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCompression();

app.MapHub<MainHub>("/connect");

app.MapControllers();


app.Run();
