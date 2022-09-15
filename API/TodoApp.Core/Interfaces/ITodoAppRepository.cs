
namespace TodoApp.Core.Interfaces
{
    public interface ITodoAppRepository
    {

        Task<Todo> Save(Todo todo);
        Task<IEnumerable<Todo>> GetAll();

        Task<Todo> GetById(long Id);

        Task Update(Todo todo);

        Task Delete(long Id);
    }
}