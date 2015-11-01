using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("rijndael", Description = "encrypts and decrypts using the rijndael algorithm")]
	public class RijndaelCommand : SymmetricAlgorithmCommandBase
	{
		public override SymmetricAlgorithm CreateSymmetricAlgorithm()
		{
			return new RijndaelManaged();
		}
	}
}