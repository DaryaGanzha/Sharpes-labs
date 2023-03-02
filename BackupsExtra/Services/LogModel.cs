using System;

namespace BackupsExtra.Services
{
    internal static class LogModel
    {
        public static void AddLogInfo(string log)
        {
            const string path = @"../../../../BackupsExtra/Services/log.txt";
            System.IO.File.AppendAllLines(path, new string[] { $"Время: {DateTime.Now} | Информация: {log} " });
        }
    }
}