
using System.Drawing;

namespace WebApp.Adapter.DP.Services;
public class AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess) : IImageProcess
{
    private readonly IAdvanceImageProcess _advanceImageProcess = advanceImageProcess;

    public void AddWatermark(string text, string filename, Stream imageStream)
    {
        _advanceImageProcess.AddWatermarkImage(imageStream, text, $"wwwroot/watermarks/{filename}", Color.FromArgb(128, 255, 255, 255), Color.FromArgb(0, 255, 255, 255));
    }
}