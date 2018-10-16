using IMDB.Console.ConsoleProviders;
using IMDB.Console.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace IMDB.Tests.Console.ConsoleProvidersTests.CommandProcessorShould
{
    [TestClass]
    public class ProcessCommandShould
    {
        [TestMethod]
        public void CallParserWithParsedParameters_Always()
        {
            //Arange
            const string cmdName = "rand";
            var parserMock = new Mock<ICommandParser>();
            var cmdStub = new Mock<ICommand>();
            parserMock
                .Setup(pm => pm.ParseCommand(It.IsAny<string>()))
                .Returns(cmdStub.Object);
            var sut = new CommandProcessor(parserMock.Object);
            //Act
            sut.ProcessCommand($"{cmdName} -");
            //Assert
            parserMock.Verify(pm => pm.ParseCommand(cmdName));
        }
        [TestMethod]
        public void CallRunWithParsedParameters_Always()
        {
            //Arange
            const string cmdName = "rand";
            const string cmdArg1 = "arg1";
            const string cmdArg2 = "arg2";
            var parserMock = new Mock<ICommandParser>();
            var cmdMock = new Mock<ICommand>();
            parserMock
                .Setup(pm => pm.ParseCommand(It.IsAny<string>()))
                .Returns(cmdMock.Object);
            var sut = new CommandProcessor(parserMock.Object);
            //Act
            sut.ProcessCommand($"{cmdName} -  {cmdArg1}:{cmdArg2}");
            //Assert
            cmdMock.Verify(pm => pm.Run(new List<string> { cmdArg1, cmdArg2 }));
        }
        [TestMethod]
        public void ReturnsErrorMessage_WhenFormatIsWrong()
        {
            //Arange
            var parserStub = new Mock<ICommandParser>();
            var sut = new CommandProcessor(parserStub.Object);
            //Act
            var result = sut.ProcessCommand("not right format");
            //Assert
            Assert.IsTrue(result.Contains("-"));
        }
    }
}
