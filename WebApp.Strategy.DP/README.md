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

Farklı indirim stratejileri olan bir e-ticaret uygulaması düşünelim. Kullanıcıya belirli koşullara göre farklı indirimler uygulanabilir.

### Strategy Arayüzü

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price)
    {
        return price;
    }
}

public class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _percentage;
    
    public PercentageDiscountStrategy(decimal percentage)
    {
        _percentage = percentage;
    }
    
    public decimal ApplyDiscount(decimal price)
    {
        return price - (price * _percentage / 100);
    }
}

public class FixedDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _fixedAmount;
    
    public FixedDiscountStrategy(decimal fixedAmount)
    {
        _fixedAmount = fixedAmount;
    }
    
    public decimal ApplyDiscount(decimal price)
    {
        return price - _fixedAmount;
    }
}

public class ShoppingCart
{
    private IDiscountStrategy _discountStrategy;
    
    public ShoppingCart(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
    
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
    
    public decimal CalculateTotalPrice(decimal price)
    {
        return _discountStrategy.ApplyDiscount(price);
    }
}

var cart = new ShoppingCart(new NoDiscountStrategy());
Console.WriteLine(cart.CalculateTotalPrice(100)); // Output: 100

cart.SetDiscountStrategy(new PercentageDiscountStrategy(10));
Console.WriteLine(cart.CalculateTotalPrice(100)); // Output: 90

cart.SetDiscountStrategy(new FixedDiscountStrategy(15));
Console.WriteLine(cart.CalculateTotalPrice(100)); // Output: 85
```
- Bu örnekte, ShoppingCart sınıfı herhangi bir indirim stratejisi kullanabilir ve çalışma zamanında farklı stratejiler arasında geçiş yapabilir. Bu sayede kodunuz esnek ve genişletilebilir olur. Strategy Design Pattern, kodunuzu daha modüler ve yönetilebilir hale getirir, çünkü farklı algoritmaları sınıflar arasında bölerek her birinin sorumluluklarını ayırır.