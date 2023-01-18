using Xunit;
using System.IO;
//using static FiveWordFiveLetters;
using System;

public class TextFileTests
{
    public class FiveWordFiveLettersTests
    {
        [Fact]
        public void ContainsTwoSameLetters_Inputhasduplicates()
        {
            // Arrange
            var input = "hello";

            // Act
            //var result = !FiveWordFiveLetters.FiveWordFiveLetters.ContainsTwoSameLetters(input);

            // Assert
            //Assert.False(result);
        }

        [Fact]
        public void ContainsTwoSameLetters_Inputhasnoduplicates()
        {
            // Arrange
            var input = "abcde";

            // Act
            //var result = !FiveWordFiveLetters.FiveWordFiveLetters.ContainsTwoSameLetters(input);

            // Assert
            //Assert.True(result);
        }
    }
}
