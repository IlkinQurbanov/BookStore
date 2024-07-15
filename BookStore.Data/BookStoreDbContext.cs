using Microsoft.EntityFrameworkCore;
using BookStore.Data.Entities;

namespace BookStore.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<BookEntity> Books { get; set; }
    }
}
