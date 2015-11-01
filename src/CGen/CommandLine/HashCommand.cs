using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("hash", Description = "hashes the input data using various types of algorithms")]
	public class HashCommand : Command
	{
		[Parameter("algorithm",
			Description = "algorithm to use [sha1|sha256|sha512|md5]",
			Default = HashAlgorithmType.SHA1,
			Position = 1)]
		public HashAlgorithmType Type { get; set; }

		[Parameter("input",
			Description = "the name of the file to read the data from",
			Position = 2)]
		public string InputFile { get; set; }

		[Parameter("stdin",
			Description = "read data from stdin instead of a file")]
		public bool Stdin { get; set; }

		[OnParsed]
		public void OnParsed()
		{
			if (InputFile == null && !Stdin)
			{
				throw new CommandLineException("You should either specify an input file or pipe data from stdin.");
			}

			if (Stdin && !Util.IsInputPiped())
			{
				throw new CommandLineException("You specified stdin, but the input was not piped.");
			}

			if (InputFile != null && !File.Exists(InputFile))
			{
				throw new CommandLineException($"File at \"{InputFile}\" does not exist.");
			}
		}

		public override void ExecuteCommand()
		{
			var result = Util.ToHex(ExecuteCore());
			if (string.IsNullOrWhiteSpace(PathName))
			{
				Console.WriteLine(result);
			}
			else
			{
				File.WriteAllText(PathName, result, Encoding.ASCII);
			}
		}

		protected override byte[] ExecuteCore()
		{
			var data = GetData();
			return ComputeHash(data);
		}

		private byte[] GetData()
		{
			if (Stdin)
			{
				return Encoding.GetBytes(Console.In.ReadToEnd());
			}
			else
			{
				return File.ReadAllBytes(InputFile);
			}
		}

		private byte[] ComputeHash(byte[] data)
		{
			using (var algorithm = CreateHashAlgorithm(Type))
			{
				return algorithm.ComputeHash(data);
			}
		}

		private HashAlgorithm CreateHashAlgorithm(HashAlgorithmType type)
		{
			switch (type)
			{
				case HashAlgorithmType.MD5:
					return new MD5CryptoServiceProvider();

				case HashAlgorithmType.SHA256:
					return new SHA256Managed();

				case HashAlgorithmType.SHA512:
					return new SHA512Managed();

				case HashAlgorithmType.SHA1:
				default:
					return new SHA1Managed();
			}
		}
	}

	public enum HashAlgorithmType
	{
		SHA1,
		SHA256,
		SHA512,
		MD5,
	}
}