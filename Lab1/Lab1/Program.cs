using System;
using System.IO;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Определение относительных путей для входного и выходного файлов
                string rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
                string inputPath = Path.Combine(rootDirectory, "INPUT.TXT");
                string outputPath = Path.Combine(rootDirectory, "OUTPUT.TXT");

                // Чтение данных из входного файла
                string[] inputData = GetDataFromFile(inputPath);
                int x = int.Parse(inputData[0].Trim());

                // Получение количества решений
                int result = GetWaysToRepresentAsSum(x);

                // Запись результата в выходной файл
                File.WriteAllText(outputPath, result.ToString());

                Console.WriteLine($"Количество способов представления {x} как суммы четырех чисел: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static int GetWaysToRepresentAsSum(int x)
        {
            int count = 0;

            // Перебор всех возможных значений a, b, c
            for (int a = 1; a <= x - 3; a++)  // a <= x - 3, потому что d минимум 1
            {
                for (int b = a; b <= x - 2; b++)  // b >= a, b <= x - 2, потому что c и d минимум 1
                {
                    for (int c = b; c <= x - 1; c++)  // c >= b, c <= x - 1, потому что d минимум 1
                    {
                        int d = x - a - b - c;  // находим d, остальная часть от x
                        if (d >= c)  // проверяем условие d >= c
                        {
                            count++;  // если условие выполнено, увеличиваем счетчик
                        }
                    }
                }
            }

            return count;
        }

        // Метод для чтения данных из файла
        public static string[] GetDataFromFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
