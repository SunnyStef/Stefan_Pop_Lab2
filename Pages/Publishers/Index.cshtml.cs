using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.ViewModels;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public IndexModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public PublisherIndexData PublisherData { get; set; }
        = new PublisherIndexData();

    public int PublisherID { get; set; }

    public int BookID { get; set; }


    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();

        // Incarcam editurile impreuna cu cartile si autorii
        PublisherData.Publishers = await _context.Publisher
            .Include(p => p.Books)
                .ThenInclude(b => b.Authors)
            .OrderBy(p => p.PublisherName)
            .ToListAsync();

        // Pastram lista folosita de pagina existenta
        Publisher = PublisherData.Publishers.ToList();

        // Verificam daca a fost selectata o editura
        if (id != null)
        {
            var selectedPublisher = PublisherData.Publishers
                .FirstOrDefault(p => p.ID == id.Value);

            if (selectedPublisher != null)
            {
                PublisherID = id.Value;

                PublisherData.Books = selectedPublisher.Books
                    ?? new List<Book>();
            }
        }

        if (bookID != null)
        {
            BookID = bookID.Value;
        }
    }

}
