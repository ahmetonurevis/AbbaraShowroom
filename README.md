# AbbaraShowroom

AbbaraShowroom, urunlerin kategori, gorsel, fiyat ve one cikan ozellikleriyle sergilendigi; yonetim paneli uzerinden iceriklerin kontrol edildigi ASP.NET Core MVC showroom uygulamasidir.

## Ozellikler

- Ana sayfa showroom vitrin yapisi
- Urun listeleme ve detay icerikleri
- Kategori yonetimi
- Slider yonetimi
- Mesaj/iletisim kayitlari
- Admin kullanici tablosu
- Urun gorseli yukleme ve yayinlama
- Entity Framework Core migration yapisi

## Teknolojiler

- ASP.NET Core MVC
- .NET 9
- Entity Framework Core 9
- SQL Server
- Razor Views
- Bootstrap ve jQuery

## Proje Yapisi

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

## Kurulum

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

## Gelistirme Notlari

Yonetim panelindeki CRUD ekranlari controller ve Razor view katmanlariyla ayrilmistir. Yeni vitrin alanlari eklerken once model ve migration, ardindan controller/view guncellemesi yapilmalidir.
