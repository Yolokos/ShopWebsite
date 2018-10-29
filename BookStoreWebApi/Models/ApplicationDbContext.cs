using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderBook>()
                .HasKey(t => new { t.OrderId, t.BookId });

            modelBuilder.Entity<OrderBook>()
                .HasOne(sc => sc.Order)
                .WithMany(s => s.OrderBooks)
                .HasForeignKey(sc => sc.OrderId);

            modelBuilder.Entity<OrderBook>()
                .HasOne(sc => sc.Book)
                .WithMany(c => c.OrderBooks)
                .HasForeignKey(sc => sc.BookId);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}
    }
}
