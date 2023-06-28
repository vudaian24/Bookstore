using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Model.Entities
{
    public partial class BookstoreContext : DbContext
    {
        public BookstoreContext()
        {
        }

        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminAccount> AdminAccounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Pay> Pays { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-P93MA1R\\SQLEXPRESS; Database=Bookstore; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.NumberPhone).HasMaxLength(10);

                entity.Property(e => e.Address).HasMaxLength(250);
            });

            modelBuilder.Entity<AdminAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AdminAccount");

                entity.Property(e => e.AdminId).HasColumnName("AdminId");

                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);

                entity.HasOne(d => d.AdminIdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminAccount_Admin");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250).IsRequired(false);

                entity.Property(e => e.BirthDay).HasColumnType("date").IsRequired(false);

                entity.Property(e => e.Description).HasMaxLength(250).IsRequired(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.BookCategoryId).HasColumnName("CategoryId");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorId");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherId");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Image).HasMaxLength(500);
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.ToTable("BookCategory"); 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250).IsRequired(false);

                entity.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.BookId).HasColumnName("BookId");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.NumberPhone).HasMaxLength(10);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.Address).HasMaxLength(500);
            });



            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CustomerAccount");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);

                entity.HasOne(d => d.CustomerIdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerAccount_Customer");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.BookId).HasColumnName("BookId");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.Property(e => e.CreatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<Pay>(entity =>
            {
                entity.ToTable("Pay");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("OrderId");

                entity.Property(e => e.PaymentMethod).HasMaxLength(10);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("News");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50).IsUnicode(false).HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.Content).HasMaxLength(800);

                entity.Property(e => e.Image).HasMaxLength(500);
            });
        }
    }
}
