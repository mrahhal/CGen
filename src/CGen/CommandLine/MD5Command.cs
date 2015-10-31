using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("md5", Description = "generate md5 hash")]
	public class MD5Command : HashAlgorithmCommandBase
	{
		public override HashAlgorithm CreateHashAlgorithm()
		{
			return new MD5CryptoServiceProvider();
		}
	}
}