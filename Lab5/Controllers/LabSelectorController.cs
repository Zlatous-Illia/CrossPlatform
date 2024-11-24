using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using LabLibrary;

namespace Lab5.Controllers
{
    public class LabSelectorController : Controller
    {
        public LabSelectorController()
        {
        }

        public IActionResult Lab1()
        {
            return View();
        }

        public IActionResult Lab2()
        {
            return View();
        }

        public IActionResult Lab3()
        {
            return View();
        }

        // ����� ��� ��������� ���������� ������������ ������
        [HttpPost]
        public IActionResult RunLab(string labName, IFormFile inputFile, IFormFile outputFile)
        {
            if (inputFile == null || outputFile == null)
            {
                ViewBag.ErrorMessage = "Please select both input and output files.";
                return View(labName);
            }

            // ����������� ���������� �� ������� ��� �������� ����������� ������
            var serverDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(serverDirectory))
            {
                Directory.CreateDirectory(serverDirectory);
            }

            // ��������� ������ ����� ��� ���������� ������ �� �������
            var inputFilePath = Path.Combine(serverDirectory, inputFile.FileName);
            var outputFilePath = Path.Combine(serverDirectory, outputFile.FileName);

            try
            {
                // ���������� �������� ����� �� �������
                using (var inputStream = new FileStream(inputFilePath, FileMode.Create))
                {
                    inputFile.CopyTo(inputStream);
                }

                // ���������� ��������� ����� �� �������
                using (var outputStream = new FileStream(outputFilePath, FileMode.Create))
                {
                    outputFile.CopyTo(outputStream);
                }

                // ���������� ��������� ������������ ������ � �������������� ���������� ����� � ������
                LabRunner.RunChosenLab(labName, inputFilePath, outputFilePath);

                string inputFileContent = System.IO.File.ReadAllText(inputFilePath);
                string outputFileContent = System.IO.File.ReadAllText(outputFilePath);

                ViewBag.SuccessMessage = $"{labName} executed successfully!";
                ViewBag.InputFileContent = inputFileContent;
                ViewBag.OutputFileContent = outputFileContent;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error while running {labName}: {ex.Message}";
            }

            return View(labName);
        }
    }
}