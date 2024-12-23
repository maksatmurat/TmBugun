namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Videolar")]
    public partial class Videolar
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string VideoBasligi { get; set; }

        public int VideoBegeniSayi { get; set; }

        public DateTime VideoEklenmeTarih { get; set; }

        [Required]
        public string VideoYol { get; set; }

        public int YayimciId { get; set; }

        [Required]
        [AllowHtml]

        public string VideoBasligiEN { get; set; }

        [Required]
        [AllowHtml]

        public string VideoBasligiRU { get; set; }

        [Required]
        [AllowHtml]

        public string VideoBasligiTR { get; set; }

        public virtual Yayimci Yayimci { get; set; }
    }
}
