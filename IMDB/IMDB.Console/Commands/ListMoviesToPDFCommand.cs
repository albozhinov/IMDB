using System;
using System.Collections.Generic;
using System.Text;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
	public sealed class ListMoviesToPDFCommand : ICommand
	{
		private IExportingServices exportingServices;
		public ListMoviesToPDFCommand(IExportingServices exportingServices)
		{
			this.exportingServices = exportingServices;
		}
		public string Run(IList<string> parameters)
		{
			exportingServices.ListMoviesToPDF();

			return "Suceessfully created pdf listing all the movies!";
		}
	}
}
