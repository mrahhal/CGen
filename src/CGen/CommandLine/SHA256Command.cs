using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("sha256", Description = "generate sha256 hash")]
	public class SHA256Command : HashAlgorithmCommandBase
	{
		public override HashAlgorithm CreateHashAlgorithm()
		{
			return new SHA256Managed();
		}
	}
}