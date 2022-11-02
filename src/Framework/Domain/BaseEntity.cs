namespace Framework.Domain;

public class BaseEntity<T> where T : struct
{
    public T Id { get; set; }
    public DateTime CreationDate { get; set; }

    public BaseEntity()
    {
        CreationDate = DateTime.Now;
    }
}