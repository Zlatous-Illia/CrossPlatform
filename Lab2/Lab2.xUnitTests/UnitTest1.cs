using System;
using Xunit;
using Lab2;

namespace Lab2.xUnitTests
{
    public class UnitTest1
    {
        // ����: �������� ���������� ������� ������ ��� ������� ���� (������ "3 3")
        [Fact]
        public void Test_ValidateDimensions_CorrectData()
        {
            string input = "3 3";
            bool result = Program.ValidateDimensions(input, out int rowsTotal, out int colsTotal);

            Assert.True(result);  // �������, ��� ����� ����� true, ��� ��� ������ ���������
            Assert.Equal(3, rowsTotal);  // �������, ��� rowsTotal ����� ����� 3
            Assert.Equal(3, colsTotal);  // �������, ��� colsTotal ����� ����� 3

            Console.WriteLine($"Test 1 passed: {result}, Field size: {rowsTotal} x {colsTotal}");
        }

        // ����: �������� ������������ ������� ������ (������ ����� - �����)
        [Fact]
        public void Test_ValidateDimensions_IncorrectData()
        {
            string input = "3 a";
            bool result = Program.ValidateDimensions(input, out int rowsTotal, out int colsTotal);

            Assert.False(result);  // �������, ��� ����� ����� false, ��� ��� ������ �����������
            Console.WriteLine($"Test 2 passed: {result}, Incorrect input data.");
        }

        // ����: �������� ����������� ���������� ���� � ����������� �������
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

        // ����: �������� ������������� ����� ����� � ������
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

        // ����: �������� ������������� ����� �������� � ������
        [Fact]
        public void Test_ValidateAndFillField_IncorrectColumnCount()
        {
            string[] inputData = new string[]
            {
                "3 3",  
                "2 1 2",
                "1 1 1",
                "1 1"  // ������������ ������ (�� ������� ��������)
            };
            bool result = Program.ValidateAndFillField(inputData, 3, 3);

            Assert.False(result);
            Console.WriteLine($"Test 5 passed: {result}, Incorrect number of columns.");
        }

        // ����: �������� ������������ �������� ������ (��������� ��������)
        [Fact]
        public void Test_MayGoToRight_ValidMove()
        {
            bool result = Program.MayGoToRight(2, 5, 2);  
            Assert.True(result);  
            Console.WriteLine($"Test 6 passed: {result}, Move to the right is possible.");
        }

        // ����: �������� �������������� �������� ������ (����������� ��������)
        [Fact]
        public void Test_MayGoToRight_InvalidMove()
        {
            bool result = Program.MayGoToRight(3, 5, 3); 
            Assert.False(result);
            Console.WriteLine($"Test 7 passed: {result}, Move to the right is not possible.");
        }

        // ����: �������� ������������ �������� ���� (��������� ��������)
        [Fact]
        public void Test_MayGoToDown_ValidMove()
        {
            bool result = Program.MayGoToDown(1, 4, 2);
            Assert.True(result);
            Console.WriteLine($"Test 8 passed: {result}, Move down is possible.");
        }

        // ����: �������� �������������� �������� ���� (����������� ��������)
        [Fact]
        public void Test_MayGoToDown_InvalidMove()
        {
            bool result = Program.MayGoToDown(2, 3, 2);  // ������: �������� ���� ����������
            Assert.False(result);  // �������, ��� �������� ���� ����������
            Console.WriteLine($"Test 9 passed: {result}, Move down is not possible.");
        }

        // ����: �������� ���������� ��������� �����, ����� �������� ���������� (������� ������)
        [Fact]
        public void Test_GetVariantsCount_NoPaths()
        {
            // ����� ������� ���� � �������������, ������� ��������� ��� ��������� ����
            Program.field = new int[,]
            {
                { 1, 2, 3 },
                { 1, 0, 1 },
                { 1, 2, 0 }
            };

            // ������ ����: 3 ������ � 3 �������
            int rows = Program.field.GetLength(0);
            int cols = Program.field.GetLength(1);

            // �������� ����� � ��������� ���������
            long result = Program.GetVariantsCount(rows, cols);

            // �������, ��� ���������� ��������� ����� ����� ����� 0
            Assert.Equal(0, result);
            Console.WriteLine($"Test 10 passed: No possible paths. Number of possible paths = {result}");
        }

        // ����: �������� ���������� ��������� �����, ����� ���������� ��������� ��������� (������� ������)
        [Fact]
        public void Test_GetVariantsCount_MultiplePaths()
        {
            // ����� ������� ����, � ������� ���������� ��������� ��������� �����
            Program.field = new int[,]
            {
                { 2, 1, 1, 2 },
                { 3, 2, 1, 44 },
                { 3, 1, 1, 0 }
            };

            // ������ ����: 3 ������ � 4 �������
            int rows = Program.field.GetLength(0);
            int cols = Program.field.GetLength(1);

            // �������� ����� � ��������� ���������
            long result = Program.GetVariantsCount(rows, cols);

            // �������, ��� ���������� ��������� ����� ����� ����� 3
            Assert.Equal(3, result);
            Console.WriteLine($"Test 11 passed: Multiple paths available. Number of possible paths = {result}");
        }
    }
}
