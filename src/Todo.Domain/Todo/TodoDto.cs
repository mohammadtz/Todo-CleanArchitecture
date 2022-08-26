using Todo.Domain.Tag;

namespace Todo.Domain.Todo;

public class TodoDto
{
    public TodoDto(Todo? todo, TagDto? tag)
    {
        if (todo == null) throw new ArgumentNullException(nameof(todo));

        Id = todo.Id;
        Title = todo.Title;
        CreationDate = todo.CreationDate;
        IsDoneDate = todo.IsDoneDate;
        IsDone = todo.IsDone;
        TagId = todo.TagId;
        Tag = todo.TagId != null ? tag : null;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? IsDoneDate { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
    public TagDto? Tag { get; set; }
}

public class TodoCommand
{
    public string Title { get; set; } = null!;
    public int? TagId { get; set; } = null;
}