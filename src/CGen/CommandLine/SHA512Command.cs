using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("sha512", Description = "generate sha512 hash")]
	public class SHA512Command : HashAlgorithmCommandBase
	{
		public override HashAlgorithm CreateHashAlgorithm()
		{
			return new SHA512Managed();
		}
	}
}