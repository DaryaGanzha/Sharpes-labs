using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupsExtra.Services;

namespace BackupsExtra
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Получаем активный BakUp , записываем логи о получении
            LogModel.AddLogInfo("Получаем информацию об активном бекапе!");

            var activeBackUp = FileModel.GetActiveBackUp();

            if (!string.IsNullOrEmpty(activeBackUp.FilePath))
            {
                Console.WriteLine($"Активный бекап получен, программа запрустилась из файла {activeBackUp.Name}");
            }
            else
            {
                var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
                FileInfo[] files = dINfo.GetFiles("*.zip");
                if (files != null || files.Count() > 0)
                {
                    FileInfo file = files.OrderByDescending(o => o.LastWriteTime).FirstOrDefault();
                    FileModel.SetActiveBackUp(file);

                    if (file != null)
                        Console.WriteLine($"Активный бекап не был найден получен, программа запрустилась из BackUp. Файл: {file.FullName}");
                }
                else
                {
                    System.IO.File.Create(@"C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\BackupsExtra\Test\Active\");
                    Console.WriteLine($"Активный бекап не был найден получен,Резервных BackUp не обнаружен. Активный файл был создан");
                }
            }

            bool isActive = true;
            while (isActive)
            {
                string result;
                Console.WriteLine("Выберите опцию:");

                Console.WriteLine("1. Получить все Бекапы!");
                Console.WriteLine("2. Сделать Бекап текущего файла!");
                Console.WriteLine("3. Удалить бекап");
                Console.WriteLine("0. Выйти");

                result = Console.ReadLine();

                if (result == "1")
                {
                    LogModel.AddLogInfo("Получаем все бекапы");
                    var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
                    FileInfo[] files = dINfo.GetFiles("*.zip");
                    if (files != null || files.Count() > 0)
                    {
                        for (int i = 0; i < files.Count(); i++)
                        {
                            Console.WriteLine($"№: {i}. Name: {files[i].FullName} . Дата создания: {files[i].CreationTime}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ни один бекап не был найден!");
                    }
                }
                else if (result == "2")
                {
                    LogModel.AddLogInfo("Делаем бекап текущего файла");
                    var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
                    FileInfo[] files = dINfo.GetFiles("*.zip");
                    if (files != null || files.Count() > 0)
                    {
                        if (files.Count() > 2)
                        {
                            files = files.OrderByDescending(o => o.CreationTime).ToArray();
                            for (int i = 2; i < files.Count(); i++)
                            {
                                System.IO.File.Delete(files[i].FullName);
                            }
                        }

                        DateTime date = DateTime.Now;
                        ZipFile.CreateFromDirectory(
                            @"../../../../BackupsExtra/Services/Active/",
                            @"../../../../BackupsExtra/Services/BackUps/" + $"backup{date:yyyy_MM_dd_HH_mm_ss}.zip");
                    }
                }
                else if (result == "3")
                {
                    LogModel.AddLogInfo("Удаляем бекап!");
                    var dINfo = new DirectoryInfo(@"../../../../BackupsExtra/Services/BackUps");
                    FileInfo[] files = dINfo.GetFiles("*.zip");
                    if (files != null || files.Count() > 0)
                    {
                        for (int i = 0; i < files.Count(); i++)
                        {
                            Console.WriteLine($"№: {i}. Name: {files[i].FullName} . Дата создания: {files[i].CreationTime}");
                        }

                        Console.WriteLine("Выберите номер файла, который вы хотите удалить!");
                        int number = Convert.ToInt32(Console.ReadLine());
                        if (number > (files.Count() - 1))
                        {
                            Console.WriteLine("Данного файла не существует");
                        }
                        else
                        {
                            FileInfo fileToDelete = files[number];
                            File.Delete(fileToDelete.FullName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Прежде , чем удалить Бекап ,  его нужно создать!");
                    }
                }
                else if (result == "0")
                {
                    isActive = false;
                }
            }

            Console.WriteLine("Программа завершена!");
        }
    }
}
