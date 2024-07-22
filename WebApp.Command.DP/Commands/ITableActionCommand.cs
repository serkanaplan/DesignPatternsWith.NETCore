using Microsoft.AspNetCore.Mvc;


namespace WebApp.Command.DP.Commands;

public interface ITableActionCommand
{
    IActionResult Execute();
}
