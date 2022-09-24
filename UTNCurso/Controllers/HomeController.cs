using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Data;
using UTNCurso.Extensions;
using UTNCurso.Models;

namespace UTNCurso.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly TodoItem _todoItemModel;

        public HomeController(TodoContext context)
        {
            _todoItemModel = new TodoItem(context);
        }

        // GET: TodoItems
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _todoItemModel.GetAllAsync());
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || !await _todoItemModel.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemModel.GetAsync(id.Value);
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
        public async Task<IActionResult> Create([Bind("Id,Task,IsCompleted")] TodoItem todoItem)
        {
            var result = await _todoItemModel.CreateAsync(todoItem);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(result.Errors);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || !await _todoItemModel.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemModel.GetAsync(id.Value);

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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Task,IsCompleted")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            var result = await _todoItemModel.UpdateAsync(todoItem);
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
            if (id == null || !await _todoItemModel.IsModelAvailableAsync())
            {
                return NotFound();
            }

            var todoItem = await _todoItemModel.GetAsync(id.Value);

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
            if (!await _todoItemModel.IsModelAvailableAsync())
            {
                return Problem("Entity set 'TodoContext.TodoItem'  is null.");
            }

            var result = await _todoItemModel.RemoveAsync(id);

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
