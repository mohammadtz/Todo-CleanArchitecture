﻿using Framework.Domain;

namespace Todo.Domain.Todo;

public class Todo : BaseEntity<int>
{
    public Todo(string title, int? tagId = null)
    {
        Title = title;
        CreationDate = DateTime.Now;
        IsDoneDate = DateTime.Now;
        IsDone = false;
        TagId = tagId;
    }

    public string Title { get; set; }
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
