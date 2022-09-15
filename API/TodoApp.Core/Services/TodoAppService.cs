



using System.Net;
using TodoApp.Core.Common;
using TodoApp.Domain;

namespace TodoApp.Core.Services
{
    public class TodoAppService : ITodoAppService
    {
        private readonly ITodoAppRepository _todoAppRepository;
        private readonly IMapper _mapper;

        public TodoAppService(ITodoAppRepository dotoAppRepository,IMapper mapper)
        {
            _todoAppRepository = dotoAppRepository;
            _mapper = mapper;
        }
        public async Task<GenericResponse<bool>> Delete(long Id)
        {
            try
            {
               
                await _todoAppRepository.Delete(Id);

                return new GenericResponse<bool>
                   (ResponseType.Success,
                   (int)HttpStatusCode.OK,
                   ResponseMessage.Success, true);
            }
            catch (Exception ex)
            {

                throw new GenericException(ex.Message);
            }
        }

        public async Task<GenericResponse<IEnumerable<GetTodoApp>>> GetAll()
        {
            try
            {
                var todos = await _todoAppRepository.GetAll();
                var result = _mapper.Map<IEnumerable<GetTodoApp>>(todos.ToList());
                return new GenericResponse<IEnumerable<GetTodoApp>>
                    (ResponseType.Success,
                    (int)HttpStatusCode.OK,
                    ResponseMessage.Success, result);
            }
            catch (Exception ex)
            {

                throw new GenericException(ex.Message);
            }
           
        }

        public async Task<GenericResponse<GetTodoApp>> GetById(long Id)
        {
            try
            {
                var getTodoAppById = await _todoAppRepository.GetById(Id);
                var result = _mapper.Map<GetTodoApp>(getTodoAppById);

                if(result == null)
                    return new GenericResponse<GetTodoApp>
                   (ResponseType.Failed,
                   (int)HttpStatusCode.BadRequest,
                   ResponseMessage.Failed, result);


                return new GenericResponse<GetTodoApp>
                    (ResponseType.Success,
                    (int)HttpStatusCode.OK,
                    ResponseMessage.Success, result);
            }
            catch (Exception ex)
            {

                throw new GenericException(ex.Message);
            }
           
        }

        public async Task<GenericResponse<GetTodoApp>> Save(PostTodoApp todo)
        {
            try
            {
                if(todo == null)
                    return new GenericResponse<GetTodoApp>
                   (ResponseType.Failed,
                   (int)HttpStatusCode.BadRequest,
                   ResponseMessage._404Message, new GetTodoApp());

                var map_toodo = _mapper.Map<Todo>(todo);
                await _todoAppRepository.Save(map_toodo);
                var response_todo = _mapper.Map<GetTodoApp>(map_toodo);

                return new GenericResponse<GetTodoApp>
                    (ResponseType.Success,
                    (int)HttpStatusCode.OK,
                    ResponseMessage.Success, response_todo);

            }
            catch (Exception ex)
            {

                throw new GenericException(ex.Message);
            }
        }

        public async Task<GenericResponse<bool>> Update(UpdateTodoApp todo)
        {
            try
            {
                var getTodo = await _todoAppRepository.GetById(todo.Id);


                if (getTodo == null)
                    return new GenericResponse<bool>
                       (ResponseType.Failed,
                       (int)HttpStatusCode.BadRequest,
                       ResponseMessage._404Message, true);

                getTodo.WhatIsToBeDone = todo.WhatIsToBeDone;
                getTodo.StartDate = todo.StartDate;
                getTodo.DueDate = todo.DueDate;
                getTodo.ModifiedDate = DateTime.Now;
                getTodo.ModifiedBy = "Tobi Adeogun";
                getTodo.IsCompleted = todo.IsCompleted;
                if (todo.IsCompleted)
                {
                    getTodo.DateCompleted = DateTime.Now;

                }
                await _todoAppRepository.Update(getTodo);

                return new GenericResponse<bool>
                        (ResponseType.Success,
                        (int)HttpStatusCode.OK,
                        ResponseMessage.Success, true);
            }
            catch (Exception ex)
            {

                throw new GenericException(ex.Message);
            }
           

            
        }
    }
}
