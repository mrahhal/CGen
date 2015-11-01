using System.Text;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("rsa", Description = "encrypts and decrypts using the rsa algorithm with public/private keys")]
	public class RSACommand : AsymmetricAlgorithmCommandBase
	{
		private RSACryptoServiceProvider Create()
		{
			return new RSACryptoServiceProvider();
		}

		private byte[] GetCspBlob()
		{
			return Encoding.UTF8.GetBytes(Key);
		}

		public override byte[] GenerateKey(bool includePrivateParameters)
		{
			using (var algorithm = Create())
			{
				return algorithm.ExportCspBlob(includePrivateParameters);
			}
		}

		public override byte[] Encrypt(byte[] data)
		{
			using (var algorithm = Create())
			{
				algorithm.ImportCspBlob(GetCspBlob());
				return algorithm.Encrypt(data, RSAEncryptionPadding.Pkcs1);
			}
		}

		public override byte[] Decrypt(byte[] data)
		{
			using (var algorithm = Create())
			{
				algorithm.ImportCspBlob(GetCspBlob());
				return algorithm.Decrypt(data, RSAEncryptionPadding.Pkcs1);
			}
		}
	}
}