using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class AsymmetricAlgorithmCommandBase : EncryptionAlgorithmCommandBase
	{
		[Parameter("generate", Description = "generate public/private keys")]
		public bool IsGenerate { get; set; }

		[Parameter("kname", Description = "the name of the file that will contain the public/private keys")]
		public string KeyFileName { get; set; }

		[OnParsed]
		public override void OnParsed()
		{
			base.OnParsed();

			if (PathName == null)
			{
				PathName = Environment.CurrentDirectory;
			}
			if (!Directory.Exists(PathName))
			{
				throw new CommandLineException($"The directory at {PathName} doesn't exist")
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}

			if (IsGenerate && KeyFileName == null)
			{
				KeyFileName = "key-" + new Random().Next(10000);
			}
		}

		public override void ExecuteCommand()
		{
			if (!IsGenerate)
			{
				base.ExecuteCommand();
				return;
			}

			var bytes = new byte[256];
			//var chars = new char[256];
			for (int i = 0; i < 256; i++)
			{
				bytes[i] = (byte)i;
				//chars[i] = (char)i;
			}
			//var text = new string(chars);
			File.WriteAllBytes("s.txt", bytes);
			//File.WriteAllText("s2.txt", text, Encoding.Default);



			var publicKey = GenerateKey(false);
			var privateKey = GenerateKey(true);

			var name = KeyFileName;
			File.WriteAllBytes(Path.Combine(PathName, name + ".pub"), publicKey);
			File.WriteAllBytes(Path.Combine(PathName, name), privateKey);
		}

		public override string ExecuteCore()
		{
			var data = Encoding.GetBytes(Data);
			var result = default(byte[]);

			Debug.Assert(!IsGenerate);
			if (IsEncrypt)
			{
				result = Encrypt(data);
			}
			else
			{
				result = Decrypt(data);
			}

			return Encoding.GetString(result);
		}

		public abstract byte[] GenerateKey(bool includePrivateParameters);

		public abstract byte[] Encrypt(byte[] data);

		public abstract byte[] Decrypt(byte[] data);
	}
}