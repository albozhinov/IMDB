using IMDB.Console.ConsoleProviders;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace IMDB.Tests.Console.ConsoleProvidersTests.MenuTests
{
    [TestClass]
    public class WriteOptionsShould
    {
        [TestMethod]
        public void CallWriteLineOnWriter_Always()
        {
            //Arange
            var writerMock = new Mock<IUIWriter>();
            var loginSessionStub = new Mock<ILoginSession>();
            loginSessionStub
                .SetupGet(lss => lss.LoggedUserPermissions)
                .Returns(new List<string>());
            var sut = new Menu(writerMock.Object, loginSessionStub.Object);
            //Act
            sut.WriteOptions();
            //Assert
            writerMock.Verify(wm => wm.WriteLine(It.IsAny<string>()));
        }
    }
}
