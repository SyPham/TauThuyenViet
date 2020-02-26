using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
[Table("ProductCategory")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Column("ProductCategoryID")]
        public int ProductCategoryID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        [Column("ProductMainCategoryID")]
        public int? ProductMainCategoryID { get; set; }

        [ForeignKey("CreateBy")]
        [InverseProperty("ProductCategories")]
        public Account CreateByNavigation { get; set; }
        [ForeignKey("ProductMainCategoryId")]
        [InverseProperty("ProductCategories")]
        public ProductMainCategory ProductMainCategory { get; set; }
        [InverseProperty("ProductCategory")]
        public ICollection<Product> Products { get; set; }
    }
}
