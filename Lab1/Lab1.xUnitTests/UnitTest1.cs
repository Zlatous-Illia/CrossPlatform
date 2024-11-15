using System;
using Xunit;
using Lab1;

namespace Lab1.xUnitTests
{
    public class ProgramTests
    {
        // Тест: Проверка пустого входного файла (ошибка: файл пустой)
        [Fact]
        public void Test_ValidateInput_EmptyFile()
        {
            string[] inputData = new string[] { };
            string result = Program.ValidateInput(inputData);

            Assert.Equal("Error: The input file is empty.", result);
            Console.WriteLine("Test 1 passed: The input file is empty.");
        }

        // Тест: Проверка некорректного формата данных (не число)
        [Fact]
        public void Test_ValidateInput_InvalidInteger()
        {
            string[] inputData = new string[] { "abc" };
            string result = Program.ValidateInput(inputData);

            Assert.Equal("Error: The input data is not a valid integer.", result);
            Console.WriteLine("Test 2 passed: The input data is not a valid integer.");
        }

        // Тест: Проверка числа вне допустимого диапазона (меньше 1)
        [Fact]
        public void Test_ValidateInput_NumberLessThan1()
        {
            string[] inputData = new string[] { "0" };
            string result = Program.ValidateInput(inputData);

            Assert.Equal("Error: The number x must be in the range 1 ≤ x ≤ 1500.", result);
            Console.WriteLine("Test 3 passed: The number is less than 1.");
        }

        // Тест: Проверка числа вне допустимого диапазона (больше 1500)
        [Fact]
        public void Test_ValidateInput_NumberGreaterThan1500()
        {
            string[] inputData = new string[] { "1501" };
            string result = Program.ValidateInput(inputData);

            Assert.Equal("Error: The number x must be in the range 1 ≤ x ≤ 1500.", result);
            Console.WriteLine("Test 4 passed: The number is greater than 1500.");
        }

        // Тест: Проверка корректного числа в допустимом диапазоне
        [Fact]
        public void Test_ValidateInput_ValidNumber()
        {
            string[] inputData = new string[] { "50" };
            string result = Program.ValidateInput(inputData);

            // Ожидаем, что метод вернет null, если число в допустимом диапазоне
            Assert.Null(result);

            Console.WriteLine($"Test 5 passed: The number is valid. Result is {result}");
        }

        // Тест: Проверка корректности результата для x = число в диапазоне 1 ≤ x ≤ 1500
        [Fact]
        public void Test_GetWaysToRepresentAsSum_CorrectResult_For_X_50()
        {
            int x = 50;
            int result = Program.GetWaysToRepresentAsSum(x);

            Assert.Equal(920, result);

            Console.WriteLine($"Test 6 passed: The number of ways to represent {x} as sum is {result}");
        }
    }
}
