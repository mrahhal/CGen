using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("decrypt", Description = "decrypt data using various types of algorithms")]
	public class DecryptionCommand : EncryptionDecryptionAlgorithmCommandBase
	{
		protected override byte[] ExecuteCore()
		{
			var data = GetData();
			var key = GetKey();
			return Decrypt(data, key);
		}

		private byte[] Decrypt(byte[] data, byte[] key)
		{
			if (IsSymmetric)
			{
				return DecryptSymmetric(data, key);
			}
			else
			{
				return DecryptAsymmetric(data, key);
			}
		}

		protected byte[] DecryptSymmetric(byte[] cipherData, byte[] key)
		{
			var buffer = default(byte[]);

			using (var algorithm = CreateSymmetricAlgorithm(Type))
			{
				using (var ms = new MemoryStream(cipherData))
				{
					var iv = new byte[16];
					Util.ReadEx(ms, iv, 0, iv.Length);
					var decryptor = algorithm.CreateDecryptor(key, iv);
					using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					{
						buffer = Util.ReadBytesToEnd(cs);
					}
				}
			}

			return buffer;
		}

		protected byte[] DecryptAsymmetric(byte[] cipherData, byte[] key)
		{
			using (var algorithm = CreateAsymmetricAlgorithm(Type))
			{
				var k = Convert.FromBase64String(Encoding.ASCII.GetString(key));
				algorithm.ImportCspBlob(k);
				return algorithm.Decrypt(cipherData, false);
			}
		}
	}
}