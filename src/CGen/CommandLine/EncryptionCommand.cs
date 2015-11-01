using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("encrypt", Description = "encrypt data using various types of algorithms")]
	public class EncryptionCommand : EncryptionDecryptionAlgorithmCommandBase
	{
		protected override byte[] ExecuteCore()
		{
			var data = GetData();
			var key = GetKey();
			return Encrypt(data, key);
		}

		private byte[] Encrypt(byte[] data, byte[] key)
		{
			if (IsSymmetric)
			{
				return EncryptSymmetric(data, key);
			}
			else
			{
				return EncryptAsymmetric(data, key);
			}
		}

		protected byte[] EncryptSymmetric(byte[] data, byte[] key)
		{
			byte[] encrypted;

			using (var algorithm = CreateSymmetricAlgorithm(Type))
			{
				var iv = algorithm.IV;
				var encryptor = algorithm.CreateEncryptor(key, iv);

				using (var ms = new MemoryStream())
				{
					ms.Write(iv, 0, iv.Length);
					using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					{
						cs.Write(data, 0, data.Length);
					}
					encrypted = ms.ToArray();
				}
			}

			return encrypted;
		}

		protected byte[] EncryptAsymmetric(byte[] data, byte[] key)
		{
			using (var algorithm = CreateAsymmetricAlgorithm(Type))
			{
				algorithm.ImportCspBlob(key);
				return algorithm.Encrypt(data, false);
			}
		}
	}
}