using System;
using Xunit;
using Lab1;

namespace Lab1.xUnitTests
{
    public class ProgramTests
    {
        [Fact]
        public void Test_ValidInput_EqualTo_6()
        {
            // Arrange
            int x = 5;
            int expected = 1;

            // Act
            int actual = Program.GetWaysToRepresentAsSum(x);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_ValidInput_EqualTo_10()
        {
            // Arrange
            int x = 3;
            int expected = 0;

            // Act
            int actual = Program.GetWaysToRepresentAsSum(x);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_ValidInput_EqualTo_1()
        {
            // Arrange
            int x = 1;
            int expected = 0;

            // Act
            int actual = Program.GetWaysToRepresentAsSum(x);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
