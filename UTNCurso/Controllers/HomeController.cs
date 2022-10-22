using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.Extensions;
using UTNCurso.Models;

namespace UTNCurso.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITodoItemService todoItemService, ILogger<HomeController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        // GET: TodoItems
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _todoItemService.GetAllAsync());
        }

        // GET: TodoItems/Details/5
        [Authorize(Policy = "IsAdult")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || !await _todoItemService.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetAsync(id.Value);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Task,IsCompleted")] TodoItemDto todoItemDto)
        {
            _logger.LogInformation("Starting creation");

            using(var scope = _logger.BeginScope("Log under transaction scope"))
            {
                var result = await _todoItemService.CreateAsync(todoItemDto);
                _logger.LogInformation("Todo Item was created");
                
                if (!result.IsSuccessful)
                {
                    _logger.LogError("Something fail trying to create the todo item");
                    ModelState.AddModelError(result.Errors);
                }

                if (ModelState.IsValid)
                {
                    _logger.LogWarning("Todo item was succesfully created, redirecting");
                    return RedirectToAction(nameof(Index));
                }
                return View(todoItemDto);
            }
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || !await _todoItemService.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetAsync(id.Value);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Task,IsCompleted")] TodoItemDto todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            var result = await _todoItemService.UpdateAsync(todoItem);
            ModelState.AddModelError(result.Errors);

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || !await _todoItemService.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetAsync(id.Value);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!await _todoItemService.IsModelAvailableAsync())
            {
                return Problem("Entity set 'TodoContext.TodoItem'  is null.");
            }

            var result = await _todoItemService.RemoveAsync(id);

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DevError()
        {
            var featureException = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current.Id,
                Message = featureException.Error.Message,
                StackTrace = featureException.Error.StackTrace
            });
        }
    }
}
