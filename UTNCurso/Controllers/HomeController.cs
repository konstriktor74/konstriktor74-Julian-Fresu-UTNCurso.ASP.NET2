using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Data;
using UTNCurso.Models;

namespace UTNCurso.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoContext _context;

        public HomeController(TodoContext context)
        {
            _context = context;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItem.ToListAsync());
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Task,IsCompleted")] TodoItem todoItem)
        {
            if (todoItem.Task.StartsWith("*"))
            {
                ModelState.AddModelError("Task", "Cannot use asterisk");
            }

            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpHead, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TodoItem == null)
            {
                return Problem("Entity set 'TodoContext.TodoItem'  is null.");
            }
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItem.Remove(todoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
    }
}
