using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.Core.Domain;

namespace UTNCurso.WebApi.Controllers
{
    [ApiController]
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
        public async Task<ActionResult<TodoItemDto>> GetById([FromRoute] long id)
        {
            var todoItem = await _todoItemService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Post([FromBody] TodoItemDto request)
        {
            var result = await _todoItemService.CreateAsync(request);

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return result;
        }
    }
}