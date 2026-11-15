using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public EditModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }


        var book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        PopulateAssignedCategoryData(_context, Book);
        ViewData["AuthorsID"] = new SelectList(
    _context.Authors.ToList(),
    "ID",
    "FullName",
    Book.AuthorsID
);

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            Book.PublisherID

        );

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        int bookId = id ?? Book.ID;

        if (bookId <= 0)
        {
            return NotFound();
        }

        // Cautam cartea si categoriile ei
        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.ID == bookId);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        // Actualizam informatiile cartii
        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.Price,
            b => b.PublishingDate,
            b => b.AuthorsID,
            b => b.PublisherID))
        {
            // Actualizam categoriile selectate
            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            // Salvam modificarile
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        // Reincarcam datele daca formularul nu este valid
        Book = bookToUpdate;

        PopulateAssignedCategoryData(_context, Book);

        ViewData["AuthorsID"] = new SelectList(
            _context.Authors.ToList(),
            "ID",
            "FullName",
            Book.AuthorsID
        );

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher.ToList(),
            "ID",
            "PublisherName",
            Book.PublisherID
        );

        return Page();
    }


    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
