namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminBilgileri")]
    public partial class AdminBilgileri
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AdminAd { get; set; }

        [Required]
        [StringLength(50)]
        public string Adminsifre { get; set; }
    }
}
