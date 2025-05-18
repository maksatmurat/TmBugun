using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.IO;

namespace TmBugun.Models
{
    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6",
    SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }

    public partial class TmBugunDB : DbContext
    {


        public TmBugunDB() : base("name=DefaultConnection")
            {
            }

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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Чтобы имена таблиц не менялись на множественное число

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
