﻿using System.Globalization;

namespace Todo.Domain.Tag;

public class TagDto
{
    public TagDto(Tag? tag)
    {
        if (tag is null)
            throw new ArgumentNullException(nameof(tag));

        Id = tag.Id;
        Name = tag.Name;
        Color = tag.Color;
        CreationDate = tag.CreationDate.ToString(CultureInfo.CurrentCulture);
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public string CreationDate { get; set; }
}

public class TagCommand
{
    public string Name { get; set; } = null!;
    public string? Color { get; set; } = null;
}