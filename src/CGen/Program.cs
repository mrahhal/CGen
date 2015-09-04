using System;
using System.Collections.Generic;
using System.Linq;
using CGen.CommandLine;
using Konsola;

namespace CGen
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var context = CommandLineParser.Parse<Context>(args, new DefaultConsole());
			if (context == null)
				return;
			context.Command.ExecuteCommand();
        }
	}
}