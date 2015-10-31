using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("rand", Description = "generate cryptographically random bytes")]
	public class RandomGeneratorCommand : Command
	{
		[Parameter("length,l",
			Description = "how many bytes to generate",
			Default = 64,
			Position = 1)]
		public int Length { get; set; }

		[OnParsed]
		public void OnParsed()
		{
			if (Length < 1 || Length > 1024)
			{
				throw new CommandLineException("length should be between 2 and 1024");
			}
		}

		public override string ExecuteCore()
		{
			var bytes = Util.RandomBytes(Length);
			return Util.ToHex(bytes);
		}
	}
}