namespace TmBugun.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yayimci")]
    public partial class Yayimci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yayimci()
        {
            Haber = new HashSet<Haber>();
            Resimler = new HashSet<Resimler>();
            Sport = new HashSet<Sport>();
            Videolar = new HashSet<Videolar>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Yayimci_ad { get; set; }

        [Required]
        [StringLength(50)]
        public string Yayimci_soyad { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Yayimci_eposta { get; set; }

        [Required]
        [StringLength(50)]
        public string Yayimci_sifre { get; set; }

        public byte[] YayimciResim { get; set; }
        [Display(Name = "Yayimci Yas")]
        public int YayimciYas { get; set; }
        [Display(Name = "Yayimci Facebook")]

        public string YayimciFacebook { get; set; }
        [Display(Name = "Yayimci Twitter")]

        public string YayimciTwitter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Haber> Haber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resimler> Resimler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sport> Sport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Videolar> Videolar { get; set; }
    }
}
