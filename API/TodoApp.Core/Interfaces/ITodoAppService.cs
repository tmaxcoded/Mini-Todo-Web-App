


namespace TodoApp.Core.Interfaces
{
    public partial interface ITodoAppService
    {
        Task<GenericResponse<GetTodoApp>> Save(PostTodoApp todo);
        Task<GenericResponse<IEnumerable<GetTodoApp>>> GetAll();

        Task<GenericResponse<GetTodoApp>> GetById(long Id);

        Task<GenericResponse<bool>> Update(UpdateTodoApp todo);

        Task<GenericResponse<bool>> Delete(long Id);
    }
}
