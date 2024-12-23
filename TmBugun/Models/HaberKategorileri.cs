namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HaberKategorileri")]
    public partial class HaberKategorileri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HaberKategorileri()
        {
            Haber = new HashSet<Haber>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string HaberKategori { get; set; }

        public string HaberKategoriLogo { get; set; }

        [StringLength(50)]
        public string HaberKategoriEN { get; set; }

        [StringLength(50)]
        public string HaberKategoriRU { get; set; }

        [StringLength(50)]
        public string HaberKategoriTR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Haber> Haber { get; set; }
    }
}
