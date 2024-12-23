namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Sport")]
    public partial class Sport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sport()
        {
            Yorumlar = new HashSet<Yorumlar>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Sport Baþlýðý")]

        public string SportBaslik { get; set; }

        [Required]
        [DisplayName("Sport Ýçeriði")]
        [AllowHtml]
        public string SportIcerik { get; set; }
        [DisplayName("Ýlan Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime SportTarih { get; set; }

        public int SportIzlenmeSayi { get; set; }

        public int SportBegenmeSayi { get; set; }

        public int SportkategoriId { get; set; }

        public int SportBlogTipId { get; set; }

        public byte[] SportResim { get; set; }

        public int YayimciId { get; set; }

        public bool? SportDurum { get; set; }

        [Required]
        [DisplayName("Sport Baþlýðý EN")]
        public string SportBaslikEN { get; set; }
        [DisplayName("Sport Ýçeriði EN")]
        [Required]
        [AllowHtml]
        public string SportIcerikEN { get; set; }

        [Required]
        [DisplayName("Sport Baþlýðý RU")]
        public string SportBaslikRU { get; set; }
        [DisplayName("Sport Ýçeriði RU")]
        [Required]
        [AllowHtml]
        public string SportIcerikRU { get; set; }

        [Required]
        [DisplayName("Sport Baþlýðý TR")]
        public string SportBaslikTR { get; set; }

        [DisplayName("Sport Ýçeriði TR")]
        [Required]
        [AllowHtml]
        public string SportIcerikTR { get; set; }

        public virtual SportBlogTip SportBlogTip { get; set; }

        public virtual SportKategorileri SportKategorileri { get; set; }

        public virtual Yayimci Yayimci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlar { get; set; }
    }
}
