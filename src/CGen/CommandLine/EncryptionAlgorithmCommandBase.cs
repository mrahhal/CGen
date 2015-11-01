using System.IO;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class EncryptionAlgorithmCommandBase : Command
	{
		[Parameter("data", Description = "the data", Position = 1)]
		public string Data { get; set; }

		[Parameter("input", Description = "the file's name or path that contains the data")]
		public string InputFileName { get; set; }

		[Parameter("key", Description = "the key to use in the algorithm")]
		public string Key { get; set; }

		[Parameter("encrypt", Description = "encrypt the data")]
		public bool IsEncrypt { get; set; }

		[Parameter("decrypt", Description = "decrypt the data")]
		public bool IsDecrypt { get; set; }

		[OnParsed]
		public virtual void OnParsed()
		{
			//if (Data == null && InputFileName == null)
			//{
			//	throw new CommandLineException("Either data or a file name should be provided.")
			//	{
			//		Kind = CommandLineExceptionKind.Message,
			//	};
			//}

			ValidateInputFileName();
		}

		protected void ValidateInputFileName()
		{
			if (InputFileName != null && !File.Exists(InputFileName))
			{
				throw new CommandLineException(
					string.Format("The file at \"{0}\" does not exist.", InputFileName))
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}
		}

		protected void ValidateForEncryptDecryptOnly()
		{
			if ((IsEncrypt && IsDecrypt) || (!IsEncrypt && !IsDecrypt))
			{
				throw new CommandLineException("Specify only one option to do, either encrypt or decrypt.")
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}
		}
	}
}