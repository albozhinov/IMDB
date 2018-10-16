using IMDB.Console.ConsoleProviders;
using IMDB.Console.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IMDB.Tests.Console.ConsoleProvidersTests.CommandParserhould.ParseCommandShould
{ 
    [TestClass]
    public class ParseCommandShould
    {
        [TestMethod]
        public void ShouldCallResolve_Always()
        {
            //Arange
            const string cmd1 = "cmd1";
            var iocProviderMock = new Mock<IIOCProvider>();
            var sut = new CommandParser(iocProviderMock.Object);
            //Act
            sut.ParseCommand(cmd1);
            //Assert
            iocProviderMock.Verify(sm => sm.ResolveNamed<ICommand>(cmd1), Times.Once);
        }
        [TestMethod]
        public void ShouldThrowNotImplementedCommand_WhenProviderThrows()
        {
            //Arrange
            const string cmd1 = "cmd1";
            var iocProviderMock = new Mock<IIOCProvider>();
            iocProviderMock
                .Setup(iocpm => iocpm.ResolveNamed<ICommand>(cmd1))
                .Callback(() => throw new NotImplementedException());
            var sut = new CommandParser(iocProviderMock.Object);
            //Act
            Assert.ThrowsException<NotImplementedException>(() => sut.ParseCommand(cmd1), "Command not found!");
        }
    }
}
