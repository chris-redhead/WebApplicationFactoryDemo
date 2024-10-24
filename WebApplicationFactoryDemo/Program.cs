using Microsoft.AspNetCore.Mvc;
using WebApplicationFactoryDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ToDoItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

namespace WebApplicationFactoryDemo
{
    public partial class Program{ }
        
}
public class ToDoItemContext
{
    public static List<ToDoItem> Items { get; set; } = new();
}

public record AddToDoItemRequest(string Description, DateTime DueDate);

public record ToDoItem(int Id, string Description, DateTime dueDate);