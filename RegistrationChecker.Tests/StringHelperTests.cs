using NUnit.Framework;
using RegistrationChecker.Helpers;

namespace RegistrationChecker.Tests
{
    public class StringHelperTests
    {
        private StringHelperTestValues testValues;

        [SetUp]
        public void Setup()
        {
            testValues = new StringHelperTestValues
            {
                ExampleWithSpaces = "x111 xxx",
                ExampleLowerCase = "x111xxx",
                ExampleLowerCaseWithSpaces = " x 111    x x     x    "
            };
        }

        [Test]
        public void GetFormattedRegistration_Removes_Whitespace()
        {
            // Arrange
            string inputWithSpaces = testValues.ExampleWithSpaces;

            // Act
            var result = StringHelper.GetFormattedRegistration(inputWithSpaces);

            // Assert
            Assert.That(result, Does.Not.Contain(" "));
        }

        [Test]
        public void GetFormattedRegistration_Capitalizes_Input()
        {
            // Arrange
            string lowerCaseInput = testValues.ExampleLowerCase;

            // Act
            var result = StringHelper.GetFormattedRegistration(lowerCaseInput);

            // Assert
            Assert.AreEqual(result, "X111XXX");
        }

        [Test]
        public void GetFormattedRegistration_Removes_Whitespace_And_Capitalizes_Input()
        {
            // Arrange
            string lowerCaseInputWithSpaces = testValues.ExampleLowerCaseWithSpaces;

            // Act
            var result = StringHelper.GetFormattedRegistration(lowerCaseInputWithSpaces);

            // Assert
            Assert.AreEqual(result, "X111XXX");
        }
    }

    public class StringHelperTestValues
    {
        public string ExampleWithSpaces { get; set; }
        public string ExampleLowerCase { get; set; }
        public string ExampleLowerCaseWithSpaces { get; set; }
    } 
}