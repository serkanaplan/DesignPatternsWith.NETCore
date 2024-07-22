# Template Method Design Pattern

Template Method Design Pattern, bir algoritmanın iskeletini bir şablon (template) yöntemi içerisinde tanımlayan ve bazı adımların alt sınıflar tarafından sağlanmasını zorunlu kılan bir davranışsal tasarım desenidir. Bu desen, bir algoritmanın adımlarını sabitlerken, alt sınıfların bu adımları özelleştirmesine izin verir.

## Temel Bileşenler

1. **AbstractClass (Soyut Sınıf)**: Şablon yöntemi tanımlar ve bazı adımların gerçekleştirilmesi için soyut yöntemler içerir.
2. **ConcreteClass (Beton Sınıf)**: Soyut sınıfın eksik adımlarını implemente eder.

## Örnek

Farklı türlerde veri işleme işlemleri olan bir uygulama düşünelim. Her veri işleme işlemi aynı adımları takip eder, ancak bazı adımlar veri türüne göre değişir.

```csharp
public abstract class DataProcessor
{
    public void ProcessData()
    {
        LoadData();
        Process();
        SaveData();
    }

    protected abstract void LoadData();
    protected abstract void Process();
    protected virtual void SaveData()
    {
        Console.WriteLine("Data saved to database.");
    }
}

public class CsvDataProcessor : DataProcessor
{
    protected override void LoadData()
    {
        Console.WriteLine("Data loaded from CSV file.");
    }

    protected override void Process()
    {
        Console.WriteLine("Data processed for CSV file.");
    }
}

public class JsonDataProcessor : DataProcessor
{
    protected override void LoadData()
    {
        Console.WriteLine("Data loaded from JSON file.");
    }

    protected override void Process()
    {
        Console.WriteLine("Data processed for JSON file.");
    }
}

var csvProcessor = new CsvDataProcessor();
csvProcessor.ProcessData();
// Output:
// Data loaded from CSV file.
// Data processed for CSV file.
// Data saved to database.

var jsonProcessor = new JsonDataProcessor();
jsonProcessor.ProcessData();
// Output:
// Data loaded from JSON file.
// Data processed for JSON file.
// Data saved to database.
```

- Bu örnekte, DataProcessor sınıfı veri işleme algoritmasının iskeletini tanımlar. Alt sınıflar olan CsvDataProcessor ve JsonDataProcessor, veri yükleme ve işleme adımlarını özelleştirir. Veri kaydetme adımı ise varsayılan olarak DataProcessor sınıfında tanımlanmıştır, ancak alt sınıflar bu adımı da özelleştirebilir.

