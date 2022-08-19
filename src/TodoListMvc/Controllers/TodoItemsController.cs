

namespace TodoListMvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TodoListMvc.Models;

    public class TodoItemsController : Controller
    {
        private readonly TodoDbContext _context;

        public TodoItemsController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            return _context.TodoItems != null ?
                        View(await _context.TodoItems.ToListAsync()) :
                        Problem("Entity set 'TodoDbContext.TodoItems'  is null.");
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
        public async Task<IActionResult> Create([Bind("Id,IsDone,Title,Description")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                // Set the publish date automatically once the record is created
                todoItem.DateModified = DateTime.UtcNow;

                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsDone,Title,Description")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Keep date in track with the change
                    todoItem.DateModified = DateTime.UtcNow;

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TodoItems == null)
            {
                return Problem("Entity set 'TodoDbContext.TodoItems'  is null.");
            }
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ToggleDone(int id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                try
                {
                    // Keep date in track with the change
                    todoItem.DateModified = DateTime.UtcNow;

                    // Flip the completion state
                    todoItem.IsDone = !todoItem.IsDone;

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

        private bool TodoItemExists(int id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
