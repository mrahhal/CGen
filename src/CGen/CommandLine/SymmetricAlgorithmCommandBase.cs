using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class SymmetricAlgorithmCommandBase : Command
	{
		[Parameter("data", Description = "the data", Position = 1)]
		public string Data { get; set; }

		[Parameter("input", Description = "the file's name or path that contains the data")]
		public string InputFileName { get; set; }

		[Parameter("key", Description = "the key to use in the algorithm", IsMandatory = true)]
		public string Key { get; set; }

		[Parameter("encrypt", Description = "encrypt the data")]
		public bool IsEncrypt { get; set; }

		[Parameter("decrypt", Description = "decrypt the data")]
		public bool IsDecrypt { get; set; }

		[OnParsed]
		public virtual void OnParsed()
		{
			if ((IsEncrypt && IsDecrypt) || (!IsEncrypt && !IsDecrypt))
			{
				throw new CommandLineException("Specify only one option to do, either encrypt or decrypt.")
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}

			if (Data == null && InputFileName == null)
			{
				throw new CommandLineException("Either data or a file name should be provided.")
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}

			if (InputFileName != null && !File.Exists(InputFileName))
			{
				throw new CommandLineException(
					string.Format("The file at \"{0}\" does not exist.", InputFileName))
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}
		}

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