using System;
using System.Collections.Generic;
using System.Text;
using Server.Services;

namespace Server.Tests
{
    class CalculatorServiceTests
    {

        [Test]
        public void Add_ShouldReturnCorrectSum()
        {
            // Arrange
            var calculatorService = new CalculatorServiceImp();
            int a = 5;
            int b = 10;
            // Act
            int result = calculatorService.Add(a, b);
            // Assert
            Assert.AreEqual(15, result);
		}
	}
}
