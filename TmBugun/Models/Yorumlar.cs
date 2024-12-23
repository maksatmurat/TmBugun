namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Yorumlar")]
    public partial class Yorumlar
    {
        public int Id { get; set; }

        public int? HaberID { get; set; }

        public int? SportID { get; set; }

        public int? ResimID { get; set; }

        public int? YorumBegSayi { get; set; }

        [Required]
        [AllowHtml]

        public string Yorum { get; set; }

        public int KullaniciID { get; set; }

        public DateTime YorumTarih { get; set; }

        public virtual Haber Haber { get; set; }

        public virtual NormalKullanici NormalKullanici { get; set; }

        public virtual Resimler Resimler { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
