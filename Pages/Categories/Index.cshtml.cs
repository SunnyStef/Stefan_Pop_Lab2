using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

namespace Stefan_Pop_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Stefan_Pop_Lab2Context _context;

    public IndexModel(Stefan_Pop_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public int SelectedCategoryID { get; set; }

    public string SelectedCategoryName { get; set; } = "";

    public IList<BookCategory> SelectedBookCategories { get; set; }
        = new List<BookCategory>();



    public async Task OnGetAsync(int? id)
    {
        // Incarcam toate categoriile
        Category = await _context.Category
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        // Verificam daca a fost selectata o categorie
        if (id != null)
        {
            var selectedCategory = Category
                .FirstOrDefault(c => c.ID == id.Value);

            if (selectedCategory != null)
            {
                SelectedCategoryID = selectedCategory.ID;
                SelectedCategoryName = selectedCategory.CategoryName;

                // Incarcam cartile categoriei si autorii lor
                SelectedBookCategories = await _context.BookCategory
                    .Include(bc => bc.Book)
                        .ThenInclude(b => b.Authors)
                    .Where(bc => bc.CategoryID == id.Value)
                    .ToListAsync();
            }
        }
    }

}
