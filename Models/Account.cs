using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
[Table("Account")]
    public partial class Account
    {
        public Account()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
            ArticleMainCategories = new HashSet<ArticleMainCategory>();
            Articles = new HashSet<Article>();
            ContactCategories = new HashSet<ContactCategory>();
            Contacts = new HashSet<Contact>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductMainCategories = new HashSet<ProductMainCategory>();
            Products = new HashSet<Product>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [Column("AccountCategoryID")]
        [StringLength(50)]
        public string AccountCategoryID { get; set; }

        [ForeignKey("AccountCategoryId")]
        [InverseProperty("Accounts")]
        public AccountCategory AccountCategory { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<ArticleMainCategory> ArticleMainCategories { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<Article> Articles { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<ContactCategory> ContactCategories { get; set; }
        [InverseProperty("ApproveByNavigation")]
        public ICollection<Contact> Contacts { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<ProductCategory> ProductCategories { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<ProductMainCategory> ProductMainCategories { get; set; }
        [InverseProperty("CreateByNavigation")]
        public ICollection<Product> Products { get; set; }
    }
}
