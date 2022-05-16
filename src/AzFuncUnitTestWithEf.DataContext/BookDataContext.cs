using AzFuncUnitTestWithEf.DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace AzFuncUnitTestWithEf.DataContext
{
    public class BookDataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDataContext(DbContextOptions<BookDataContext> options) : base(options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=book_db;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}