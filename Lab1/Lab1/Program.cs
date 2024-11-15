using System;
using System.IO;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Определение относительных путей для входного и выходного файлов
                string rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
                string inputPath = Path.Combine(rootDirectory, "INPUT.TXT");
                string outputPath = Path.Combine(rootDirectory, "OUTPUT.TXT");

                // Чтение данных из входного файла
                string[] inputData = GetDataFromFile(inputPath);

                // Проверка входных данных
                string validationError = ValidateInput(inputData);
                if (validationError != null)
                {
                    Console.WriteLine(validationError);
                    return;
                }

                // Попытка преобразовать данные в число
                int x = int.Parse(inputData[0].Trim());

                // Получение количества решений
                int result = GetWaysToRepresentAsSum(x);

                // Запись результата в выходной файл
                File.WriteAllText(outputPath, result.ToString());

                Console.WriteLine($"The number of ways to represent {x} as a sum of four integers: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Метод для вычисления количества способов представления x как суммы четырех чисел
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
            // Проверка существования файла
            if (!File.Exists(path))
            {
                Console.WriteLine($"Error: The input file '{path}' does not exist.");
                return Array.Empty<string>();
            }

            return File.ReadAllLines(path);
        }

        // Метод для проверки входных данных
        public static string ValidateInput(string[] inputData)
        {
            // Проверка на наличие данных в файле
            if (inputData.Length == 0)
            {
                return "Error: The input file is empty.";
            }

            // Попытка преобразовать данные в число
            if (!int.TryParse(inputData[0].Trim(), out int x))
            {
                return "Error: The input data is not a valid integer.";
            }

            // Проверка диапазона числа x
            if (x < 1 || x > 1500)
            {
                return "Error: The number x must be in the range 1 ≤ x ≤ 1500.";
            }

            return null; // Если все проверки прошли успешно
        }
    }
}
