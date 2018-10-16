using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass]
    public class ListMoviesToPDFCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenArgumentsAreNull()
        {
            //Arrange
            var exportingServicesStub = new Mock<IExportingServices>();

            var sut = new ListMoviesToPDFCommand(exportingServicesStub.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
        }
        [TestMethod]
        public void ReturnSuccess_WhenArgumentsAreCorrect()
        {
            //Arrange
            var exportingServicesMock = new Mock<IExportingServices>();

            var sut = new ListMoviesToPDFCommand(exportingServicesMock.Object);
            //Act
            var result = sut.Run(new List<string>());
            //Assert
            Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
            exportingServicesMock.Verify(esm => esm.ListMoviesToPDF());
        }
    }
}
