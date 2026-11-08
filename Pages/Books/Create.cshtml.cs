using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public CreateModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }


    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName"
        );
        ViewData["AuthorsID"] = new SelectList(
    _context.Authors.ToList(), "ID", "FullName");
        return Page();
    }


    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
