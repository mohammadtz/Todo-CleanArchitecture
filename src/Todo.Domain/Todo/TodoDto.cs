using System.Globalization;
using Todo.Domain.Tag;

namespace Todo.Domain.Todo;

public class TodoDto
{
    public TodoDto(Todo? todo, TagDto? tag)
    {
        if (todo == null) throw new ArgumentNullException(nameof(todo));

        Id = todo.Id;
        Title = todo.Title;
        CreationDate = todo.CreationDate.ToString(CultureInfo.CurrentCulture);
        IsDoneDate = todo.IsDoneDate?.ToString(CultureInfo.CurrentCulture);
        IsDone = todo.IsDone;
        TagId = todo.TagId;
        Tag = todo.TagId != null ? tag : null;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string CreationDate { get; set; }
    public string? IsDoneDate { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
    public TagDto? Tag { get; set; }
}

public class TodoCommand
{
    public string Title { get; set; } = null!;
    public int? TagId { get; set; } = null;
}