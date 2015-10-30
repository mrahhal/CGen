using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Konsola;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class Command : CommandBase<Context>
	{
		[Parameter("name", Description = "the file name to output the result to")]
		public string FileName { get; set; }

		public override void ExecuteCommand()
		{
			var result = ExecuteCore();
			if (string.IsNullOrWhiteSpace(FileName))
			{
				Console.WriteLine(result);
			}
			else
			{
				File.WriteAllText(FileName, result);
			}
		}

		public abstract string ExecuteCore();
	}
}