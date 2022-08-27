using Framework.Domain;

namespace Todo.Domain.Tag;

public class Tag : BaseEntity<int>
{
    public Tag(string name, string? color)
    {
        Name = name;
        Color = color ?? "ffffff";
        CreationDate = DateTime.Now;

        Tasks = new HashSet<Todo.Todo>();
    }

    public string Name { get; set; }
    public string Color { get; set; }

    public virtual ICollection<Todo.Todo> Tasks { get; set; }
}