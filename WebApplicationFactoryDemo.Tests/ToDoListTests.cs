using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApplicationFactoryDemo.Tests;

public class ToDoListTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ToDoListTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetItem_ReturnsItems()
    {
        // Arrange
        var client = _factory.CreateClient();
        ToDoItemContext.Items.Clear();
        ToDoItemContext.Items.AddRange(
            [
                new ToDoItem(
                    1,
                    "Create Dotnet Liverpool Presentation",
                    DateTime.Today.AddDays(-1)),
                new ToDoItem(
                    2,
                    "Go for a pint",
                    DateTime.Today.AddHours(1))
            ]);

        // Act
        var response = await client.GetAsync("item");

        // Assert
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<List<ToDoItem>>(
            responseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.Equal(2, items.Count);
    }
    
    [Fact]
    public async Task PostItem_PersistsItem()
    {
        // Arrange
        var client = _factory.CreateClient();
        ToDoItemContext.Items.Clear();

        // Act
        var response = await client.PostAsJsonAsync(
            "item",
            new
            {
                description = "Write tests",
                dueDate = DateTime.Today.AddDays(1)
            });

        // Assert
        response.EnsureSuccessStatusCode();
        var item = Assert.Single(ToDoItemContext.Items);
        Assert.Equal("Write tests", item.Description);
    }
}