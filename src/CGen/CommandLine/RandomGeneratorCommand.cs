using System;
using System.IO;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("rand", Description = "generate cryptographically random bytes")]
	public class RandomGeneratorCommand : Command
	{
		[Parameter("length,l",
			Description = "how many bytes to generate",
			Default = 32,
			Position = 1)]
		public int Length { get; set; }

		[OnParsed]
		public void OnParsed()
		{
			if (Length < 1 || Length > 1024)
			{
				throw new CommandLineException("length should be between 1 and 1024") { Kind = CommandLineExceptionKind.Message };
			}
		}

		public override void ExecuteCommand()
		{
			var result = ExecuteCore();
			if (string.IsNullOrWhiteSpace(PathName))
			{
				Console.WriteLine(Util.ToHex(result));
			}
			else
			{
				File.WriteAllBytes(PathName, result);
			}
		}

		protected override byte[] ExecuteCore()
		{
			return Util.RandomBytes(Length);
		}
	}
}