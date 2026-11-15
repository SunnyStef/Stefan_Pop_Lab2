using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public IndexModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public BookData BookD { get; set; } = new BookData();

    public int BookID { get; set; }

    public int CategoryID { get; set; }






    public async Task OnGetAsync(
        int? id, int? categoryID,
        string? sortOrder, string? searchString)
    {
        BookD = new BookData();

        // Pregatim sortarea
        TitleSort = string.IsNullOrEmpty(sortOrder)
            ? "title_desc" : "";

        AuthorSort = sortOrder == "author"
            ? "author_desc" : "author";

        // Retinem textul cautat
        CurrentFilter = searchString ?? "";

        // Incarcam cartile si datele asociate
        BookD.Books = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .ToListAsync();

        // Cautam dupa titlu sau autor
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            BookD.Books = BookD.Books.Where(b =>
                b.Title.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase)
                ||
                (b.Authors != null &&
                    (b.Authors.FirstName.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    b.Authors.LastName.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase)))
            );
        }

        // Sortam rezultatele
        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Title);
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Authors?.FullName);
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Authors?.FullName);
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Title);
                break;
        }

        Book = BookD.Books.ToList();

        // Verificam cartea selectata
        if (id != null)
        {
            var selectedBook = Book
                .FirstOrDefault(b => b.ID == id.Value);

            if (selectedBook != null)
            {
                BookID = id.Value;

                BookD.Categories = selectedBook.BookCategories
                    .Select(bc => bc.Category)
                    .ToList();
            }
        }

        if (categoryID != null)
        {
            CategoryID = categoryID.Value;
        }
    }




    public string TitleSort { get; set; } = "";

    public string AuthorSort { get; set; } = "";
    public string CurrentFilter { get; set; } = "";

}