using Moj.CMS.Integration.ClientGenerator.Dtos;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.ClientGenerator
{
    class Program
    {
        public static List<FileDto> Files { get; set; } = new List<FileDto>();
        public static string CurrentDirectory => Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin", StringComparison.Ordinal));

        static async Task Main(string[] args)
        {
            var folderPath = CurrentDirectory + @"SwaggerDocuments";

            if (!Directory.Exists(folderPath))
                return;

            var filePaths = Directory.EnumerateFiles(folderPath, "*.json").ToList();

            Files = filePaths.Select(filePath => new FileDto
            {
                Id = filePaths.IndexOf(filePath) + 1,
                FileName = filePath.Replace(folderPath + "\\", ""),
                Path = filePath
            }).ToList();

            if (Files.Count == 0)
                Console.WriteLine("No files found!");
            else
            {
                var tryAgain = false;
                do
                {
                    tryAgain = false;
                    Console.WriteLine("Please choose option from 0 to {0}", Files.Count);
                    var optionsMsg = "0- All \r\n" +
                       string.Join("\r\n", Files.Select(file => $"{file.Id}- {file.FileName}"));
                    Console.WriteLine(optionsMsg);
                    Console.Write("Option number: ");
                    var selectedOption = Console.ReadLine();
                    if (ValidateInput(selectedOption))
                    {
                        try
                        {
                            Console.WriteLine("Started processing...");
                            await ProcessSelectedOption(selectedOption);
                            Console.WriteLine("File(s) generated successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong {0}", ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, would you like to try again? [Y/N].");
                        tryAgain = Console.ReadLine().ToLower() == "y";
                    }
                } while (tryAgain);
            }
        }

        public static bool ValidateInput(string selectedOptionId)
        {
            var correct = Files.Any(file => file.Id.ToString() == selectedOptionId) || selectedOptionId == "0";
            return correct;
        }

        public static async Task ProcessSelectedOption(string selectedOptionId)
        {
            var fileList = selectedOptionId == "0"
                ? Files
                : Files.Where(file => file.Id.ToString() == selectedOptionId);
            await GenerateFilesAsync(fileList);
        }

        public static async Task GenerateFilesAsync(IEnumerable<FileDto> fileList)
        {
            foreach (var file in fileList)
            {
                string jsonString = await File.ReadAllTextAsync(file.Path);

                var openApiDocument = await OpenApiDocument.FromJsonAsync(jsonString);

                var className = file.FileName.Replace("Swagger.json", "Service");

                var settings = new CSharpClientGeneratorSettings
                {
                    ClassName = className,
                    GeneratePrepareRequestAndProcessResponseAsAsyncMethods = true,
                    ClientBaseClass = "Moj.CMS.Integration.ClientGenerator.Services.Extensions.ServiceBase",
                    CSharpGeneratorSettings =
                    {
                        Namespace = $"Moj.CMS.Integration.ClientGenerator.Services.{className}"
                    }
                };

                var generator = new CSharpClientGenerator(openApiDocument, settings);
                var code = generator.GenerateFile();

                var clientPath = CurrentDirectory + @$"Services\{settings.ClassName}.cs";

                await File.WriteAllTextAsync(clientPath, code);
            }
        }
    }
}
