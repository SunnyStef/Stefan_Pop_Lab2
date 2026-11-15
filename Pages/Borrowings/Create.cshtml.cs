
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public CreateModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    // Pregatim listele pentru carti si membri
    private void LoadDropDownLists(
        int? bookId = null, int? memberId = null)
    {
        // Incarcam cartile impreuna cu autorii
        var bookList = _context.Book
            .Include(b => b.Authors)
            .ToList()
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " +
                    (x.Authors != null
                        ? x.Authors.LastName + " " + x.Authors.FirstName
                        : "Fara autor")
            })
            .ToList();

        // Lista cartilor
        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName", bookId);

        // Lista membrilor
        ViewData["MemberID"] = new SelectList(
            _context.Member.ToList(),
            "ID", "FullName", memberId);
    }

    // Afisam formularul Create
    public IActionResult OnGet()
    {
        LoadDropDownLists();

        return Page();
    }

    // Preluam datele introduse in formular
    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // Salvam imprumutul
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadDropDownLists(
                Borrowing.BookID,
                Borrowing.MemberID);

            return Page();
        }

        _context.Borrowing.Add(Borrowing);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
