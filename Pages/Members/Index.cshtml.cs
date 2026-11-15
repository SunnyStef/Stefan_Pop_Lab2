using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.MemberPages;

public class IndexModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public IndexModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    public IList<Member> Member { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Member = await _context.Member.ToListAsync();
    }
}
