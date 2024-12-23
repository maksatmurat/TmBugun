namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SportKategorileri")]
    public partial class SportKategorileri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SportKategorileri()
        {
            Sport = new HashSet<Sport>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SportKategori { get; set; }

        public string SportKategoriLogo { get; set; }

        [StringLength(50)]
        public string SportKategoriEN { get; set; }

        [StringLength(50)]
        public string SportKategoriRU { get; set; }

        [StringLength(50)]
        public string SportKategoriTR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sport> Sport { get; set; }
    }
}
