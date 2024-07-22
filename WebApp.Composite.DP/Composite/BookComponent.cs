
namespace WebApp.Composite.DP.Composite;

public class BookComponent(int id, string name) : IComponent
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;

    public int Count() => 1;

    public string Display() => $"<li class='list-group-item'>{Name}</li>";
}
