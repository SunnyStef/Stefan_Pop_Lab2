using Microsoft.EntityFrameworkCore;

public class Stefan_Pop_Lab2Context(DbContextOptions<Stefan_Pop_Lab2Context> options) : DbContext(options)
{
    public DbSet<Stefan_Pop_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Stefan_Pop_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Stefan_Pop_Lab2.Models.Authors> Authors { get; set; } = default!;

}
