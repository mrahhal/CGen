using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("aes", Description = "encrypts and decrypts using the aes algorithm")]
	public class AesCommand : SymmetricAlgorithmCommandBase
	{
		public override SymmetricAlgorithm CreateSymmetricAlgorithm()
		{
			return new AesManaged();
		}
	}
}