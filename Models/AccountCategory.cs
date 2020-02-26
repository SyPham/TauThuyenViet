using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
[Table("AccountCategory")]
    public partial class AccountCategory
    {
        public AccountCategory()
        {
            Accounts = new HashSet<Account>();
        }

        [Column("AccountCategoryID")]
        [StringLength(50)]
        public string AccountCategoryID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }

        [InverseProperty("AccountCategory")]
        public ICollection<Account> Accounts { get; set; }
    }
}
