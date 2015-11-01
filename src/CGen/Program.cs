using CGen.CommandLine;
using Konsola.Parser;

namespace CGen
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var parser = new CommandLineParser<Context>();
			var result = parser.Parse(args);
			if (result.Kind == ParsingResultKind.Success)
			{
				result.Context.Command.ExecuteCommand();
			}
		}
	}
}