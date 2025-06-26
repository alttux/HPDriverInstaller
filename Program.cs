using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Запуск установочных скриптов...");
        Console.WriteLine();

        // Получаем текущий каталог приложения
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Получаем все подкаталоги в текущем каталоге
        string[] directories = Directory.GetDirectories(currentDirectory);

        bool anyScriptExecuted = false;

        foreach (string dir in directories)
        {
            string installScript = Path.Combine(dir, "install.cmd");

            if (File.Exists(installScript))
            {
                anyScriptExecuted = true;
                Console.WriteLine($"Найден скрипт: {installScript}");
                Console.WriteLine("Запуск...");

                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = installScript;
                    process.StartInfo.WorkingDirectory = dir;
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                    process.WaitForExit();

                    Console.WriteLine($"Скрипт завершился с кодом: {process.ExitCode}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выполнении скрипта: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        if (!anyScriptExecuted)
        {
            Console.WriteLine("Не найдено ни одного скрипта install.cmd в подкаталогах.");
        }

        Console.WriteLine("Все операции завершены.");
        Console.WriteLine();
        Console.Write("Перезагрузить компьютер сейчас? (y/n): ");

        var key = Console.ReadKey();
        if (key.KeyChar == 'y' || key.KeyChar == 'Y')
        {
            Console.WriteLine();
            Console.WriteLine("Инициируется перезагрузка...");
            Process.Start("shutdown", "/r /t 0");
        }

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}