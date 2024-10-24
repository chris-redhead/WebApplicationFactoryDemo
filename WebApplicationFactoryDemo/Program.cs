using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/item", () =>
    {
        return ToDoItemContext.Items.ToList();
    })
    .WithName("GetTodoItems")
    .WithOpenApi();

app.MapPost("/item", (AddToDoItemRequest request) =>
    {
        var item = new ToDoItem(
            ToDoItemContext.Items.Count + 1,
            request.Description,
            request.DueDate);
        ToDoItemContext.Items.Add(item);
        return Results.Created($"/item/{item.Id}", item);
    })
    .WithName("CreateTodoItem")
    .WithOpenApi();

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