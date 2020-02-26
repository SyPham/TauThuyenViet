using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models
{
    public partial class DBContext : DbContext
    {
        public DBContext( DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountCategory> AccountCategories { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<ArticleMainCategory> ArticleMainCategories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactCategory> ContactCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductMainCategory> ProductMainCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Username).ValueGeneratedNever();

                entity.HasOne(d => d.AccountCategory)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountCategoryID)
                    .HasConstraintName("FK_ACCOUNT_ACCOUNTTY_ACCOUNTT");
            });

            modelBuilder.Entity<AccountCategory>(entity =>
            {
                entity.Property(e => e.AccountCategoryID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasOne(d => d.ArticleCategory)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleCategoryID)
                    .HasConstraintName("FK_ARTICLE_ARTICLECA_ARTICLEC");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLE_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasOne(d => d.ArticleMainCategory)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.ArticleMainCategoryID)
                    .HasConstraintName("FK_ARTICLEC_ARTICLEMA_ARTICLEM");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLEC_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<ArticleMainCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArticleMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ARTICLEM_ACCOUNT_A_ACCOUNT");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasOne(d => d.ApproveByNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ApproveBy)
                    .HasConstraintName("FK_Contact_Account");

                entity.HasOne(d => d.ContactCategory)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactCategoryID)
                    .HasConstraintName("FK_Contact_ContactCategory");
            });

            modelBuilder.Entity<ContactCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ContactCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ContactCategory_Account");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_Product_Account");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryID)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ProductCategory_Account");

                entity.HasOne(d => d.ProductMainCategory)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductMainCategoryID)
                    .HasConstraintName("FK_ProductCategory_ProductMainCategory");
            });

            modelBuilder.Entity<ProductMainCategory>(entity =>
            {
                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ProductMainCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK_ProductMainCategory_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
