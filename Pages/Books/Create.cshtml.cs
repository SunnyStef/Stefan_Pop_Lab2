using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
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

        var book = new Book();

        PopulateAssignedCategoryData(_context, book);

        return Page();
    }


    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        // Verificam daca datele introduse sunt valide
        if (!ModelState.IsValid)
        {
            ViewData["AuthorsID"] = new SelectList(
                _context.Authors.ToList(),
                "ID", "FullName", Book.AuthorsID);

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher.ToList(),
                "ID", "PublisherName", Book.PublisherID);

            PopulateAssignedCategoryData(_context, Book);

            foreach (var cat in AssignedCategoryDataList)
            {
                cat.Assigned = selectedCategories != null &&
                    selectedCategories.Contains(cat.CategoryID.ToString());
            }

            return Page();
        }

        // Initializam lista categoriilor cartii
        Book.BookCategories = new List<BookCategory>();

        // Adaugam categoriile selectate
        if (selectedCategories != null)
        {
            foreach (var cat in _context.Category)
            {
                if (selectedCategories.Contains(cat.ID.ToString()))
                {
                    Book.BookCategories.Add(new BookCategory
                    {
                        CategoryID = cat.ID
                    });
                }
            }
        }

        // Salvam cartea in baza de date
        _context.Book.Add(Book);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

}
