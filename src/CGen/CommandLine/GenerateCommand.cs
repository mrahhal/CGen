using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("generate", Description = "generate public/private keys using the rsa algorithm")]
	public class GenerateCommand : Command
	{
		[Parameter("name",
			Description = "the file's name that will contain the keys")]
		public string FileName { get; set; }

		public override void ExecuteCommand()
		{
			var publicKey = GenerateKey(false);
			var privateKey = GenerateKey(true);

			var name = FileName;
			File.WriteAllBytes(Path.Combine(PathName, name + ".pub"), publicKey);
			File.WriteAllBytes(Path.Combine(PathName, name), privateKey);
		}

		[OnParsed]
		public void OnParsed()
		{
			if (PathName == null || !Directory.Exists(PathName))
			{
				throw new CommandLineException("The output should represent a directory.");
			}

			if (FileName == null)
			{
				FileName = "key-" + new Random().Next(100000);
			}
		}

		private RSACryptoServiceProvider Create()
		{
			return new RSACryptoServiceProvider();
		}

		public byte[] GenerateKey(bool includePrivateParameters)
		{
			using (var algorithm = Create())
			{
				return algorithm.ExportCspBlob(includePrivateParameters);
			}
		}

		protected override byte[] ExecuteCore()
		{
			throw new NotImplementedException();
		}
	}
}