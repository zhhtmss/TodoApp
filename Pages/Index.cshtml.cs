using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<TodoTask> Tasks { get; set; } = new();

        [BindProperty]
        public TodoTask NewTask { get; set; } = new();

        public async Task OnGetAsync()
        {
            Tasks = await _context.TodoTasks
                .OrderBy(x => x.IsCompleted)
                .ThenBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                Tasks = await _context.TodoTasks.ToListAsync();
                return Page();
            }

            _context.TodoTasks.Add(NewTask);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task != null)
            {
                _context.TodoTasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}