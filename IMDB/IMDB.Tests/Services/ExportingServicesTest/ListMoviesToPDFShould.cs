using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Tests.Services.ExportingServicesTest
{
	[TestClass]
	public class ListMoviesToPDFShould
	{
		[TestMethod]
		public void ThrowNotEnoughPermissionsException_WhenUserIsNotAuthorized()
		{
			// Arrange
			var movieRepoStub = new Mock<IRepository<Movie>>();
			movieRepoStub
				.Setup(mr => mr.All())
				.Returns(new List<Movie>() { new Movie { ID = 123, IsDeleted = false } }.AsQueryable());

			var loginSessionMock = new Mock<ILoginSession>();
			loginSessionMock
				.SetupGet(ls => ls.LoggedUserPermissions)
				.Returns(new List<string>() { "cmd0", "cmd1", "butnotlistmoviestopdf" });

			var sut = new ExportingServices(movieRepoStub.Object, loginSessionMock.Object);
			// Act & Assert
			Assert.ThrowsException<NotEnoughPermissionException>(() => sut.ListMoviesToPDF());
		}
	}
}
