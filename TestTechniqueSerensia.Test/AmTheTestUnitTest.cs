using TestTechniqueSerensia.Entities;
using TestTechniqueSerensia.Interface;

namespace TestTechniqueSerensia.Test
{
    public class AmTheTestUnitTest
    {
        IAmTheTest test;

        public AmTheTestUnitTest()
        {
            test = new AmTheTest();
        }
        [Fact]
        public void TestGetSuggestionsWhenIsNotNull()
        {
            // Act
            var listOfSuggestions = test.GetSuggestions("gros", new List<string> { "gros", "gros", "graisse", "aggressif" }, 2);

            // Assert
            Assert.NotNull(listOfSuggestions);
        }

        [Fact]
        public void TestGetSuggestionsShouldReturnOrderedSuggestions()
        {

            // Act
            var listOfSuggestions = test.GetSuggestions(
                "gros",
                new List<string> { "test", "gros", "gras" },
                3
            ).ToList();

            // Assert
            Assert.Equal(
                new List<string> { "gros", "gras", "test" },
                listOfSuggestions
            );
        }

        [Fact]
        public void TestGetSuggestionsWhenTermExist()
        {
            // Act
            var listOfSuggestions = test.GetSuggestions("gros", new List<string> { "gros", "gras", "graisse", "agressif","go","ros","gro" }, 2);
            // Assert
            Assert.Contains(listOfSuggestions, x => x == "gros");
            Assert.Equal(2, listOfSuggestions.Count());
        }


        [Fact]
        public void TestGetDifferenceScoreWhenDestIsEmpty()
        {
            //Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                test.GetDifferenceScore(string.Empty, "test"));
        }

        [Fact]
        public void TestGetDifferenceScoreWhenSrcIsEmpty()
        {
            //Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                test.GetDifferenceScore("test", string.Empty));
        }

        [Fact]
        public void TestGetDifferenceScoreWhenBothAreEmpty()
        {
            //Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                test.GetDifferenceScore(string.Empty, string.Empty));
        }


        [Fact]
        public void TestGetDifferenceScoreWhenBothAreEqual()
        {
            // Act
            var result = test.GetDifferenceScore("gros", "gros");
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestGetDifferenceScoreWhenSrcIsLonger()
        {
            // Act
            var result = test.GetDifferenceScore("gros", "graisse");
            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestGetDifferenceScoreWhenSrcIsShorter()
        {
            // Act
            var result = test.GetDifferenceScore("gros", "gro");

            // Assert
            Assert.Equal(int.MaxValue, result);
        }

        [Fact]
        public void TestGetDifferenceScoreWhenOneLetterIsDifferent()
        {
            // Arrange
            test = new AmTheTest();

            // Act
            var result = test.GetDifferenceScore("gros", "gras");

            // Assert
            Assert.Equal(1, result);
        }
    }
}