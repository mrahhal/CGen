using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("sha1", Description = "generate sha1 hash")]
	public class SHA1Command : HashAlgorithmCommandBase
	{
		public override HashAlgorithm CreateHashAlgorithm()
		{
			return new SHA1Managed();
		}
	}
}