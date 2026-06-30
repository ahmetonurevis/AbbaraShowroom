<div align="center">

# 🛋️ AbbaraShowroom

**Showroom ürünlerini kategori, slider ve yönetim paneliyle sunan ASP.NET Core MVC uygulaması**

![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-68217A?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)

</div>

---

AbbaraShowroom, urunlerin kategori, gorsel, fiyat ve one cikan ozellikleriyle sergilendigi; yonetim paneli uzerinden iceriklerin kontrol edildigi ASP.NET Core MVC showroom uygulamasidir.

## ✨ Özellikler

- Ana sayfa showroom vitrin yapisi
- Urun listeleme ve detay icerikleri
- Kategori yonetimi
- Slider yonetimi
- Mesaj/iletisim kayitlari
- Admin kullanici tablosu
- Urun gorseli yukleme ve yayinlama
- Entity Framework Core migration yapisi

## 🧱 Teknoloji Yığını

- ASP.NET Core MVC
- .NET 9
- Entity Framework Core 9
- SQL Server
- Razor Views
- Bootstrap ve jQuery

## 📁 Proje Yapısı

```text
AbbaraShowroom/
├── AbbaraShowroom.sln
└── AbbaraShowroom/
    ├── Controllers/     # MVC controller katmani
    ├── Models/          # Product, Category, Slider, Message modelleri
    ├── Context/         # AppDbContext
    ├── Migrations/      # EF Core migration dosyalari
    ├── Views/           # Razor sayfalari
    └── wwwroot/         # CSS, JS ve yuklenen gorseller
```

## 🚀 Kurulum

1. Repoyu klonlayin.
2. Visual Studio veya .NET CLI ile cozum dosyasini acin.
3. `AbbaraShowroom/appsettings.json` icindeki SQL Server connection string degerini kendi ortaminiza gore ayarlayin.
4. Veritabanini olusturun:

```bash
dotnet ef database update --project AbbaraShowroom/AbbaraShowroom.csproj
```

5. Uygulamayi calistirin:

```bash
dotnet run --project AbbaraShowroom/AbbaraShowroom.csproj
```

## 🛠️ Geliştirme Notları

Yonetim panelindeki CRUD ekranlari controller ve Razor view katmanlariyla ayrilmistir. Yeni vitrin alanlari eklerken once model ve migration, ardindan controller/view guncellemesi yapilmalidir.

---

<div align="center">
<sub>Bu proje özenle dokümante edildi. 🚀</sub>
</div>
