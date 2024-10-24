using Microsoft.AspNetCore.Mvc;
using WebApplicationFactoryDemo.Repositories;

namespace WebApplicationFactoryDemo.Controllers;

[ApiController]
[Route("item")]
public class ToDoListController : ControllerBase
{
    private readonly ToDoItemRepository _repository;

    public ToDoListController(ToDoItemRepository repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    public IEnumerable<ToDoItem> Get()
    {
        return _repository.GetItems();
    }
    
    [HttpPost]
    public IActionResult Post(AddToDoItemRequest request)
    {
        var item = new ToDoItem(
            ToDoItemContext.Items.Count + 1,
            request.Description,
            request.DueDate);
        _repository.AddItem(item);
        
        return Created($"/item/{item.Id}", item);
    }
}