namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Haber")]
    public partial class Haber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Haber()
        {
            Yorumlar = new HashSet<Yorumlar>();
        }

        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("Haber Baþlýðý")]
        public string HaberBaslik { get; set; }

        [DisplayName("Haber Ýçeriði")]
        [Required]
        [AllowHtml]
        public string HaberIcerik { get; set; }

        public DateTime HaberTarih { get; set; }

        public int HaberIzlenmeSayi { get; set; }

        public int HaberBegenmeSayi { get; set; }

        public int HaberkategoriId { get; set; }

        public int HaberBlogTipId { get; set; }

        public byte[] HaberResim { get; set; }

        public int YayimciId { get; set; }

        public bool? HaberDurum { get; set; }
        [Required]
        [AllowHtml]
        [DisplayName("Haber Baþlýðý EN")]
        public string HaberBaslikEN { get; set; }
        [DisplayName("Haber Ýçeriði EN")]
        [Required]
        [AllowHtml]
        public string HaberIcerikEN { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("Haber Baþlýðý RU")]
        public string HaberBaslikRU { get; set; }

        [DisplayName("Haber Ýçeriði RU")]
        [Required]
        [AllowHtml]
        public string HaberIcerikRU { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("Haber Baþlýðý TR")]
        public string HaberBaslikTR { get; set; }
        [DisplayName("Haber Ýçeriði TR")]
        [Required]
        [AllowHtml]
        public string HaberIcerikTR { get; set; }

        public virtual HaberBlogTip HaberBlogTip { get; set; }

        public virtual HaberKategorileri HaberKategorileri { get; set; }

        public virtual Yayimci Yayimci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlar { get; set; }
    }
}
