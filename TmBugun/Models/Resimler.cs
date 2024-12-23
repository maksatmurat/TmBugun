namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Resimler")]
    public partial class Resimler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resimler()
        {
            Yorumlar = new HashSet<Yorumlar>();
        }

        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string ResimYazi { get; set; }

        public int ResimBegSayi { get; set; }

        public int YayimciId { get; set; }

        public DateTime ResimEklenmeTarih { get; set; }

        public byte[] Resim { get; set; }

        [Required]
        [AllowHtml]
        public string ResimYaziEN { get; set; }

        [Required]
        [AllowHtml]
        public string ResimYaziRU { get; set; }

        [Required]
        [AllowHtml]
        public string ResimYaziTR { get; set; }

        public virtual Yayimci Yayimci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlar { get; set; }
    }
}
