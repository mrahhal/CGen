using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class SymmetricAlgorithmCommandBase : EncryptionAlgorithmCommandBase
	{
		public override string ExecuteCore()
		{
			var data =
				InputFileName != null ?
				File.ReadAllText(InputFileName) :
				Data;
			var bytes = Encoding.GetBytes(data);
			var key = Util.FromHex(Key);
			var result = default(byte[]);

			using (var algorithm = CreateSymmetricAlgorithm())
			{
				if (IsEncrypt)
				{
					result = Encrypt(algorithm, bytes, key);
				}
				else
				{
					result = Decrypt(algorithm, bytes, key);
				}
				var b2 = Decrypt(algorithm, result, key);
				var b2s = Encoding.GetString(b2);
			}

			return Encoding.GetString(result);
		}

		private byte[] Encrypt(SymmetricAlgorithm algorithm, byte[] data, byte[] key)
		{
			byte[] encrypted;
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

			return encrypted;
		}

		private byte[] Decrypt(SymmetricAlgorithm algorithm, byte[] cipherData, byte[] key)
		{
			var buffer = default(byte[]);

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

			return buffer;
		}

		public abstract SymmetricAlgorithm CreateSymmetricAlgorithm();
	}
}