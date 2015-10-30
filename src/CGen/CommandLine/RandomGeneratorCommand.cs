using Konsola;
using Konsola.Parser;
//using Konsola.Constraints;

namespace CGen.CommandLine
{
	[Command("rand", Description = "generate cryptographically random bytes")]
	public class RandomGeneratorCommand : Command
	{
		private const int DefaultLength = 64;

		//[Range(2, 1024, IsMaxInclusive = true)]
		[Parameter("length,l", Description = "how many bytes to generate")]
		public int Length { get; set; } = DefaultLength;

		public override string ExecuteCore()
		{
			var bytes = Util.RandomBytes(Length);
			return Util.ToHex(bytes);
		}
	}
}