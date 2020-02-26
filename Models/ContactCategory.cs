using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
[Table("ContactCategory")]
    public partial class ContactCategory
    {
        public ContactCategory()
        {
            Contacts = new HashSet<Contact>();
        }

        [Column("ContactCategoryID")]
        public int ContactCategoryID { get; set; }
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

        [ForeignKey("CreateBy")]
        [InverseProperty("ContactCategories")]
        public Account CreateByNavigation { get; set; }
        [InverseProperty("ContactCategory")]
        public ICollection<Contact> Contacts { get; set; }
    }
}
