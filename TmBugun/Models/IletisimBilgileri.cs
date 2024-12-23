namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("IletisimBilgileri")]
    public partial class IletisimBilgileri
    {
        public int Id { get; set; }

        [Required]
        public string Facebook { get; set; }

        [Required]
        public string Twitter { get; set; }

        [Required]
        public string Eposta { get; set; }

        [Required]
        [AllowHtml]
        public string Adres { get; set; }

        [Required]
        [AllowHtml]
        public string Hakkimizda { get; set; }

        [Required]
        public string Instagram { get; set; }

        [Required]
        [AllowHtml]
        public string HakkimizdaEN { get; set; }

        [Required]
        [AllowHtml]
        public string HakkimizdaRU { get; set; }

        [Required]
        [AllowHtml]
        public string HakkimizdaTR { get; set; }
    }
}
