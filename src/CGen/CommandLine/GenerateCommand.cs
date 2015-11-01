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
			var publicK = default(byte[]);
			var privateK = default(byte[]);
			GenerateKeys(out publicK, out privateK);
			var publicKey = Convert.ToBase64String(publicK);
			var privateKey = Convert.ToBase64String(privateK);

			var name = FileName;
			File.WriteAllText(Path.Combine(PathName, name + ".pub"), publicKey, Encoding.Default);
			File.WriteAllText(Path.Combine(PathName, name), privateKey, Encoding.Default);
		}

		[OnParsed]
		public void OnParsed()
		{
			if (PathName != null && !Directory.Exists(PathName))
			{
				throw new CommandLineException("The output should represent a directory.");
			}

			if (PathName == null)
			{
				PathName = Environment.CurrentDirectory;
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

		private void GenerateKeys(out byte[] publicKey, out byte[] privateKey)
		{
			using (var algorithm = Create())
			{
				publicKey = algorithm.ExportCspBlob(false);
				privateKey = algorithm.ExportCspBlob(true);
			}
		}

		protected override byte[] ExecuteCore()
		{
			throw new NotImplementedException();
		}
	}
}