using NUnit.Framework;
using RegistrationChecker.Helpers;

namespace RegistrationChecker.Tests
{
    public class ValidationHelperTests
    {
        private ValidationHelperTestValues testValues;

        [SetUp]
        public void Setup()
        {
            testValues = new ValidationHelperTestValues
            {
                ExampleCurrent = "XX11XXX",
                ExampleNorthernIreland = "OIL1111",
                ExampleDiplomatic = "111X111",
                ExampleDatelessLongNumberPrefix = "X111XXX",
                ExampleInvalid = "11111111111111111111111111111"  // Does not conform to any known standard
            };
        }

        [Test]
        public void ValidateRegistration_Validates_Current()
        {
            // Arrange
            string current = testValues.ExampleCurrent;

            // Act
            var result = ValidationHelper.ValidateRegistration(current);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void ValidateRegistration_Validates_Northern_Ireland()
        {
            // Arrange
            string northernIreland = testValues.ExampleNorthernIreland;

            // Act
            var result = ValidationHelper.ValidateRegistration(northernIreland);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void ValidateRegistration_Validates_Diplomatic()
        {
            // Arrange
            string diplomatic = testValues.ExampleDiplomatic;

            // Act
            var result = ValidationHelper.ValidateRegistration(diplomatic);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void ValidateRegistration_Validates_Dateless_Long_Number()
        {
            // Arrange
            string datelessLongNumber = testValues.ExampleDatelessLongNumberPrefix;

            // Act
            var result = ValidationHelper.ValidateRegistration(datelessLongNumber);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void ValidateRegistration_Fails_Invalid()
        {
            // Arrange
            string invalid = testValues.ExampleInvalid;

            // Act
            var result = ValidationHelper.ValidateRegistration(invalid);

            // Assert
            Assert.False(result);
        }
    }

    public class ValidationHelperTestValues
    {
        public string ExampleCurrent { get; set; }
        public string ExampleNorthernIreland { get; set; }
        public string ExampleDiplomatic { get; set; }
        public string ExampleDatelessLongNumberPrefix { get; set; }
        public string ExampleInvalid { get; set; }
    }
}
