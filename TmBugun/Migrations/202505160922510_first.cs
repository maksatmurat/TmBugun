namespace TmBugun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminBilgileri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminAd = c.String(nullable: false, maxLength: 50),
                        Adminsifre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Haber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HaberBaslik = c.String(nullable: false, maxLength: 2147483647),
                        HaberIcerik = c.String(nullable: false, maxLength: 2147483647),
                        HaberTarih = c.DateTime(nullable: false),
                        HaberIzlenmeSayi = c.Int(nullable: false),
                        HaberBegenmeSayi = c.Int(nullable: false),
                        HaberkategoriId = c.Int(nullable: false),
                        HaberBlogTipId = c.Int(nullable: false),
                        HaberResim = c.Binary(),
                        YayimciId = c.Int(nullable: false),
                        HaberDurum = c.Boolean(),
                        HaberBaslikEN = c.String(nullable: false, maxLength: 2147483647),
                        HaberIcerikEN = c.String(nullable: false, maxLength: 2147483647),
                        HaberBaslikRU = c.String(nullable: false, maxLength: 2147483647),
                        HaberIcerikRU = c.String(nullable: false, maxLength: 2147483647),
                        HaberBaslikTR = c.String(nullable: false, maxLength: 2147483647),
                        HaberIcerikTR = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HaberBlogTip", t => t.HaberBlogTipId)
                .ForeignKey("dbo.HaberKategorileri", t => t.HaberkategoriId)
                .ForeignKey("dbo.Yayimci", t => t.YayimciId)
                .Index(t => t.HaberkategoriId)
                .Index(t => t.HaberBlogTipId)
                .Index(t => t.YayimciId);
            
            CreateTable(
                "dbo.HaberBlogTip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HaberBlogTipi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HaberKategorileri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HaberKategori = c.String(nullable: false, maxLength: 50),
                        HaberKategoriLogo = c.String(maxLength: 2147483647),
                        HaberKategoriEN = c.String(maxLength: 50),
                        HaberKategoriRU = c.String(maxLength: 50),
                        HaberKategoriTR = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yayimci",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Yayimci_ad = c.String(nullable: false, maxLength: 50),
                        Yayimci_soyad = c.String(nullable: false, maxLength: 50),
                        Yayimci_eposta = c.String(nullable: false, maxLength: 2147483647),
                        Yayimci_sifre = c.String(nullable: false, maxLength: 50),
                        YayimciResim = c.Binary(),
                        YayimciYas = c.Int(nullable: false),
                        YayimciFacebook = c.String(maxLength: 2147483647),
                        YayimciTwitter = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resimler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResimYazi = c.String(nullable: false, maxLength: 2147483647),
                        ResimBegSayi = c.Int(nullable: false),
                        YayimciId = c.Int(nullable: false),
                        ResimEklenmeTarih = c.DateTime(nullable: false),
                        Resim = c.Binary(),
                        ResimYaziEN = c.String(nullable: false, maxLength: 2147483647),
                        ResimYaziRU = c.String(nullable: false, maxLength: 2147483647),
                        ResimYaziTR = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Yayimci", t => t.YayimciId)
                .Index(t => t.YayimciId);
            
            CreateTable(
                "dbo.Yorumlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HaberID = c.Int(),
                        SportID = c.Int(),
                        ResimID = c.Int(),
                        YorumBegSayi = c.Int(),
                        Yorum = c.String(nullable: false, maxLength: 2147483647),
                        KullaniciID = c.Int(nullable: false),
                        YorumTarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Haber", t => t.HaberID)
                .ForeignKey("dbo.NormalKullanici", t => t.KullaniciID)
                .ForeignKey("dbo.Sport", t => t.SportID)
                .ForeignKey("dbo.Resimler", t => t.ResimID)
                .Index(t => t.HaberID)
                .Index(t => t.SportID)
                .Index(t => t.ResimID)
                .Index(t => t.KullaniciID);
            
            CreateTable(
                "dbo.NormalKullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(maxLength: 50),
                        KullaniciAd = c.String(nullable: false, maxLength: 50),
                        sifre = c.String(nullable: false, maxLength: 20),
                        yas = c.DateTime(),
                        NormalKullanici_eposta = c.String(nullable: false, maxLength: 50),
                        kullaniciResim = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportBaslik = c.String(nullable: false, maxLength: 2147483647),
                        SportIcerik = c.String(nullable: false, maxLength: 2147483647),
                        SportTarih = c.DateTime(nullable: false),
                        SportIzlenmeSayi = c.Int(nullable: false),
                        SportBegenmeSayi = c.Int(nullable: false),
                        SportkategoriId = c.Int(nullable: false),
                        SportBlogTipId = c.Int(nullable: false),
                        SportResim = c.Binary(),
                        YayimciId = c.Int(nullable: false),
                        SportDurum = c.Boolean(),
                        SportBaslikEN = c.String(nullable: false, maxLength: 2147483647),
                        SportIcerikEN = c.String(nullable: false, maxLength: 2147483647),
                        SportBaslikRU = c.String(nullable: false, maxLength: 2147483647),
                        SportIcerikRU = c.String(nullable: false, maxLength: 2147483647),
                        SportBaslikTR = c.String(nullable: false, maxLength: 2147483647),
                        SportIcerikTR = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SportBlogTip", t => t.SportBlogTipId)
                .ForeignKey("dbo.SportKategorileri", t => t.SportkategoriId)
                .ForeignKey("dbo.Yayimci", t => t.YayimciId)
                .Index(t => t.SportkategoriId)
                .Index(t => t.SportBlogTipId)
                .Index(t => t.YayimciId);
            
            CreateTable(
                "dbo.SportBlogTip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportBlogTipi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SportKategorileri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SportKategori = c.String(nullable: false, maxLength: 50),
                        SportKategoriLogo = c.String(maxLength: 2147483647),
                        SportKategoriEN = c.String(maxLength: 50),
                        SportKategoriRU = c.String(maxLength: 50),
                        SportKategoriTR = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Videolar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoBasligi = c.String(nullable: false, maxLength: 2147483647),
                        VideoBegeniSayi = c.Int(nullable: false),
                        VideoEklenmeTarih = c.DateTime(nullable: false),
                        VideoYol = c.String(nullable: false, maxLength: 2147483647),
                        YayimciId = c.Int(nullable: false),
                        VideoBasligiEN = c.String(nullable: false, maxLength: 2147483647),
                        VideoBasligiRU = c.String(nullable: false, maxLength: 2147483647),
                        VideoBasligiTR = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Yayimci", t => t.YayimciId)
                .Index(t => t.YayimciId);
            
            CreateTable(
                "dbo.IletisimBilgileri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Facebook = c.String(nullable: false, maxLength: 2147483647),
                        Twitter = c.String(nullable: false, maxLength: 2147483647),
                        Eposta = c.String(nullable: false, maxLength: 2147483647),
                        Adres = c.String(nullable: false, maxLength: 2147483647),
                        Hakkimizda = c.String(nullable: false, maxLength: 2147483647),
                        Instagram = c.String(nullable: false, maxLength: 2147483647),
                        HakkimizdaEN = c.String(nullable: false, maxLength: 2147483647),
                        HakkimizdaRU = c.String(nullable: false, maxLength: 2147483647),
                        HakkimizdaTR = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videolar", "YayimciId", "dbo.Yayimci");
            DropForeignKey("dbo.Sport", "YayimciId", "dbo.Yayimci");
            DropForeignKey("dbo.Resimler", "YayimciId", "dbo.Yayimci");
            DropForeignKey("dbo.Yorumlar", "ResimID", "dbo.Resimler");
            DropForeignKey("dbo.Yorumlar", "SportID", "dbo.Sport");
            DropForeignKey("dbo.Sport", "SportkategoriId", "dbo.SportKategorileri");
            DropForeignKey("dbo.Sport", "SportBlogTipId", "dbo.SportBlogTip");
            DropForeignKey("dbo.Yorumlar", "KullaniciID", "dbo.NormalKullanici");
            DropForeignKey("dbo.Yorumlar", "HaberID", "dbo.Haber");
            DropForeignKey("dbo.Haber", "YayimciId", "dbo.Yayimci");
            DropForeignKey("dbo.Haber", "HaberkategoriId", "dbo.HaberKategorileri");
            DropForeignKey("dbo.Haber", "HaberBlogTipId", "dbo.HaberBlogTip");
            DropIndex("dbo.Videolar", new[] { "YayimciId" });
            DropIndex("dbo.Sport", new[] { "YayimciId" });
            DropIndex("dbo.Sport", new[] { "SportBlogTipId" });
            DropIndex("dbo.Sport", new[] { "SportkategoriId" });
            DropIndex("dbo.Yorumlar", new[] { "KullaniciID" });
            DropIndex("dbo.Yorumlar", new[] { "ResimID" });
            DropIndex("dbo.Yorumlar", new[] { "SportID" });
            DropIndex("dbo.Yorumlar", new[] { "HaberID" });
            DropIndex("dbo.Resimler", new[] { "YayimciId" });
            DropIndex("dbo.Haber", new[] { "YayimciId" });
            DropIndex("dbo.Haber", new[] { "HaberBlogTipId" });
            DropIndex("dbo.Haber", new[] { "HaberkategoriId" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.IletisimBilgileri");
            DropTable("dbo.Videolar");
            DropTable("dbo.SportKategorileri");
            DropTable("dbo.SportBlogTip");
            DropTable("dbo.Sport");
            DropTable("dbo.NormalKullanici");
            DropTable("dbo.Yorumlar");
            DropTable("dbo.Resimler");
            DropTable("dbo.Yayimci");
            DropTable("dbo.HaberKategorileri");
            DropTable("dbo.HaberBlogTip");
            DropTable("dbo.Haber");
            DropTable("dbo.AdminBilgileri");
        }
    }
}
