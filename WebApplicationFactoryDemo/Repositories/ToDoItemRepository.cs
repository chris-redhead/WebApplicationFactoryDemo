namespace WebApplicationFactoryDemo.Repositories;

/// <summary>
/// This is an example repository to demonstrate how behaviour based unit tests
/// don't need to change when the implementation changes.
/// </summary>
public class ToDoItemRepository
{
    public void AddItem(ToDoItem item)
    {
        ToDoItemContext.Items.Add(item);
    }
    
    public IEnumerable<ToDoItem> GetItems()
    {
        return ToDoItemContext.Items;
    }
}