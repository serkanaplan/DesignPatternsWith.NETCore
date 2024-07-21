# Kategori

Davranışsal(Behavioral)

# Strategy Design Pattern

Strategy Design Pattern, yazılım geliştirme sürecinde farklı algoritmaların birbirinin yerine kullanılabilmesini sağlayan bir davranışsal tasarım desenidir. Bu desen, bir sınıfın davranışını, algoritmasını veya stratejisini çalışma zamanında dinamik olarak değiştirebilmek için kullanılır.
Kısaca runtime esnasında bir objenin davranışını değiştirmemize imkan verir

## Temel Bileşenler

1. **Strategy (Strateji) Arayüzü**: Ortak algoritma arayüzünü tanımlar.
2. **ConcreteStrategy (Beton Strateji) Sınıfları**: Bu arayüzü implemente eden ve farklı algoritmaları sağlayan sınıflardır.
3. **Context (Bağlam) Sınıfı**: Strateji nesnesi ile etkileşime girer. Algoritmanın kullanımını kapsar ve istemcinin bu nesneleri oluşturmasına izin verir.

## Örnek

kullanıcı Crud operasyonları için istediği veritabanını en baştan seçebilir ve işlemler buna göre yapılır
böylece derleme zamanında statik olarak tanımlanan veritabanı çalışma zamanında dinamik olarak seçilebiir hale gelir