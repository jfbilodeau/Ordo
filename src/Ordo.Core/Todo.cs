namespace Ordo.Core;

public class Todo
{
    public Guid Id { get; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }

    private Todo(string name, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        DueDate = dueDate;
    }

    public static Todo Create(string name, DateTime dueDate) => new(name, dueDate);
}
