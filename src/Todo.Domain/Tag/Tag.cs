namespace Todo.Domain.Tag;

public class Tag
{
    public Tag(string name, string color)
    {
        Name = name;
        Color = color;
        CreationDate = DateTime.Now;

        Tasks = new HashSet<Todo.Todo>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime CreationDate { get; set; }

    public virtual ICollection<Todo.Todo> Tasks { get; set; }
}