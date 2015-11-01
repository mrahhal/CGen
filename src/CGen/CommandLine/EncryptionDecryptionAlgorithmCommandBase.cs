using System;
using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class EncryptionDecryptionAlgorithmCommandBase : Command
	{
		[Parameter("algorithm",
			Description = "algorithm to use [aes|rijndael|rsa]",
			Default = EncryptionAlgorithmType.Aes,
			Position = 1)]
		public EncryptionAlgorithmType Type { get; set; }

		[Parameter("input",
			Description = "the name of the file to read the data from",
			Position = 2)]
		public string InputFile { get; set; }

		[Parameter("stdin",
			Description = "read data from stdin instead of a file")]
		public bool Stdin { get; set; }

		[Parameter("key",
			Description = "the key to use to encrypt/decrypt")]
		public string Key { get; set; }

		[Parameter("keyfile,kf",
			Description = "the name of the file to read the key from")]
		public string KeyFile { get; set; }

		[OnParsed]
		public void OnParsed()
		{
			if (Key == null && KeyFile == null)
			{
				throw new CommandLineException("You must specify either a key or a key file to use.");
			}

			if (Stdin && !Util.IsInputPiped())
			{
				throw new CommandLineException("You specified stdin, but the input was not piped.");
			}

			if (KeyFile != null && !File.Exists(KeyFile))
			{
				throw new CommandLineException($"File at {KeyFile} does not exist.");
			}

			if (!Stdin && InputFile == null)
			{
				throw new CommandLineException("You must specify an input file to read the data from.");
			}
		}

		protected byte[] GetKey()
		{
			return
				KeyFile != null ?
				File.ReadAllBytes(KeyFile) :
				Util.FromHex(Key);
		}

		protected byte[] GetData()
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

		protected bool IsSymmetric
		{
			get
			{
				switch (Type)
				{
					case EncryptionAlgorithmType.Aes:
					case EncryptionAlgorithmType.Rijndael:
						return true;
					default:
						return false;
				}
			}
		}

		protected SymmetricAlgorithm CreateSymmetricAlgorithm(EncryptionAlgorithmType type)
		{
			switch (type)
			{
				case EncryptionAlgorithmType.Rijndael:
					return new RijndaelManaged();

				case EncryptionAlgorithmType.Aes:
					return new AesManaged();

				default:
					return null;
			}
		}

		protected RSACryptoServiceProvider CreateAsymmetricAlgorithm(EncryptionAlgorithmType type)
		{
			switch (type)
			{
				case EncryptionAlgorithmType.RSA:
					return new RSACryptoServiceProvider();

				default:
					return null;
			}
		}
	}

	public enum EncryptionAlgorithmType
	{
		Aes,
		Rijndael,
		RSA,
	}
}