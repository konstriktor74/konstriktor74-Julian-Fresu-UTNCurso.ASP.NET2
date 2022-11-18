using System.Net;
using Microsoft.AspNetCore.Mvc;
using UTNCurso.Core.Domain;
using UTNCurso.Core.DTOs;
using UTNCurso.Core.Interfaces;

namespace UTNCursoApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    //[Route("{v:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> _logger;
        private readonly ITodoItemService _todoItemService;

        public TodosController(ILogger<TodosController> logger, ITodoItemService todoItemService)
        {
            _logger = logger;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItemDto>> GetAll()
        {
            return await _todoItemService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TodoItemDto> GetById([FromRoute] long id)
        {
            var todoItem = await _todoItemService.GetAsync(id);

            return todoItem;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] TodoItemDto request)
        {
            var result = await _todoItemService.CreateAsync(request);

            return result;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<TodoItemDto>> Search([FromQuery] string taskDescription, [FromQuery] bool? isCompleted)
        {
            return await _todoItemService.Search(taskDescription, isCompleted);
        }
    }
}