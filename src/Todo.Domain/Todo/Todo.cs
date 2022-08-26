namespace Todo.Domain.Todo;

public class Todo
{
    public Todo(string title, int? tagId = null)
    {
        Title = title;
        CreationDate = DateTime.Now;
        IsDoneDate = DateTime.Now;
        IsDone = false;
        TagId = tagId;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? IsDoneDate { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
    public virtual Tag.Tag? Tag { get; set; }

    public void ChangeStatus()
    {
        var status = !IsDone;
        IsDone = status;
        IsDoneDate = status ? DateTime.Now : null;
    }
}
