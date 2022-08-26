namespace Todo.Domain.Tag;

public interface ITagRepository
{
    Task<IEnumerable<TagDto>> GetAllAsync();
    Task<Tag?> GetAsync(int id);
    Task<bool> IsValid(int id);
    Task Insert(Tag? entity);
    Task Update(Tag? entity);
    Task Delete(Tag? entity);
    Task Delete(int id);
    Task SaveChangesAsync();
}