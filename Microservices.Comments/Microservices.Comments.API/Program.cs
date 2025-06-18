using System.Net.Http.Headers;
using Microservices.Comments.API.Middleware;
using Microservices.Comments.Core.Services;
using Microservices.Comments.Domain.Interfaces;
using Microservices.Comments.Infrastructure;
using Microservices.Comments.Infrastructure.Interfaces;
using Microservices.Comments.SqlServerConnection;
using Microservices.Comments.SqlServerConnection.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IPostResolver, PostResolver>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5011/");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
