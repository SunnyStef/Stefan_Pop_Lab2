using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public IndexModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;


    public async Task OnGetAsync()
    {
        Borrowing = await _context.Borrowing
            .Include(b => b.Book)
                .ThenInclude(b => b!.Authors)
            .Include(b => b.Member)
            .ToListAsync();
    }

}
