using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
	[TestClass]
	public class LogoutCommandShould
	{
		[TestMethod]
		public void ThrowArgumentNullException_WhenArgumentsAreNull()
		{
			//Arrange
			var userServicesStub = new Mock<IUserServices>();

			var sut = new LogoutCommand(userServicesStub.Object);
			//Act & Assert
			Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
		}
		[TestMethod]
		public void ReturnsSuccess_WhenArgumentsAreCorrect()
		{
			//Arrange
			var userServicesStub = new Mock<IUserServices>();

			var sut = new LogoutCommand(userServicesStub.Object);
			//Act
			var result = sut.Run(new List<string>());
			//Assert
			Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
		}
	}
}
