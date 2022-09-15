using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.API.Controllers
{

    public class TodoAppController : BaseController
    {
        private readonly ITodoAppService _todoAppService;

        public TodoAppController(ITodoAppService todoAppService)
        {
            _todoAppService = todoAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _todoAppService.GetAll());
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _todoAppService.GetById(id));
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]PostTodoApp todo)
        {
            var _result = await _todoAppService.Save(todo);
            return Ok(_result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTodoApp todo)
        {
            var _result = await _todoAppService.Update(todo);
            return Ok(_result);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var _result = await _todoAppService.Delete(id);
            return Ok(_result);
        }
    }
}
