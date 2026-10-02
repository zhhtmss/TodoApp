using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoTask Task { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            Task = task;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = await _context.TodoTasks.FindAsync(Task.Id);

            if (task == null)
            {
                return NotFound();
            }

            task.Title = Task.Title;
            task.IsCompleted = Task.IsCompleted;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}