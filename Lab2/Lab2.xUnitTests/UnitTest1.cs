using System;
using Xunit;
using Lab2;

namespace Lab2.xUnitTests
{
    public class UnitTest1
    {
        // Тест: Проверка корректных входных данных для размера поля (формат "3 3")
        [Fact]
        public void Test_ValidateDimensions_CorrectData()
        {
            string input = "3 3";
            bool result = Program.ValidateDimensions(input, out int rowsTotal, out int colsTotal);

            Assert.True(result);  // Ожидаем, что метод вернёт true, так как данные корректны
            Assert.Equal(3, rowsTotal);  // Ожидаем, что rowsTotal будет равно 3
            Assert.Equal(3, colsTotal);  // Ожидаем, что colsTotal будет равно 3

            Console.WriteLine($"Test 1 passed: {result}, Field size: {rowsTotal} x {colsTotal}");
        }

        // Тест: Проверка некорректных входных данных (вместо числа - буква)
        [Fact]
        public void Test_ValidateDimensions_IncorrectData()
        {
            string input = "3 a";
            bool result = Program.ValidateDimensions(input, out int rowsTotal, out int colsTotal);

            Assert.False(result);  // Ожидаем, что метод вернёт false, так как данные некорректны
            Console.WriteLine($"Test 2 passed: {result}, Incorrect input data.");
        }

        // Тест: Проверка корректного заполнения поля с правильными данными
        [Fact]
        public void Test_ValidateAndFillField_CorrectData()
        {
            string[] inputData = new string[]
            {
                "3 3",  
                "2 1 2",
                "1 1 1",
                "1 1 0"
            };
            bool result = Program.ValidateAndFillField(inputData, 3, 3);

            Assert.True(result);
            Console.WriteLine($"Test 3 passed: {result}, Game field successfully filled.");
        }

        // Тест: Проверка некорректного числа строк в данных
        [Fact]
        public void Test_ValidateAndFillField_IncorrectRowCount()
        {
            string[] inputData = new string[]
            {
                "3 3",  
                "2 1 2",
                "1 1 1"
            };
            bool result = Program.ValidateAndFillField(inputData, 3, 3);

            Assert.False(result);
            Console.WriteLine($"Test 4 passed: {result}, Incorrect number of rows.");
        }

        // Тест: Проверка некорректного числа столбцов в данных
        [Fact]
        public void Test_ValidateAndFillField_IncorrectColumnCount()
        {
            string[] inputData = new string[]
            {
                "3 3",  
                "2 1 2",
                "1 1 1",
                "1 1"  // Некорректная строка (не хватает столбцов)
            };
            bool result = Program.ValidateAndFillField(inputData, 3, 3);

            Assert.False(result);
            Console.WriteLine($"Test 5 passed: {result}, Incorrect number of columns.");
        }

        // Тест: Проверка корректности движения вправо (возможное движение)
        [Fact]
        public void Test_MayGoToRight_ValidMove()
        {
            bool result = Program.MayGoToRight(2, 5, 2);  
            Assert.True(result);  
            Console.WriteLine($"Test 6 passed: {result}, Move to the right is possible.");
        }

        // Тест: Проверка некорректности движения вправо (невозможное движение)
        [Fact]
        public void Test_MayGoToRight_InvalidMove()
        {
            bool result = Program.MayGoToRight(3, 5, 3); 
            Assert.False(result);
            Console.WriteLine($"Test 7 passed: {result}, Move to the right is not possible.");
        }

        // Тест: Проверка корректности движения вниз (возможное движение)
        [Fact]
        public void Test_MayGoToDown_ValidMove()
        {
            bool result = Program.MayGoToDown(1, 4, 2);
            Assert.True(result);
            Console.WriteLine($"Test 8 passed: {result}, Move down is possible.");
        }

        // Тест: Проверка некорректности движения вниз (невозможное движение)
        [Fact]
        public void Test_MayGoToDown_InvalidMove()
        {
            bool result = Program.MayGoToDown(2, 3, 2);  // Пример: движение вниз невозможно
            Assert.False(result);  // Ожидаем, что движение вниз невозможно
            Console.WriteLine($"Test 9 passed: {result}, Move down is not possible.");
        }

        // Тест: Проверка количества возможных путей, когда движение невозможно (сложный случай)
        [Fact]
        public void Test_GetVariantsCount_NoPaths()
        {
            // Задаём игровое поле с препятствиями, которые блокируют все возможные пути
            Program.field = new int[,]
            {
                { 1, 2, 3 },
                { 1, 0, 1 },
                { 1, 2, 0 }
            };

            // Размер поля: 3 строки и 3 столбца
            int rows = Program.field.GetLength(0);
            int cols = Program.field.GetLength(1);

            // Вызываем метод и проверяем результат
            long result = Program.GetVariantsCount(rows, cols);

            // Ожидаем, что количество возможных путей будет равно 0
            Assert.Equal(0, result);
            Console.WriteLine($"Test 10 passed: No possible paths. Number of possible paths = {result}");
        }

        // Тест: Проверка количества возможных путей, когда существует несколько вариантов (сложный случай)
        [Fact]
        public void Test_GetVariantsCount_MultiplePaths()
        {
            // Задаём игровое поле, в котором существуют несколько возможных путей
            Program.field = new int[,]
            {
                { 2, 1, 1, 2 },
                { 3, 2, 1, 44 },
                { 3, 1, 1, 0 }
            };

            // Размер поля: 3 строки и 4 столбца
            int rows = Program.field.GetLength(0);
            int cols = Program.field.GetLength(1);

            // Вызываем метод и проверяем результат
            long result = Program.GetVariantsCount(rows, cols);

            // Ожидаем, что количество возможных путей будет равно 3
            Assert.Equal(3, result);
            Console.WriteLine($"Test 11 passed: Multiple paths available. Number of possible paths = {result}");
        }
    }
}
