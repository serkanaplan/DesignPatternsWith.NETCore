﻿

using System.Drawing;

namespace WebApp.Adapter.DP.Services;

public interface IAdvanceImageProcess
{
    void AddWatermarkImage(Stream stream, string text, string filePath, Color color, Color outlineColor);
}