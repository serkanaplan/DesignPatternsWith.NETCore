﻿using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Command.DP.Commands;
using WebApp.Command.DP.Models;


namespace WebApp.Command.DP.Controllers;

public class ProductsController(AppIdentityDbContext context) : Controller
{
    private readonly AppIdentityDbContext _context = context;

    public async Task<IActionResult> Index() => View(await _context.Products.ToListAsync());

    public async Task<IActionResult> CreateFile(int type)
    {
        var products = await _context.Products.ToListAsync();

        FileCreateInvoker fileCreateInvoker = new();

        EFileType fileType = (EFileType)type;

        switch (fileType)
        {
            case EFileType.Excel:
                ExcelFile<Product> excelFile = new(products);

                fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));

                break;

            case EFileType.Pdf:
                PdfFile<Product> pdfFile = new(products, HttpContext);
                fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                break;

            default:
                break;
        }

        return fileCreateInvoker.CreateFile();
    }

    public async Task<IActionResult> CreateFiles()
    {
        var products = await _context.Products.ToListAsync();
        ExcelFile<Product> excelFile = new(products);
        PdfFile<Product> pdfFile = new(products, HttpContext);
        FileCreateInvoker fileCreateInvoker = new();

        fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));
        fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));

        var filesResult = fileCreateInvoker.CreateFiles();

        using (var zipMemoryStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create))
            {
                foreach (var item in filesResult)
                {
                    var fileContent = item as FileContentResult;

                    var zipFile = archive.CreateEntry(fileContent.FileDownloadName);

                    using (var zipEntryStream = zipFile.Open())
                    {
                        await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
                    }
                }
            }

            return File(zipMemoryStream.ToArray(), "application/zip", "all.zip");
        }
    }
}
