using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3
{
    public class Program
    {
        // Определение ходов коня
        static readonly (int, int)[] MOVES = new (int, int)[]
        {
            (-2, -1), (-1, -2), (1, -2), (2, -1),
            (2, 1), (1, 2), (-1, 2), (-2, 1)
        };

        public static void Main(string[] args)
        {
            // Определение путей для входного и выходного файлов
            string rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            string inputPath = Path.Combine(rootDirectory, "INPUT.TXT");
            string outputPath = Path.Combine(rootDirectory, "OUTPUT.TXT");

            // Считываем входные данные
            var inputLines = File.ReadAllLines(inputPath);
            var board = new string[3];
            var target = new string[3];
        }
    }
}