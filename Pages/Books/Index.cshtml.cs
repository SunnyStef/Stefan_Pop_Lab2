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




    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();

        // Incarcam cartile cu autorii, editurile si categoriile
        BookD.Books = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        // Pastram lista folosita de pagina existenta
        Book = BookD.Books.ToList();

        // Verificam daca a fost selectata o carte
        if (id != null)
        {
            var selectedBook = Book.FirstOrDefault(b => b.ID == id.Value);

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

}