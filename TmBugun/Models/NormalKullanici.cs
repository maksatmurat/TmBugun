namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NormalKullanici")]
    public partial class NormalKullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NormalKullanici()
        {
            Yorumlar = new HashSet<Yorumlar>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string Soyad { get; set; }

        [Required]
        [StringLength(50)]
        public string KullaniciAd { get; set; }

        [Required]
        [StringLength(20)]
        public string sifre { get; set; }

        public DateTime? yas { get; set; }

        [Required]
        [StringLength(50)]
        public string NormalKullanici_eposta { get; set; }

        public byte[] kullaniciResim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlar { get; set; }
    }
}
