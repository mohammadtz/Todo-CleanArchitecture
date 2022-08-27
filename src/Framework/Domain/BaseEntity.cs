namespace Framework.Domain;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreationDate { get; set; }

    public BaseEntity()
    {
        CreationDate = DateTime.Now;
    }
}