using Microsoft.EntityFrameworkCore;
using Stefan_Pop_Lab2.Models;

public class Stefan_Pop_Lab2Context(DbContextOptions<Stefan_Pop_Lab2Context> options) : DbContext(options)
{
    public DbSet<Stefan_Pop_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Stefan_Pop_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Stefan_Pop_Lab2.Models.Authors> Authors { get; set; } = default!;

    public DbSet<Stefan_Pop_Lab2.Models.Category> Category { get; set; } = default!;

    public DbSet<Stefan_Pop_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;

    public DbSet<Member> Member { get; set; } = default!;

    public DbSet<Borrowing> Borrowing { get; set; } = default!;



}
