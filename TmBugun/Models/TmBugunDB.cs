namespace TmBugun.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TmBugunDB : DbContext
    {
        public TmBugunDB()
            : base("name=TmBugunDB")
        {
        }

        //public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AdminBilgileri> AdminBilgileri { get; set; }
        public virtual DbSet<Haber> Haber { get; set; }
        public virtual DbSet<HaberBlogTip> HaberBlogTip { get; set; }
        public virtual DbSet<HaberKategorileri> HaberKategorileri { get; set; }
        public virtual DbSet<IletisimBilgileri> IletisimBilgileri { get; set; }
        public virtual DbSet<NormalKullanici> NormalKullanici { get; set; }
        public virtual DbSet<Resimler> Resimler { get; set; }
        public virtual DbSet<Sport> Sport { get; set; }
        public virtual DbSet<SportBlogTip> SportBlogTip { get; set; }
        public virtual DbSet<SportKategorileri> SportKategorileri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Videolar> Videolar { get; set; }
        public virtual DbSet<Yayimci> Yayimci { get; set; }
        public virtual DbSet<Yorumlar> Yorumlar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HaberBlogTip>()
                .HasMany(e => e.Haber)
                .WithRequired(e => e.HaberBlogTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HaberKategorileri>()
                .HasMany(e => e.Haber)
                .WithRequired(e => e.HaberKategorileri)
                .HasForeignKey(e => e.HaberkategoriId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NormalKullanici>()
                .HasMany(e => e.Yorumlar)
                .WithRequired(e => e.NormalKullanici)
                .HasForeignKey(e => e.KullaniciID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resimler>()
                .HasMany(e => e.Yorumlar)
                .WithOptional(e => e.Resimler)
                .HasForeignKey(e => e.ResimID);

            modelBuilder.Entity<SportBlogTip>()
                .HasMany(e => e.Sport)
                .WithRequired(e => e.SportBlogTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SportKategorileri>()
                .HasMany(e => e.Sport)
                .WithRequired(e => e.SportKategorileri)
                .HasForeignKey(e => e.SportkategoriId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yayimci>()
                .HasMany(e => e.Haber)
                .WithRequired(e => e.Yayimci)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yayimci>()
                .HasMany(e => e.Resimler)
                .WithRequired(e => e.Yayimci)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yayimci>()
                .HasMany(e => e.Sport)
                .WithRequired(e => e.Yayimci)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yayimci>()
                .HasMany(e => e.Videolar)
                .WithRequired(e => e.Yayimci)
                .WillCascadeOnDelete(false);
        }
    }
}
