using Microsoft.AspNetCore.Mvc;


namespace WebApp.Command.DP.Commands;

public class FileCreateInvoker
{
    private ITableActionCommand _tableActionCommand;
    private List<ITableActionCommand> tableActionCommands = new List<ITableActionCommand>();

    public void SetCommand(ITableActionCommand tableActionCommand) => _tableActionCommand = tableActionCommand;

    public void AddCommand(ITableActionCommand tableActionCommand) => tableActionCommands.Add(tableActionCommand);

    public IActionResult CreateFile() => _tableActionCommand.Execute();

    public List<IActionResult> CreateFiles() => tableActionCommands.Select(x => x.Execute()).ToList();
}
