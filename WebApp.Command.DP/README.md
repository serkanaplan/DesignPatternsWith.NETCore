# Command Design Pattern

Command Design Pattern, bir talebin (command) nesne olarak kapsüllenmesini sağlayan ve bu şekilde farklı istemcilerin talepleri parametrize edebilmesine olanak tanıyan bir davranışsal tasarım desenidir. Bu desen, istemci ile alıcı (receiver) arasındaki bağımlılığı azaltarak, talebin yürütülme zamanını ve yerini belirleyebilmenizi sağlar.

## Temel Bileşenler

1. **Command (Komut)**: Bir talebi temsil eden soyut sınıf veya arayüz.
2. **ConcreteCommand (Beton Komut)**: Komut arayüzünü uygulayan ve talebi belirli bir alıcıya ileten sınıf.
3. **Receiver (Alıcı)**: Komutun çalıştırılacağı gerçek işlemi gerçekleştiren sınıf.
4. **Invoker (Çağırıcı)**: Komutu çağıran sınıf.
5. **Client (İstemci)**: Komutları ve alıcıları oluşturan sınıf.

## Örnek

Bir örnekle açıklayalım: Bir uzaktan kumanda uygulaması düşünelim. Bu uygulama, ışıkları açmak veya kapatmak gibi komutları içerir.

### Command Arayüzü

```csharp
public interface ICommand
{
    void Execute();
    void Undo();
}
```
## ConcreteCommand Sınıfları

```csharp
public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }

    public void Undo()
    {
        _light.Off();
    }
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Off();
    }

    public void Undo()
    {
        _light.On();
    }
}
```

## Receiver Sınıfı

```csharp
public class Light
{
    public void On()
    {
        Console.WriteLine("Light is On");
    }

    public void Off()
    {
        Console.WriteLine("Light is Off");
    }
}
```
## Invoker Sınıfı

```csharp   
public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }

    public void PressUndoButton()
    {
        _command.Undo();
    }
}
```
## Kullanımı

```csharp
var light = new Light();
var lightOnCommand = new LightOnCommand(light);
var lightOffCommand = new LightOffCommand(light);

var remoteControl = new RemoteControl();

remoteControl.SetCommand(lightOnCommand);
remoteControl.PressButton();  // Output: Light is On

remoteControl.SetCommand(lightOffCommand);
remoteControl.PressButton();  // Output: Light is Off

remoteControl.PressUndoButton();  // Output: Light is On
```

- Bu örnekte, ICommand arayüzü komutları tanımlar. LightOnCommand ve LightOffCommand sınıfları, ICommand arayüzünü uygulayarak ışıkları açma ve kapatma işlemlerini gerçekleştirir. Light sınıfı komutların çalıştırılacağı gerçek işlemleri içerir. RemoteControl sınıfı ise komutları çağırır.