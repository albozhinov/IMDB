using IMDB.Console.Contracts;
using System;

namespace IMDB.Console
{
    public sealed class Engine : IEngine
    {
        private IUIReader reader;
        private IUIWriter writer;
		private IMenu menu;
		private ICommandProcessor processor;
        public Engine(IUIReader reader, IUIWriter writer, ICommandProcessor processor, IMenu menu)
        {
            this.processor = processor;
            this.reader = reader;
            this.writer = writer;
			this.menu = menu;
        }
        public void Start()
        {
            while (true)
            {
                try
                {
					menu.WriteOptions();
                    var commandLine = reader.ReadLine();
                    var result = processor.ProcessCommand(commandLine);
                    writer.WriteLine(result);
                }
                catch(Exception e)
                {
                    writer.WriteLine(e.Message);
                }
            }
        }
    }
}
